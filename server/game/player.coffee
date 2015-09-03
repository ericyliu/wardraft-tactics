class Player

  constructor: (@id, @connection, @color) ->
    @account = @connection.data.account
    @buildable = ['worker']
    @units = []

module.exports = Player
