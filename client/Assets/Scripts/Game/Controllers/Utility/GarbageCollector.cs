using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour {

  public static GarbageCollector current;
  
  void Start () {
    current = this;
  }

  void Update () {
    for (int i=0; i<transform.childCount; i++) {
      GameObject.Destroy(transform.GetChild(i).gameObject);
    }
  }
  
  public static void Collect (GameObject toDestroy) {
    toDestroy.transform.SetParent(GarbageCollector.current.transform);
  }

}
