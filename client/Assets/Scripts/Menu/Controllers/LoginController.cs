using UnityEngine;
using System.Collections;
using Wardraft.Service;

namespace Wardraft.Menu {

  public static class LoginController {
  
    public static void Start () {
      ChatService.RegisterServices();
    }
  
    public static void Login (string username, string password) {
      LoginService.Login(username, password);
    }
    
    public static void Logout () {
      LoginService.Logout();
    }
  
  }

}