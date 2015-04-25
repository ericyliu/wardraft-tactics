Logger = require '../../utils/logger'

module.exports =

  echo: (ws,data) ->
    if data.message is undefined
      Logger.sendError ws, 'test/echo requires a "message" in the data'
      return
    Logger.sendMessage ws, data.message
