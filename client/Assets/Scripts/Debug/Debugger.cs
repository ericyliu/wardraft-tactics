﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Debugger : MonoBehaviour {

	public bool testsOn;
	public bool testMapLoader;
  public bool testGameLoader;
  public bool testDraftLoader;
  public bool testTurns;

	// Use this for initialization
	void Start () {
		if (testsOn) {
			Debug.Log("Debugging..." + DateTime.Now);
			if (testMapLoader) mapLoader();
      if (testGameLoader) gameLoader();
      if (testDraftLoader) draftLoader();
      if (testTurns) turns();
		}
	}
	
  #region mapLoader
	void mapLoader () {
		Debug.Log("- Map Loader");
    
    string mapname = "testmap";
    
    Test test;
    Map map = new Map();
    map.createMap(mapname);
    
    test = new Test("Testing corners of map are TestTerrain");
      TestTerrain test_terrain = test.add("test_terrain", new TestTerrain()) as TestTerrain;
      test.assert("upper left", (map.mapTiles[0,0].terrains[0].GetType()).Equals(test_terrain.GetType()));
      test.assert("upper right", (map.mapTiles[19,0].terrains[0].GetType()).Equals(test_terrain.GetType()));
      test.assert("lower left", (map.mapTiles[0,19].terrains[0].GetType()).Equals(test_terrain.GetType()));
      test.assert("lower right", (map.mapTiles[19,19].terrains[0].GetType()).Equals(test_terrain.GetType()));
      test.finish();
      
    test = new Test("Tiles are height 0");
      test.assert("is height 0", (map.mapTiles[0,0].height == 0));
      test.finish();   
	}
  #endregion
  
  #region gameLoader
  void gameLoader () {
    Debug.Log ("- Game Loader");
  
    int seed = 1234567890;
    string mapname = "testmap";
    List<User> users = new List<User>() {new User("user_1","user_id_1"),
                                         new User("user_2","user_id_2"),
                                         new User("user_3","user_id_3"),
                                         new User("user_4","user_id_4")};
    Game game = new Game(mapname,users,seed);
    
    Test test;
    
    test = new Test("Map is initialized");
      test.assert("is not null", game.map != null);
      test.finish();
    
    test = new Test("Players are initialized");
      test.assert("is not null", game.players != null);
      test.assert("player 1 is user_1", game.players[0].name == "user_1");
      test.finish();
      
    test = new Test("Game is now Loaded");
      test.assert("state is loaded", game.state == Enums.GameState.Loaded);
      test.finish();
  }
  #endregion
   
  #region draftLoader
  void draftLoader () {
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
      test.assert("1st same as 5th", name1 == name2);
      test.finish();
    
    game = new Game(mapname,users,seed);
    game.startDraft();
    Game game2 = new Game(mapname,users,seed);
    game2.startDraft();
    
    test = new Test("game and game2 have the same pick order");
      test.assert("same first",  game.draft.nextPick() ==  game2.draft.nextPick());
      test.assert("same second",  game.draft.nextPick() ==  game2.draft.nextPick());
      test.assert("same third",  game.draft.nextPick() ==  game2.draft.nextPick());
      test.assert("same fourth",  game.draft.nextPick() ==  game2.draft.nextPick());
      test.finish();
       
  }
  #endregion
  
  #region turns
  void turns () {
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
      test.assert("first turn is 0", game.turn.number == 0);
      test.assert("first turn is first player", game.turn.player.name == game.players[0].name);
      game.nextTurn();
      test.assert("second turn is 1", game.turn.number == 1);
      test.assert("second turn is second player", game.turn.player.name == game.players[1].name);
      test.finish ();
      
    test = new Test("Turns save correctly"); 
      test.assert("first turn in turn list matches number", game.turn_list[0].number == 0);
      test.assert("first turn in turn list matches player", game.turn_list[0].player.name == game.players[0].name);
      test.finish();
  }
  #endregion
  
}
