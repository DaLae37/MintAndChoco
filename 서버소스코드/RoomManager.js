

function RoomManager(io){

  var RmMg = this;
  RmMg.rooms = [];
  RmMg.roomIndex = [];
  RmMg.create = function(data0,data1){
    var roomNum = data0.socket.id+data1.socket.id;
    var room = new Room(roomNum,data0,data1);
    data0.socket.join(roomNum);
    data1.socket.join(roomNum);
    io.to(roomNum).emit("pick", {a : "a"});
    RmMg.rooms[roomNum] = room;
    RmMg.roomIndex[data0.socket.id] = roomNum;
    RmMg.roomIndex[data1.socket.id] = roomNum;

    console.log("Room Created :", roomNum);
  
  };
  RmMg.destroy = function(roomNum, LbMg){
      var room = RmMg.rooms[roomNum];
      room.players.forEach(function(data){
        data.socket.emit('getout', {a : "a"});
  			LbMg.kick(data.socket);
        delete RmMg.roomIndex[data.socket.id];
      });
      console.log('방이 터졌다...흑 흑 흑 너무 슬퍼용');
      delete RmMg.rooms[roomNum];
  };
}

function Room(num, player0, player1)
{
  this.num = num;
  this.players = [player0, player1];
  this.userCnt = 0;
}

module.exports = RoomManager;
