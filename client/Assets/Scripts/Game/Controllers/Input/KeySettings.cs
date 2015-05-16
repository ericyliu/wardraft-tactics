using System.Collections.Generic;
using UnityEngine;

namespace Wardraft.Controls {

  public class KeySettings {
  
    public static Dictionary<Actions, KeyCode> Bindings = new Dictionary<Actions, KeyCode>() {
      // Camera Manipulation
      { Actions.CameraUp, KeyCode.UpArrow },
      { Actions.CameraDown, KeyCode.DownArrow },
      { Actions.CameraLeft, KeyCode.LeftArrow },
      { Actions.CameraRight, KeyCode.RightArrow }
    };
  
  }

}