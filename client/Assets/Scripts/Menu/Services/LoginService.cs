using UnityEngine;
using System.Collections;
using System;
using Wardraft.Menu;

namespace Wardraft.Service {

  public static class LoginService {
  
    public static void RegisterServices () {
      WebsocketService.current.RegisterService("account/login", OnLogin);
      WebsocketService.current.RegisterService("account/logout", OnLogout);
    }
    
    // Service Calls
    
    public static void Login (string username, string password) {
      Hashtable data = new Hashtable();
      data.Add("username", username);
      data.Add("password", password);
      LoadingWidgetController.current.Display("Logging In");
      WebsocketService.current.Send("account/login", data);
    }
    
    public static void Logout () {
      LoadingWidgetController.current.Display("Logging Out");
      WebsocketService.current.Send("account/logout", null);
    }
    
    // End Service Calls
    
    // Callbacks
    
    public static void OnLogin (Hashtable data) {
      if (!Convert.ToBoolean(data["success"])) {
        LoadingWidgetController.current.Hide();
        Debug.LogError(data["message"]);
        return;
      }
      ApplicationController.Current.LoadMenu(Enums.Scene.Main);
      ChatService.JoinChatroom(1);
    }
    
    public static void OnLogout (Hashtable data) {
      if (!Convert.ToBoolean(data["success"])) {
        LoadingWidgetController.current.Hide();
        Debug.LogError(data["message"]);
        return;
      }
      ApplicationController.Current.LoadMenu(Enums.Scene.Login);
    }
    
    // End Callbacks
  
  }

}