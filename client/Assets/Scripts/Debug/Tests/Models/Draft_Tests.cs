using UnityEngine;
using System.Collections.Generic;

public static partial class Tests {
  
  public static void Draft_Tests () {
    Debug.Log ("- Draft Loader");
    
    int seed = 1234567890;
    string mapname = "testmap";
    List<User> users = new List<User>() {new User("user_1","user_id_1"),
      new User("user_2","user_id_2"),
      new User("user_3","user_id_3"),
      new User("user_4","user_id_4")};
    Game game = new Game(mapname,users,seed);
    game.startDraft();
    
    Test test;
    
    test = new Test("pick order loops back");
      string name1 = game.draft.nextPick();
      game.draft.nextPick();
      game.draft.nextPick();
      game.draft.nextPick();
      string name2 = game.draft.nextPick();
      test.assert("1st same as 5th").Equal(name1,name2);
      test.finish();
    
    
    test = new Test("game and game2 have the same pick order");
      game = new Game(mapname,users,seed);
      game.startDraft();
      Game game2 = new Game(mapname,users,seed);
      game2.startDraft();
      test.assert("same first").Equal(game.draft.nextPick(),game2.draft.nextPick());
      test.assert("same second").Equal(game.draft.nextPick(),game2.draft.nextPick());
      test.assert("same third").Equal(game.draft.nextPick(),game2.draft.nextPick());
      test.assert("same fourth").Equal(game.draft.nextPick(),game2.draft.nextPick());
      test.finish();    
  }
  
}

