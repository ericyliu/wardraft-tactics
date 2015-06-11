using UnityEngine;
using System.Collections;
using System;
using Wardraft.Menu;

namespace Wardraft.Service {

  public static class RegisterService {
  
    public static void RegisterServices () {
      WebsocketService.current.RegisterService("account/register", OnRegister);
    }
    
    // Service Calls
    
    public static void Register (string username, string password) {
      Hashtable data = new Hashtable();
      data.Add("username", username);
      data.Add("password", password);
      AppData.username = username;
      AppData.password = password;
      LoadingWidgetController.current.Display("Signing Up...");
      WebsocketService.current.Send("account/register", data);
    }
    
    // End Service Calls
    
    // Callbacks
    
    public static void OnRegister (Hashtable data) {
      if (!Convert.ToBoolean(data["success"])) {
        LoadingWidgetController.current.Hide();
        Debug.LogError(data["message"]);
        RegisterController.OnError(data);
        return;
      }
      LoginController.Login(AppData.username, AppData.password);
    }
    
    // End Callbacks
  }

}
