using UnityEngine;
using System.Collections;
using Wardraft.Service;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace Wardraft.Menu {

  public class ApplicationController : MonoBehaviour {
  
    public static ApplicationController Current;
    
    Canvas canvas;
  
    void Awake () {
      Current = this;
      canvas = gameObject.GetComponent<Canvas>();
      DontDestroyOnLoad(canvas.gameObject);
      DontDestroyOnLoad(GameObject.Find("Networking"));
      DontDestroyOnLoad(GameObject.Find("Garbage"));
    }
  
    void Start () {
      setupControllers();
      LoadMenu(Enums.Scene.Login);
    }
    
    void setupControllers () {
      ChatController.Start();
      LoginController.Start();
      RegisterController.Start();
      PlayerListController.Start();
    }
    
    public void LoadMenu (Enums.Scene scene) {
      loadMenuButtons(scene);
      Application.LoadLevel(scene.ToString() + "Menu");
      canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
      LoadingWidgetController.current.Hide();
      MenuWidgetController.current.gameObject.SetActive(false);
    }
    
    void loadMenuButtons (Enums.Scene scene) {
      if (scene == Enums.Scene.Login) {
        MenuWidgetController.current.SetButtons(new List<MenuWidgetController.MenuButton>() {
          MenuWidgetController.MenuButton.Return,
          MenuWidgetController.MenuButton.Quit
        });
      }
      else if (scene == Enums.Scene.Main) {
        MenuWidgetController.current.SetButtons(new List<MenuWidgetController.MenuButton>() {
          MenuWidgetController.MenuButton.Return,
          MenuWidgetController.MenuButton.Logout,
          MenuWidgetController.MenuButton.Quit
        });
      }
    }
    
    void OnApplicationQuit () {
      Debug.Log("Cleaning up...");
    }
  
  }
 
}
