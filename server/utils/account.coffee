module.exports =
  isLoggedIn: (ws) ->
    ws.data.account isnt undefined
