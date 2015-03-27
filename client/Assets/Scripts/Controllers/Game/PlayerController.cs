using UnityEngine;
using System.Collections;
using Wardraft.UI;

namespace Wardraft.Game {

  public class PlayerController : MonoBehaviour {
  
    public Player player;
    public ActiveActorController selected;
    public GameUIController GUIC;
    
    public static PlayerController yourself;
    
    void Start () {
      GUIC = GameObject.Find("UI").GetComponent<GameUIController>();
    }
    
    public void Select (ActiveActorController toSelect) {
      if (toSelect != selected && selected != null) {
        selected.Deselect();
      }
      selected = toSelect;
      GUIC.ShowSelectedInfo(toSelect.AA);
    }
  
  }

}
