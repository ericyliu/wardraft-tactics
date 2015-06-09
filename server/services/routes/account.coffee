Logger = require '../../utils/logger'
Account = require '../../models/schemas/account'
Mongoose = require('mongoose-q')()
WebSocket = require 'ws'
ConnectionMap = require '../../models/connectionMap'
AccountHelper = require '../../utils/account'

AccountRoutes =

  # route: account/register
  # parameters:
  #   username: username to register
  #   password: password to register

  register: (ws, route, data) ->
    if data.username is undefined or data.password is undefined
      Logger.sendData ws, route,
        success: false
        message: 'Registration requires a username and a password.'
      return
    Logger.logVerbose "Registering <#{data.username}> with password <#{data.password}>"

    promise = Account.findOne
      username: data.username

    promise.execQ()
      .then (result) ->
        if result isnt null
          Logger.sendData ws, route,
            success: false
            message: 'Username already taken.'
          return

        account = new Account
          username: data.username
          password: data.password
          friends: []

        account.saveQ()
          .then () ->
            Logger.sendData ws, route,
              success: true,
              message: "You have successfully registered!"
            Logger.log "Successfully registered <#{data.username}>"
          .catch (err) ->
            Logger.logError err
          .done()


  # route: account/login
  # parameters:
  #   username: username to login
  #   password: password to login

  login: (ws, route, data) ->
    if data.username is undefined or data.password is undefined
      Logger.sendFailure ws, route,
        message: 'Logging in requires a username and a password.'
      return

    for key, connection of ConnectionMap
      if connection.data.account?.username is data.username
        Logger.sendFailure ws, route,
          message: 'User already logged in.'
        return

    Logger.logVerbose "Logging in <#{data.username}> with password <#{data.password}>"

    promise = Account.findOne
      username: data.username

    promise.execQ()
      .then (result) ->
        if result is null
          Logger.sendFailure ws, route,
            message: "Username is not registered."
          return
        if result.password != data.password
          Logger.sendFailure ws, route,
            message: "Password is incorrect."
          return
        ws.data.account = result
        ws.data.chatrooms ?= []
        Logger.sendSuccess ws, route,
          message: "You have successfully logged in."
        Logger.log "<#{data.username}> has logged in"

        for id, connection of ConnectionMap
          if AccountHelper.isLoggedIn connection
            Logger.sendSuccess connection, 'players/login',
              id: ws.data.id
              username: data.username

      .catch (err) ->
        Logger.logError err
      .done()


  # route: account/logout

  logout: (ws, route, data) ->
    return unless AccountHelper.isLoggedIn ws
    username = ws.data.account.username
    Logger.logVerbose "Logging out <#{username}>"
    @['chat/leave'] ws, 'chat/leave'
    delete ws.data.account
    Logger.sendSuccess ws, route,
      message: 'You have logged out.'
    Logger.log "<#{username}> has logged out"

    for id, connection of ConnectionMap
      if AccountHelper.isLoggedIn connection
        Logger.sendSuccess connection, 'players/logout',
          id: ws.data.id
          username: username

module.exports = AccountRoutes
