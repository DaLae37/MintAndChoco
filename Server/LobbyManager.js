function LobbyManager(io){
  var LbMg = this;
  LbMg.lobby = [];
  LbMg.cat = [];
  LbMg.mouse = [];
  LbMg.updating = false;

  //메인 화면에 사람등장!
  LbMg.push = function(data){
    // LbMg.lobby.push({
    //   socket : socket,
    //   pick, pick,
    // });
    if(data.tmp == 0)
    {
      LbMg.mouse.push(data)
    }
    else if (data.tmp == 1)
    {
      LbMg.cat.push(data)
    }
  };
  // LbMg.push = function(socket, pick){
  //   LbMg.lobby.push(socket);
  // };
  LbMg.kick = function(socket){
    var index0 = -1;
    var index1 = -1;
    for(var i=0; i<LbMg.mouse.length; i++)
    {
      if(LbMg.mouse[i].socket == socket){
        console.log('고양이 새끼가 나감');
        index0 = i;
      }
    }
    for(var i=0; i<LbMg.cat.length; i++)
    {
      if(LbMg.cat[i].socket == socket){
        console.log('쥐 새끼가 나감');
        index1 = i;
      }
    }
    if(index0 >= 0) LbMg.mouse.splice(index0, 1);
    if(index1 >= 0) LbMg.cat.splice(index1, 1);
    //console.log("index :", index);
    //console.log("length :", LbMg.lobby.length);
  };
  LbMg.dispatch = function(RmMg){
    if(LbMg.dispatching) return;
    LbMg.dispatching = true;
    while(LbMg.cat.length >= 1 && LbMg.mouse.length >= 1){
      var player0 = LbMg.mouse.splice(0,1);
      var player1 = LbMg.cat.splice(0,1);
      //console.log("player0: ,player0);
      RmMg.create(player0[0],player1[0]);
    }
    LbMg.dispatching = false;
  };
}
module.exports = LobbyManager;
