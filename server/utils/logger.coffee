stackTrace = require 'stack-trace'

module.exports =

  verbose: true
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
    filename = path.slice(path.length - 3).join '/'
    datetime = "#{new Date().toLocaleDateString()} #{new Date().toLocaleTimeString()}"
    text = "#{datetime} [.../#{filename}] #{text}"

  _log: (text) ->
    console.log text

  sendError: (ws, text) ->
    data = message: text
    @sendData ws, 'server/error', data

  sendMessage: (ws, text) ->
    data = message: text
    @sendData ws, 'server/message', data

  sendData: (ws, route, data) ->
    ws.send "#{route}#{@queryIndicator}#{JSON.stringify data}"
