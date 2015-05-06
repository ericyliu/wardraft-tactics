stackTrace = require 'stack-trace'
WebSocket = require 'ws'

module.exports =

  verbose: true
  time: false
  filepath: false

  queryIndicator: '|'

  logVerbose: (text) ->
    if @verbose then @_log @formatLog text

  logError: (text) ->
    @_log @formatLog "ERROR: #{text}"

  log: (text) ->
    @_log @formatLog text

  formatLog: (text) ->
    stack = stackTrace.get()
    path = stack[2].getFileName().split '/'
    filename = if @filepath then "[.../#{path.slice(path.length - 3).join '/'}] " else ''
    datetime = if @time then "#{new Date().toLocaleDateString()} #{new Date().toLocaleTimeString()} " else ''
    text = "#{datetime}#{filename}#{text}"

  _log: (text) ->
    console.log text

  sendError: (ws, text) ->
    data = message: text
    @sendData ws, 'server/error', data

  sendMessage: (ws, text) ->
    data = message: text
    @sendData ws, 'server/message', data

  sendSuccess: (ws, route, data) ->
    data.success = true
    @sendData ws, route, data

  sendFailure: (ws, route, data) ->
    data.success = false
    @sendData ws, route, data

  sendData: (ws, route, data) ->
    if ws.readyState is WebSocket.OPEN
      ws.send "#{route}#{@queryIndicator}#{JSON.stringify data}"
