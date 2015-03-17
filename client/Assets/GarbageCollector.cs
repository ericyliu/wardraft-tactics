using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour {

  void Update () {
    for (int i=0; i<transform.childCount; i++) {
      GameObject.Destroy(transform.GetChild(i));
    }
  }

}
