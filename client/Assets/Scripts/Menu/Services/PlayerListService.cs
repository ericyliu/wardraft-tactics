using UnityEngine;
using System.Collections;
using Newtonsoft.Json.Linq;
using Wardraft.Menu;
using System;

namespace Wardraft.Service {

  public static class PlayerListService {

    public static void RegisterServices () {
      WebsocketService.current.RegisterService("players/list", OnPlayerList);
      WebsocketService.current.RegisterService("players/login", OnPlayerLogin);
      WebsocketService.current.RegisterService("players/logout", OnPlayerLogout);
    }
    
    public static void GetPlayerList () {
      WebsocketService.current.Send("players/list");
    }
    
    public static void OnPlayerList (Hashtable data) {
      foreach (JObject playerHash in data["players"] as IEnumerable) {
        PlayerStub player = new PlayerStub(Convert.ToString(playerHash["id"]), Convert.ToString(playerHash["username"]));
        PlayerListController.AddPlayer(player);
      }
    }
    
    public static void OnPlayerLogin (Hashtable data) {
      PlayerStub player = new PlayerStub(Convert.ToString(data["id"]), Convert.ToString(data["username"]));
      PlayerListController.AddPlayer(player);
    }
    
    public static void OnPlayerLogout (Hashtable data) {
      PlayerStub player = new PlayerStub(Convert.ToString(data["id"]), Convert.ToString(data["username"]));
      PlayerListController.RemovePlayer(player);
    }

  }

}
