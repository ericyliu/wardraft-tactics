using UnityEngine;
using System.Collections;

namespace Wardraft.Game {

  public class TileViewModel : MonoBehaviour {
    
    public void Normal () {
      destroyOutlines();
    }
    
    public void Mouseover () {
      destroyOutlines();
      GameObject outline = Instantiate(ResourceLoader.current.misc["tileMouseoverOutline"]) as GameObject;
      outline.name = "MouseoverOutline";
      outline.transform.SetParent(transform,false);
    }
    
    public void Select () {
      destroyOutlines();
      GameObject outline = Instantiate(ResourceLoader.current.misc["tileSelectionOutline"]) as GameObject;
      outline.name = "SelectionOutline";
      outline.transform.SetParent(transform,false);
    }
    
    void destroyOutlines () {
      Transform selectionOutline = transform.FindChild("SelectionOutline");
      Transform mouseoverOutline = transform.FindChild("MouseoverOutline");
      if (selectionOutline != null) Object.Destroy(selectionOutline.gameObject);
      if (mouseoverOutline != null) Object.Destroy(mouseoverOutline.gameObject);
    }
  
  }

}