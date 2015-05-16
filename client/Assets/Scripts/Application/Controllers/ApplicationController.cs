using UnityEngine;
using System.Collections;
using Wardraft.Service;
using UnityEngine.UI;
using System.Collections.Generic;

public class ApplicationController : MonoBehaviour {

  public ApplicationController current;
  public enum Scene { Login, Registration, Main }
  
  Canvas canvas;

  void Awake () {
    current = this;
    canvas = gameObject.GetComponent<Canvas>();
    DontDestroyOnLoad(canvas.gameObject);
    DontDestroyOnLoad(GameObject.Find("Networking"));
    DontDestroyOnLoad(GameObject.Find("Garbage"));
  }

  void Start () {
    setupControllers();
    registerServices();
    LoadMenu(Scene.Login);
  }
  
  void setupControllers () {
    ChatController.Start();
  }
  
  //Service Methods
  public void OnLogin (Hashtable data) {
    LoadMenu(Scene.Main);
    ChatService.JoinChatroom(1);
  }
  
  public void OnLogout (Hashtable data) {
    LoadMenu(Scene.Login);
  }
  
  public void LoadMenu (Scene scene) {
    loadMenuButtons(scene);
    Application.LoadLevel(scene.ToString() + "Menu");
    canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    LoadingWidgetController.current.Hide();
    MenuWidgetController.current.gameObject.SetActive(false);
  }
  
  void loadMenuButtons (Scene scene) {
    if (scene == Scene.Login) {
      MenuWidgetController.current.SetButtons(new List<MenuWidgetController.MenuButton>() {
        MenuWidgetController.MenuButton.Return,
        MenuWidgetController.MenuButton.Quit
      });
    }
    else if (scene == Scene.Main) {
      MenuWidgetController.current.SetButtons(new List<MenuWidgetController.MenuButton>() {
        MenuWidgetController.MenuButton.Return,
        MenuWidgetController.MenuButton.Logout,
        MenuWidgetController.MenuButton.Quit
      });
    }
  }
  
  void registerServices () {
    WebsocketService wss = WebsocketService.current;
    wss.RegisterService("account/login", OnLogin);
    wss.RegisterService("account/logout", OnLogout);
  }
  
  void OnApplicationQuit () {
    Debug.Log("Cleaning up...");
  }

}
