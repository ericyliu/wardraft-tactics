using UnityEngine;
using System.Collections.Generic;

public class PlayerList {

  List<PlayerStub> players = new List<PlayerStub>();
  
  public void AddPlayer (PlayerStub player) {
    if (players.Find(p => p.id == player.id) != null) players.Add(player);
  }
  
  public void RemovePlayer (PlayerStub player) {
    players.RemoveAll(p => p.id == player.id);
  }
  
  public List<PlayerStub> GetPlayers () {
    return players;
  }

}
