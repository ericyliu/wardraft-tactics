﻿using System.Collections.Generic;
using System;

public class Game {

  public Enums.GameState  state;

  public List<User>       users;
  public List<Player>     players;
  public Map              map;
  public Draft            draft;
  public Random    random;

  public List<Turn>       turn_list;
  public Turn             turn;

  public Player           winner;

  #region public

  public Game(string mapname, List<User> game_users, int game_seed) {
    state = Enums.GameState.Loading;
    random = new Random(game_seed);
    users = Utility.ShuffleUsers(game_users, ref random);
    loadMap(mapname);
    loadPlayers(users);
    state = Enums.GameState.Loaded;
  }

  public void StartDraft() {
    draft = new Draft(users, this);
    state = Enums.GameState.UnitPick;
  }

  public void StartGame() {
    state = Enums.GameState.Playing;
    turn_list = new List<Turn>();
    turn = new Turn(0, players[0]);
  }

  public bool Pause(Player player) {
    if (player.pauses > 0) {
      state = Enums.GameState.Paused;
      player.pauses--;
      return true;
    }
    return false;
  }

  public void Unpause() {
    state = Enums.GameState.Playing;
  }

  public void EndGame (Player game_winner) {
    winner = game_winner;
    state = Enums.GameState.Finished;
  }

  public void NextTurn () {
    turn_list.Add(Utility.DeepClone<Turn>(turn));
    turn = new Turn(++turn.number, players[turn.number%players.Count]);
  }

  public bool CheckGameOver () {
    var players_alive = new List<Player>();
    foreach (Player player in players) {
      if (player.alive) players_alive.Add(player);
    }
    if (players_alive.Count == 1) {
      EndGame(players_alive[0]);
      return true;
    }
    return false;
  }

  public void DoToAllActives (Action<ActiveActor> func) {
    foreach (Player player in players) {
      DoToOwnedActives(func, player);
    }
  }

  public void DoToOwnedActives (Action<ActiveActor> func, Player player) {
    foreach (ActiveActor active in player.ownedActives) {
      func(active);
    }
  }

  public void OnUnitPickDone(Draft unitPick) {
    foreach (Player player in players) {
      player.buildableUnits = unitPick.ReturnPicks(player.name);
    }
    StartGame();
  }

  #endregion

  #region private

  private void loadMap(string mapname) {
    map = new Map();
    map.CreateMap(mapname);
  }

  private void loadPlayers(List<User> user_list) {
    players = new List<Player>();
    foreach (User user in user_list) {
      players.Add (new Player(user));
    }
  }

  #endregion

}
