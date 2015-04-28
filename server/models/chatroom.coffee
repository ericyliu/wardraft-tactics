class Chatroom

  constructor: (@id) ->
    @connections = {}
    @history = []
    @maxHistoryLength = 10
    @name = ''
    @persist = false

  addConnection: (ws) =>
    @connections[ws.data.account.username] = ws

  removeConnection: (username) =>
    if @connections[username]
      delete @connections[username]

  getConnection: (username) =>
    @connections[username]

  addMessage: (sender, message) =>
    chat =
      sender: sender
      text: message.text
      action: message.action
      time: new Date()
    if @history.length > @maxHistoryLength
      @history.splice 0, 1
    @history.push chat

    return chat

  getJsonReady: () ->
    jsonObject = {}
    jsonObject.history = @history
    jsonObject.name = @name
    jsonObject.id = @id
    jsonObject.people = Object.keys @connections
    return jsonObject

module.exports = Chatroom
