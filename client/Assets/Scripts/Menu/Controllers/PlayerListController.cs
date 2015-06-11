using UnityEngine;
using System.Collections;
using Wardraft.Service;

namespace Wardraft.Menu {

public static class PlayerListController {

  static PlayerList playerList = new PlayerList();

  public static void Start () {
    PlayerListService.RegisterServices();
  }
  
  public static void AddPlayer (PlayerStub player) {
    playerList.AddPlayer(player);
    if (PlayerListViewModel.current != null) {
      PlayerListViewModel.current.AddPlayer(player);
    }
  }
  
  public static void RemovePlayer (PlayerStub player) {
    playerList.RemovePlayer(player);
    if (PlayerListViewModel.current != null) {
      PlayerListViewModel.current.RemovePlayer(player);
    }
  }

}

}