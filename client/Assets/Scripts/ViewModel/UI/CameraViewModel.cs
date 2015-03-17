using UnityEngine;
using System.Collections;

namespace Wardraft.UI {

  public class CameraViewModel : MonoBehaviour {
  
    Transform     container;
    public float  speed   = 20f;
    
    void Start () {
      container = transform.parent;
    }
    
    public void MoveForward () { move(container.forward); }
    
    public void MoveBack () { move(container.forward * -1f); }
    
    public void MoveLeft () { move(container.right * -1f); }
    
    public void MoveRight () { move(container.right); }
    
    void move (Vector3 dir) {
      transform.Translate(dir * Time.deltaTime * speed, Space.World);
    }
  
  }

}