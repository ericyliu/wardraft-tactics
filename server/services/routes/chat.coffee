Chatroom = require '../../models/chatroom.coffee'
ChatSystem = require '../../models/chatSystem.coffee'
Logger = require '../../utils/logger'

ChatSystem.start()

ChatRoutes =

  # route: chat/create
  # parameters:
  #   name: (optional) name of chatroom

  create: (ws, route, data) ->
    username = ws.data.account.username
    chatroom = ChatSystem.createChatroom data.type
    data.name ?= ''
    chatroom.name = data.name
    Logger.sendSuccess ws, route,
      success: true
      id: chatroom.id

  # route: chat/join
  # parameters:
  #   id: id of room to join
  #   type: (optional) private or public

  join: (ws, route, data) ->
    if data.id is undefined
      Logger.sendFailure ws, route,
        message: 'Joining a chatroom requires an id.'
      return

    if data.type isnt undefined
      if data.type isnt 'public' and data.type isnt 'private'
        Logger.sendFailure ws, route,
          message: "Type must be 'public' or 'private'."
        return

    chatroom = ChatSystem.getChatroom data.id, data.type
    if chatroom is undefined
      data.type ?= ''
      Logger.sendFailure ws, route,
        message: "Unable to find a #{data.type} room with id: #{data.id}"
      return

    username = ws.data.account.username
    chat = chatroom.addMessage username,
      text: "#{username} has joined the room."
      action: "join"

    @["chat/_sendChat"] chatroom, chat

    chatroom.addConnection ws

    ws.data.chatrooms ?= []
    ws.data.chatrooms.push chatroom.id

    Logger.sendSuccess ws, route, chatroom.getJsonReady()

  # route: chat/leave
  # parameters:
  #   id: (optional) leave all chatrooms if no id is provided

  leave: (ws, route, data) ->
    username = ws.data.account.username
    if data?.id then ids = [data.id] else ids = ws.data.chatrooms
    count = 0
    for id in ids
      chatroom = ChatSystem.getChatroom id
      if chatroom
        chatroom.removeConnection ws.data.account.username
        chat = chatroom.addMessage username,
          text: "#{username} has left the room."
          action: "leave"
        @["chat/_sendChat"] chatroom, chat
        count++

    Logger.sendSuccess ws, route,
      message: "Removed user from #{count} chatrooms."

  # route: chat/message
  # parameters:
  #   id: id of room to send message to
  #   message: message to sends
  #   type: (optional) private or public

  message: (ws, route, data) ->
    if data.id is undefined or data.message is undefined
      Logger.sendFailure ws, route,
        message: 'Sending a message requires a chatroom id and message.'
      return

    chatroom = ChatSystem.getChatroom data.id, data.type
    if chatroom is undefined
      Logger.sendFailure ws, route,
        message: "Unable to find a room with id: #{data.id}"
      return

    username = ws.data.account.username
    chat = chatroom.addMessage username,
      message: data.message
      action: "message"

    @["chat/_sendChat"] chatroom, chat

  list: (ws, route, data) ->
    rooms = ChatSystem.getAllChatrooms 'public'
    Logger.sendSuccess ws, route,
      chatrooms: rooms

  _sendChat: (chatroom, chat) ->
    for name, connection of chatroom.connections
      Logger.sendData connection, 'chat/message',
        id: chatroom.id
        chat: chat

module.exports = ChatRoutes

