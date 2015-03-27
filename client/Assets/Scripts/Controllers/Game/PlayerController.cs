using UnityEngine;
using System.Collections;

namespace Wardraft.Game {

  public class PlayerController : MonoBehaviour {
  
    public Player player;
    public ActiveActorController selected;
    
    public static PlayerController yourself;
    
    public void Select (ActiveActorController toSelect) {
      if (toSelect != selected && selected != null) {
        selected.Deselect();
      }
      selected = toSelect;
    }
  
  }

}
