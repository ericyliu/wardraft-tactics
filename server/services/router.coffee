Logger = require '../utils/logger'
Util = require './util.coffee'
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
      Logger.sendError ws, "Invalid message format. Expected: 'route|JsonObject'"
      return

    if @routes[route] is undefined or route.indexOf('_') isnt -1
      Logger.sendError ws, "Route #{route} does not exist."
      return

    if (Util.isLoggedIn ws) or (route is 'account/register') or (route is 'account/login')
      @routes[route] ws, route, data
    else
      Logger.sendError ws, "Must be logged in use route '#{route}'."

module.exports = Router
