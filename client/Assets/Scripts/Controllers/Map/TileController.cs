using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Wardraft.Game {

  public class TileController : MonoBehaviour {
  
    public TileViewModel TVM;
    public Tile tile;
    
    public void HoverOn () {
      if (PlayerController.yourself.selected != this) {
        TVM.Mouseover();
      }
    }
    
    public void HoverExit () {
      if (PlayerController.yourself.selected != this) {
        TVM.Normal();
      }
    }
    
    public void Select (BaseEventData data) {
      if (data is PointerEventData) {
        PointerEventData.InputButton button = (data as PointerEventData).button;
        if (button == PointerEventData.InputButton.Left) {
          TVM.Select();
          PlayerController.yourself.Select(this);
          MapController.current.MVM.RemoveTileHighlights();
        }
        else if (button == PointerEventData.InputButton.Right) {
          if (PlayerController.yourself.selected is ActiveActorController) {
            (PlayerController.yourself.selected as ActiveActorController).MoveTo(tile);
          }
        }
      }
    }
    
    public void Deselect () {
      TVM.Normal();
    }
  
  }

}