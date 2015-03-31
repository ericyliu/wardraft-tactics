using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Wardraft.UI;

namespace Wardraft.Controls {

  public class InputRouter : MonoBehaviour {
    
    CameraController CC;
    
    void Start () {
      CC = GameObject.Find("MainCamera").GetComponent<CameraController>();
      checkStartValues();
    }
    
    void Update () {
      checkCameraInputs();
    }
    
    //Input Functions
    void checkCameraInputs () {
      float x=0, y=0;
      if (Input.GetKey(KeySettings.Bindings[Actions.CameraDown])) y--;
      if (Input.GetKey(KeySettings.Bindings[Actions.CameraUp])) y++;
      if (Input.GetKey(KeySettings.Bindings[Actions.CameraLeft])) x--;
      if (Input.GetKey(KeySettings.Bindings[Actions.CameraRight])) x++;
      
      if (Input.mousePosition.y < CC.mouseCameraGutter) y--;
      if (Input.mousePosition.y > Screen.height - CC.mouseCameraGutter) y++;
      if (Input.mousePosition.x < CC.mouseCameraGutter) x--;
      if (Input.mousePosition.x > Screen.width - CC.mouseCameraGutter) x++;
      
      CC.SetVelocity(x, y);
    }
    
    //Debug functions
    void checkStartValues () {
      if (CC == null) Debug.LogError("CameraController not found");
    }
  
  }

}