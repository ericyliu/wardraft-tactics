using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Wardraft.Test;
using Wardraft.UI;

namespace Wardraft.Game {

  public class GameController : MonoBehaviour {
  
    public ResourceLoader RL;
    public MapController MC;
    public GameUIController GUIC; 
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
          loadGame();
        }
      }
    }
    
    public Player GetMe () {
      return game.players.Find(x => x.id == GameData.PlayerID);
    }
  
    void loadGame () {
      Debug.Log("Starting Game...");
      //TODO: pass in user data and game seed from prior scene
      List<User> users = TestingData.users;
      int seed = 1234567890;
      AppData.UserID = "1";
      //END TODO
      GameData.PlayerID = AppData.UserID;
      game.LoadGame(MapData.current.MapName, users, seed);
      createPlayers(game);
      MC.LoadMap();
      setupUserData();
      //TODO: move this to after draft
      startGame();
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
    
    void startGame () {
      game.StartGame();
      GUIC.UpdateCurrentPlayerInfo(GetMe());
    }
     
    void checkDependencies () {
      if (RL == null) Debug.LogError("Please set ResourceLoader in GameController");
      if (MC == null) Debug.LogError("Please set MapController in GameController");
      if (GUIC == null) Debug.LogError("Please set GameUIController in GameController");
    }
  
  }

}
