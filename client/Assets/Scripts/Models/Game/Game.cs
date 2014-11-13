using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game {

  public Enums.GameState state;

  public List<User> users;
  public List<Player> players;
  public Map map;
  public Draft draft;
  public System.Random random;
  
  public List<Turn> turn_list;
  public Turn turn;
  
  public Player winner;
  
  #region public
  
  public Game(string mapname, List<User> game_users, int game_seed) {
    state = Enums.GameState.Loading;
    random = new System.Random(game_seed);
    users = Utility.shuffleUsers(game_users, ref random);
    loadMap(mapname);
    loadPlayers(users);
    state = Enums.GameState.Loaded;
  }
  
  public void startDraft() {
    draft = new Draft(users, this);
    state = Enums.GameState.UnitPick;
  }
  
  public void startGame() {
    state = Enums.GameState.Playing;
    turn_list = new List<Turn>();
    turn = new Turn(0, players[0]);
  }
  
  public bool pause(Player player) {
    if (player.pauses > 0) {
      state = Enums.GameState.Paused;
      player.pauses--;
      return true;
    }
    return false;
  }
  
  public void unpause() {
    state = Enums.GameState.Playing;
  }
  
  public void endGame (Player game_winner) {
    winner = game_winner;
    state = Enums.GameState.Finished;
  }
  
  public void nextTurn () {
    turn_list.Add(Utility.DeepClone<Turn>(turn));
    turn = new Turn(++turn.number, players[turn.number%players.Count]);
  }
  
  public bool checkGameOver () {
    List<Player> players_alive = new List<Player>();
    foreach (Player player in players) {
      if (player.alive) players_alive.Add(player);
    }
    if (players_alive.Count == 1) {
      endGame(players_alive[0]);
      return true;
    }
    else return false;
  }
  
  public void doToAllActives (Action<ActiveActor> func) {
    foreach (Player player in players) {
      doToOwnedActives(func, player);
    }
  }
  
  public void doToOwnedActives (Action<ActiveActor> func, Player player) {
    foreach (ActiveActor active in player.ownedActives) {
      func(active);
    }
  }
  
  public void pickDone(Draft unitPick) {
    foreach (Player player in players) {
      player.buildableUnits = unitPick.returnPicks(player.name);
    }
    startGame();
  }

  #endregion

  #region private

  private void loadMap(string mapname) {
    map = new Map();
    map.createMap(mapname);
  }
  
  private void loadPlayers(List<User> users) {
    players = new List<Player>();
    foreach (User user in users) {
      players.Add (new Player(user));
    }
  }
  
  #endregion
  
}
