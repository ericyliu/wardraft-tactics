using UnityEngine;
using System.Collections.Generic;

public static partial class Tests {

  public static void Game_Tests () {

    Debug.Log ("Game Loader");

    const int seed = 1234567890;
    string mapname = "testmap";
    var users = new List<User> {
      new User("user_1","user_id_1"),
      new User("user_2","user_id_2"),
      new User("user_3","user_id_3"),
      new User("user_4","user_id_4")
    };
    var game = new Game(mapname,users,seed);

    Test test;

    test = new Test("Map is initialized");
    test.Assert("is not null").NotEqual(game.map,null);
    test.Finish();

    test = new Test("Players are initialized");
    test.Assert("is not null").NotEqual(game.players,null);
    test.Assert("player 1 is user_1").Equal(game.players[0].name,"user_1");
    test.Finish();

    test = new Test("Game is now Loaded");
    test.Assert("state is loaded").Equal(game.state,Enums.GameState.Loaded);
    test.Finish();

  }

}
