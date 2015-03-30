using UnityEngine;
using System.Collections;
using Wardraft.Game;

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
      Vector3 prevPos = transform.position;
      transform.Translate(dir * Time.deltaTime * speed, Space.World);
      RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward);
      foreach (RaycastHit hit in hits) {
        if (hit.collider.transform.GetComponentInChildren<TileViewModel>() != null) {
          return;
        }
      }
      transform.position = prevPos;
    }
  
  }

}