require('coffee-script/register');
var app = require('express')();
var http = require('http').Server(app);
var Router = require('./services/router');
var Logger = require('./utils/logger')
var WebSocketServer = require('./services/websocketServer');
var MongoDBService = require('./services/mongoDBService');
var config = require('./config')

app.get('/', function(req, res){
  res.sendfile('index.html');
});

Router.init();
MongoDBService.start(config.db.address, config.db.port);
WebSocketServer.start(config.server.address, config.server.wsPort);

http.listen(config.server.port, function(){
  Logger.log("HTTP server started on " + config.server.address + ":" + config.server.port);
});
