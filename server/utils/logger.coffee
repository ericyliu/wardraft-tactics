module.exports =

  verbose: true
  queryIndicator: '|'

  logVerbose: (text) ->
    if @verbose then @log text

  logError: (text) ->
    @log "ERROR: #{text}"

  log: (text) ->
    console.log text

  sendError: (ws, text) ->
    data = message: text
    @sendData ws, 'server/error', data

  sendMessage: (ws, text) ->
    data = message: text
    @sendData ws, 'server/message', data

  sendData: (ws, route, data) ->
    console.log ws
    console.log data
    console.log route
    ws.send "#{route}#{@queryIndicator}#{JSON.stringify data}"
