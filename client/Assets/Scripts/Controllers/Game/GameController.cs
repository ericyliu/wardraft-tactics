using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wardraft.Test;

namespace Wardraft.Game {

  public class GameController : MonoBehaviour {
  
    public string MapName;
    public ResourceLoader RL;
    public MapController MC;
    GameObject playersContainer;
  
    Game game;
  
    // MAIN FUNCTION TO START
    void Start () {
      game = new Game();
      playersContainer = GameObject.Find("Players");
      checkDependencies();
    }
    
    void Update () {
      if (game.state == Enums.GameState.Unstarted) {
        if (RL.done) {
          startGame();
        }
      }
    }
  
    void startGame () {
      Debug.Log("Starting Game...");
      //TODO: pass in user data and game seed from prior scene
      List<User> users = TestingData.users;
      int seed = 1234567890;
      AppData.UserID = "1";
      //END TODO
      GameData.PlayerID = AppData.UserID;
      game.LoadGame(MapName, users, seed);
      createPlayers(game);
      MC.LoadMap();
      setupUserData();
    }
    
    void createPlayers (Game game) {
      foreach (Player player in game.players) {
        GameObject playerObj = new GameObject();
        PlayerController PC = playerObj.AddComponent<PlayerController>();
        PC.player = player;
        playerObj.transform.SetParent(playersContainer.transform);
        playerObj.name = "Player#" + player.id;
        if (player.id == GameData.PlayerID) {
          PlayerController.yourself = PC;
        }
      }
    }
   
    void setupUserData () {
      GameData.PlayerID = AppData.UserID;
    }
     
    void checkDependencies () {
      if (RL == null) Debug.LogError("Please set ResourceLoader in GameController");
      if (MC == null) Debug.LogError("Please set MapController in GameController");
    }
  
  }

}
