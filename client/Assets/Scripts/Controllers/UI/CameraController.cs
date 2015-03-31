using UnityEngine;
using System.Collections;

namespace Wardraft.UI {

  public class CameraController : MonoBehaviour {
  
    CameraViewModel CVM;
    Vector2         velocity;
    public float mouseCameraGutter = 20f;
    
    void Start () {
      CVM = gameObject.GetComponent<CameraViewModel>();
      velocity = new Vector2(0f,0f);
    }
    
    public void SetVelocity (float x, float y) {
      velocity = new Vector2(x,y);
    }
    
    void Update () {
      move();
    }
    
    //Move according to velocity
    void move () {
      if (velocity.x < 0) CVM.MoveLeft();
      if (velocity.x > 0) CVM.MoveRight();
      if (velocity.y < 0) CVM.MoveBack();
      if (velocity.y > 0) CVM.MoveForward();
    }
  
  }

}
