Chatroom = require './chatroom'

ChatSystem =

  nextId: 0

  chatrooms:
    public: {}
    private: {}

  start: () ->
    @_setupLobbyChatroom()

  createChatroom: (type) ->
    type ?= 'private'
    chatroom = new Chatroom(++@nextId)
    @chatrooms[type][chatroom.id] = chatroom
    return chatroom

  removeChatroom: (id, type) ->
    delete @chatrooms[type][id]

  getChatroom: (id, type) ->
    if type then @chatrooms[type][id]
    if @chatrooms['public'][id] then @chatrooms['public'][id] else @chatrooms['private'][id]

  getAllChatrooms: (type) ->
    ids = Object.keys @chatrooms[type]
    rooms = []
    for id in ids
      rooms.push
        id: id
        name: @chatrooms[type][id].name
        people: (Object.keys @chatrooms[type][id].connections).length
    return rooms

  _setupLobbyChatroom: () ->
    chatroom = new Chatroom(++@nextId)
    chatroom.persists = true
    chatroom.name = "Lobby"
    @chatrooms.public[chatroom.id] = chatroom

module.exports = ChatSystem
