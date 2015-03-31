using UnityEngine;
using System.Collections;
using Wardraft.UI;

namespace Wardraft.Game {

  public class PlayerController : MonoBehaviour {
  
    public Player player;
    public System.Object selected;
    public GameUIController GUIC;
    
    public static PlayerController yourself;
    
    void Start () {
      GUIC = GameObject.Find("UI").GetComponent<GameUIController>();
    }
    
    public void Select (System.Object toSelect) {
      if (toSelect != selected && selected != null) {
        if (selected is ActiveActorController) (selected as ActiveActorController).Deselect();
        if (selected is TileController) (selected as TileController).Deselect();
      }
      selected = toSelect;
      GUIC.ShowSelectedInfo(toSelect);
    }
  
  }

}
