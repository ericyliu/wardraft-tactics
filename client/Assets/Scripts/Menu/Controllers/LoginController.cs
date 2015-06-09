using UnityEngine;
using System.Collections;
using Wardraft.Service;
using Wardraft.App;
using System;

namespace Wardraft.Menu {

  public static class LoginController {
  
    public static void Start () {
      LoginService.RegisterServices();
    }
  
    public static void Login (string username, string password) {
      LoginService.Login(username, password);
    }
    
    public static void Logout () {
      LoginService.Logout();
    }
    
    public static void OnError (Hashtable error) {
      Debug.Log("hi");
      Debug.Log(LoginViewModel.Current);
      if (LoginViewModel.Current != null) LoginViewModel.Current.DisplayStatus(Convert.ToString(error["message"]), Status.Error);
    }
  
  }

}