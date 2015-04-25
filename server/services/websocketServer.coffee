Logger = require '../utils/logger'
Router = require './router'
wss = require('ws').Server

nextId = 0

WebSocketServer =

  server: undefined

  start: (port) ->
    @server = new wss port: port

    @server.on 'connection', (ws) ->
      Logger.log 'A user has connected'
      Logger.sendMessage ws, 'You have connected.'
      ws.data =
        id: nextId
      nextId++

      ws.on 'message', (message) ->
        Router.route ws, message

      ws.on 'close', (code, message) ->
        Logger.log 'A user has disconnected'

module.exports = WebSocketServer
