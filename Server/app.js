
// var express = require('express');
// var app     = express();
// var http    = require('http').Server(app);
// var io = require('socket.io')(http);
// var path    = require('path');
// http.listen(7727, function(){
//   console.log("server on!: http://localhost:3000/");
// });
// app.use(express.static(path.join(__dirname,"public")));

var io = require('socket.io')({
	transports: ['websocket'],
});
io.attach(7728);
var userList = [];
console.log('server is on port 7728');
// var catWinCnt = 0;
// var mouseWinCnt = 0;
// var socket = io();
var lobbyManager = new (require('./LobbyManager.js'))(io);
var roomManager = new (require('./RoomManager.js'))(io);

io.on('connection', function(socket){
	var matching = false;
	console.log('영남이형');
	var nickname;
	socket.on('join', function(data){
		nickname = data.name;

		console.log(data);
		socket.emit('join', data);
	});

	socket.on('pick', function(data){
		var tmp = data.pick;
		console.log(tmp);
		matching = true;
		lobbyManager.push({socket, nickname, tmp});
		lobbyManager.dispatch(roomManager);
	});

	socket.on('userData', function(data){
		var roomNum = roomManager.roomIndex[socket.id];
		io.to(roomNum).emit('userData', data);
	});
	socket.on('gameLoad', function(){
		var roomNum = roomManager.roomIndex[socket.id];
		io.to(roomNum).emit('gameLoad', {ready : ++roomManager.rooms[roomNum].userCnt});
	});
	socket.on('move', function(data){
		var roomNum = roomManager.roomIndex[socket.id];
		socket.broadcast.to(roomNum).emit('move', data);
	});
	socket.on('bullet', function(data){
		var roomNum = roomManager.roomIndex[socket.id];
		io.to(roomNum).emit('bullet', data);
	});
	socket.on('rotate', function(data){
		var roomNum = roomManager.roomIndex[socket.id];
		socket.broadcast.to(roomNum).emit('rotate', data);
	});
	socket.on('hp', function(data){
		var roomNum = roomManager.roomIndex[socket.id];
		socket.broadcast.to(roomNum).emit('hp', data);
	});
	socket.on('die', (data)=>{
		console.log('죽음을 감지!');
		if(data.pick == 0){
			console.log('고양이의 승리!');
			var roomNum = roomManager.roomIndex[socket.id];
			var room = roomManager.rooms[roomNum];
			if(room != undefined)
			{
				room.players.forEach(function(pack){
					console.log(pack.tmp);
					if(data.pick == pack.tmp)
					{
						pack.socket.emit('lose', {a : "a"});
					}
					else{
						pack.socket.emit('win', {a : "a"});
					}
					// delete RmMg.roomIndex[data.socket.id];
				});
			}
			var roomNum = roomManager.roomIndex[socket.id];
			// io.to(roomNum).emit('total', {
			// 	cat : catWinCnt,
			// 	mouse : mouseWinCnt,
			// });
			if(roomNum){
				console.log('게임결과후 부서지는 방');
				roomManager.rooms[roomNum].userCnt--;
				roomManager.destroy(roomNum, lobbyManager);
			}
			lobbyManager.dispatch(roomManager);
		}
		else if(data.pick == 1){
			console.log('쥐의 승리');
			var roomNum = roomManager.roomIndex[socket.id];
			var room = roomManager.rooms[roomNum];
			if(room != undefined)
			{
				room.players.forEach(function(pack){
					if(data.pick == pack.tmp)
					{
						pack.socket.emit('lose', {a : "a"});
					}
					else{
						pack.socket.emit('win', {a : "a"});
					}
					// delete RmMg.roomIndex[data.socket.id];
				});
			}
			var roomNum = roomManager.roomIndex[socket.id];
			// io.to(roomNum).emit('total', {
			// 	cat : catWinCnt,
			// 	mouse : mouseWinCnt,
			// });
			if(roomNum){
				console.log('게임결과후 부서지는 방');
				roomManager.rooms[roomNum].userCnt--;
				roomManager.destroy(roomNum, lobbyManager);
			}
			lobbyManager.dispatch(roomManager);
		}
	});
	socket.on('disconnect', function(){

		var roomNum = roomManager.roomIndex[socket.id];
    if(roomNum){
			console.log('탈주로 인해 부서지는 방');
			roomManager.rooms[roomNum].userCnt--;
			roomManager.destroy(roomNum, lobbyManager);
    }
		if(matching){
			lobbyManager.kick(socket);
		}
		lobbyManager.dispatch(roomManager);
    console.log('user disconnected: ', socket.id);
    //console.log(socket);
  });
});
