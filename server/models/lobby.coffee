ChatSystem = require './ChatSystem'

class Lobby

  constructor: (@id) ->
    @connections = {}
    @chatroom = ChatSystem.createChatroom

  addConnection: (ws) =>
    @connections[ws.data.account.username] = ws
    @chatroom.addConnection ws

  removeConnection: (username) =>
    if @connections[username]
      delete @connections[username]
    @chatroom.removeConnection ws

  getConnection: (username) =>
    @connections[username]

  getJsonReady: () ->
    jsonObject = {}
    jsonObject.id = @id
    jsonObject.users = Object.keys @connections
    jsonObject.chatroom = @chatroom.id
    return jsonObject

module.exports = Lobby
