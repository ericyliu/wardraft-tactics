class Map

  constructor: (id) ->
    @tiles = if id then @loadMap id else []

  loadMap: (id) -> return



module.exports = Map
