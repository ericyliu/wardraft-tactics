Logger = require '../../utils/logger'
Account = require '../../models/schemas/account'
Mongoose = require('mongoose-q')()

AccountRoutes =

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

  login: (ws, route, data) ->
    if data.username is undefined or data.password is undefined
      Logger.sendData ws, route,
        success: false
        message: 'Logging in requires a username and a password.'
      return
    Logger.logVerbose "Logging in <#{data.username}> with password <#{data.password}>"

    promise = Account.findOne
      username: data.username

    promise.execQ()
      .then (result) ->
        if result is null
          Logger.sendData ws, route,
            success: false
            message: "Username is not registered."
          return
        if result.password != data.password
          Logger.sendData ws, route,
            success: false
            message: "Password is incorrect."
          return
        ws.data.account = result
        Logger.sendData ws, route,
          success: true
          message: "You have successfully logged in."
        Logger.log "<#{data.username}> has logged in"
      .catch (err) ->
        Logger.logError err
      .done()

  logout: (ws, route, data) ->
    if ws.data.account is undefined
      Logger.sendData ws, route,
        success: false
        message: 'You are not logged in.'
      return
    username = ws.data.account.username
    Logger.logVerbose "Logging out <#{username}>"

    delete ws.data.account
    Logger.sendData ws, route,
      success: true
      message: 'You have logged out.'
    Logger.log "<#{username}> has logged out"

module.exports = AccountRoutes
