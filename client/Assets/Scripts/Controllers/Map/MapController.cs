using UnityEngine;
using System.Collections;

namespace Wardraft.Game {

  public class MapController : MonoBehaviour {
  
    public MapViewModel MVM;
  
    void Start () {
      checkDependencies();
    }
  
    public void LoadMap () {
      MVM.LoadMap();
    }
    
    void checkDependencies () {
      if (MVM == null) Debug.LogError("Please set MapViewModel in MapController.");
    }
  
  }

}
