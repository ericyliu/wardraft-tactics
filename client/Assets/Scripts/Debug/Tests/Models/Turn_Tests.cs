using UnityEngine;
using System.Collections.Generic;

public static partial class Tests {
  
  public static void Turn_Tests () {
    
    Debug.Log("Turns");
    
    int seed = 1234567890;
    string mapname = "testmap";
    List<User> users = new List<User>() {new User("user_1","user_id_1"),
      new User("user_2","user_id_2"),
      new User("user_3","user_id_3"),
      new User("user_4","user_id_4")};
    Game game = new Game(mapname,users,seed);
    
    Test test;
    
    test = new Test("Turns increment correctly");
      game.startGame();
      test.assert("first turn is 0").Equal(game.turn.number,0);
      test.assert("first turn is first player").Equal(game.turn.player.name,game.players[0].name);
      game.nextTurn();
      test.assert("second turn is 1").Equal(game.turn.number,1);
      test.assert("second turn is second player").Equal(game.turn.player.name,game.players[1].name);
      test.finish ();
    
    test = new Test("Turns save correctly"); 
      test.assert("first turn in turn list matches number").Equal(game.turn_list[0].number,0);
      test.assert("first turn in turn list matches player").Equal(game.turn_list[0].player.name,game.players[0].name);
      test.finish();
    
  }
  
}
