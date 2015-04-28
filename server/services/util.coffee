module.exports =

  isLoggedIn: (ws) ->
    return false if ws?.data?.account is undefined
    return true
