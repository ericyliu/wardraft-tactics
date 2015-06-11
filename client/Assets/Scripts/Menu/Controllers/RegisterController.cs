using UnityEngine;
using System.Collections;
using System;
using Wardraft.App;
using Wardraft.Service;

namespace Wardraft.Menu {

  public static class RegisterController {
  
    public static void Start () {
      RegisterService.RegisterServices();
    }
    
    public static void Register (string username, string password) {
      RegisterService.Register(username, password);
    }
    
    public static void OnError (Hashtable error) {
      if (RegisterViewModel.current != null) RegisterViewModel.current.DisplayStatus(Convert.ToString(error["message"]), Status.Error);
    }
  }

}