using UnityEngine;
using System.Collections.Generic;
using Wardraft.Game;

public static partial class Tests {

  public static void Turn_Tests () {

    Debug.Log("Turns");

    const int seed = 1234567890;
    string mapname = "testmap";
    var users = new List<User> {
      new User("user_1","user_id_1"),
      new User("user_2","user_id_2"),
      new User("user_3","user_id_3"),
      new User("user_4","user_id_4")
    };
    var game = new Game();
    game.LoadGame(mapname,users,seed);

    Test test;

    test = new Test("Turns increment correctly");
      game.StartGame();
      test.Assert("first turn is 0").Equal(game.turn.number,0);
      test.Assert("first turn is first player").Equal(game.turn.playerId,game.players[0].name);
      game.EndTurn();
      test.Assert("second turn is 1").Equal(game.turn.number,1);
      test.Assert("second turn is second player").Equal(game.turn.playerId,game.players[1].name);
      test.Finish ();

    test = new Test("Turns save correctly");
      test.Assert("first turn in turn list matches number").Equal(game.turn_list[0].number,0);
      test.Assert("first turn in turn list matches player").Equal(game.turn_list[0].playerId,game.players[0].name);
      test.Finish();

  }

}
