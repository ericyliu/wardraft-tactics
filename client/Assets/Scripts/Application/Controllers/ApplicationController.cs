using UnityEngine;
using System.Collections;

public class ApplicationController : MonoBehaviour {

  enum Scene { Login, Registration, Lobby }

  void Awake () {
    DontDestroyOnLoad(gameObject);
  }

  void Start () {
    LoadMenu(Scene.Login);
  }
  
  void LoadMenu (Scene scene) {
    Application.LoadLevel(scene.ToString() + "Menu");
  }

}
