Player = require './player'
Colors = require './enums/colors'
GeneralUtils = require './utils/general'
Events = require './events'
Map = require './map'

class WardraftGame

  constructor: (@id) ->
    @players = [] #id: player
    @turn = undefined #player id
    @map = new Map

  addPlayer: (id, connection) ->
    @players.add new Player
      id: id
      connection: connection
      color: Colors[@players.count]

  # Game
  startGame: ->
    @turn = 0
    GeneralUtils.shuffle @players

  nextTurn: ->
    @turn++
    @turn = 0 if @turn >= @players.count
    Events.trigger 'nextTurn'

module.exports = WardraftGame
