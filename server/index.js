require('coffee-script/register');
var app = require('express')();
var http = require('http').Server(app);
var Router = require('./services/router');
var Logger = require('./utils/logger')
var WebSocketServer = require('./services/websocketServer');
var config = require('./config')

app.get('/', function(req, res){
  res.sendfile('index.html');
});

Router.init();
WebSocketServer.start(config.server.wsPort);

http.listen(config.server.port, function(){
  console.log("listening on " + config.server.address + ":" + config.server.port);
});
