Logger = require '../utils/logger'
Router = require './router'
wss = require('ws').Server
ConnectionMap = require '../models/ConnectionMap'

WebSocketServer =

  server: undefined

  nextId: 0

  start: (address, port) ->
    @server = new wss {address: address, port: port}, () ->
      Logger.log "Websocket server started on #{address}:#{port}"

    @server.on 'connection', (ws) =>
      Logger.log 'A user has connected'
      Logger.sendMessage ws, 'You have connected.'
      ws.data =
        id: @nextId
      @nextId++
      ConnectionMap[ws.data.id] = ws

      ws.on 'message', (message) ->
        Router.route ws, message

      ws.on 'close', (code, message) ->
        Logger.log 'A user has disconnected'
        if ws.data and ConnectionMap[ws.data.id]
          Router.route ws, "account/logout|{}" if ws.data.account
          delete ConnectionMap[ws.data.id]

module.exports = WebSocketServer
