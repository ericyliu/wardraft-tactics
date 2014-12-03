using UnityEngine;
using System.Collections.Generic;

public static partial class Tests {

  public static void Draft_Tests () {
    Debug.Log ("Draft Loader\n");

    const int seed = 1234567890;
    string mapname = "testmap";
    var users = new List<User> {
      new User("user_1","user_id_1"),
      new User("user_2","user_id_2"),
      new User("user_3","user_id_3"),
      new User("user_4","user_id_4")
    };
    var game = new Game(mapname,users,seed);
    game.StartDraft();

    Test test;

    test = new Test("pick order loops back");
      string name1 = game.draft.NextPick();
      game.draft.NextPick();
      game.draft.NextPick();
      game.draft.NextPick();
      string name2 = game.draft.NextPick();
      test.Assert("1st same as 5th").Equal(name1,name2);
      test.Finish();


    test = new Test("game and game2 have the same pick order");
      game = new Game(mapname,users,seed);
      game.StartDraft();
      var game2 = new Game(mapname,users,seed);
      game2.StartDraft();
      test.Assert("same first").Equal(game.draft.NextPick(),game2.draft.NextPick());
      test.Assert("same second").Equal(game.draft.NextPick(),game2.draft.NextPick());
      test.Assert("same third").Equal(game.draft.NextPick(),game2.draft.NextPick());
      test.Assert("same fourth").Equal(game.draft.NextPick(),game2.draft.NextPick());
      test.Finish();
  }

}

