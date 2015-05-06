Mongoose = require 'mongoose'
Logger = require '../utils/logger'

MongoDBService =

  start: (address, port) ->
    Mongoose.connect "mongodb://#{address}:#{port}/wardraft"

    db = Mongoose.connection;
    db.on('error', console.error.bind(console, 'connection error:'))
    db.once 'open', (callback) ->
      Logger.log "Connected to MongoDB at #{address}:#{port}"

module.exports = MongoDBService
