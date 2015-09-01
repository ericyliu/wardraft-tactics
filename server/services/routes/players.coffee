ConnectionMap = require '../../models/connectionMap'
Logger = require '../../utils/logger'
AccountHelper = require '../../utils/account'

PlayersRoutes =

  # route: players/list

  list: (ws, route, data) ->
    players = []
    for id, connection of ConnectionMap
      if AccountHelper.isLoggedIn connection
        players.push
          id: id
          username: connection.data?.account?.username

    Logger.sendData ws, route,
      success: true
      players: players


module.exports = PlayersRoutes
