Logger = require '../utils/logger'
fs = require 'fs'

routesPath = 'services/routes/'

Router =

  routes: {}

  init: () ->
    routeFiles = fs.readdirSync routesPath
    for routeFile in routeFiles
      routeGroup = require "./routes/#{routeFile}"
      for routeName in Object.keys routeGroup
        @routes["#{(routeFile.split '.')[0]}/#{routeName}"] = routeGroup[routeName]

  route: (ws, message) ->
    route = (message.split Logger.queryIndicator)[0]
    try
      data = JSON.parse (message.split Logger.queryIndicator)[1]
    catch e
      Logger.logError e
      return

    if @routes[route] is undefined
      Logger.sendError ws, "Route #{route} does not exist."
      return

    @routes[route] ws, data

module.exports = Router
