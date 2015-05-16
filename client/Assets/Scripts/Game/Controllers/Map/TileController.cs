using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Wardraft.Game {

  public class TileController : MonoBehaviour {
  
    public TileViewModel TVM;
    public Tile tile;
    
    public static GameObject GetTileObject (Tile tile) {
      return GameObject.Find("Tile:" + tile.position.X + "," + tile.position.Y);
    }
    
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
          if (PlayerController.yourself.isAbilityPrimed) {
            PlayerController.yourself.UseAbility(null, tile);
            return;
          }
          TVM.Select();
          PlayerController.yourself.Select(this);
          MapController.current.MVM.RemoveTileHighlights();
        }
        else if (button == PointerEventData.InputButton.Right) {
          if (PlayerController.yourself.isAbilityPrimed) {
            PlayerController.yourself.UnprimeAbility();
            return;
          }
          if (PlayerController.yourself.selected is ActiveActorController) {
            ActiveActorController aac = PlayerController.yourself.selected as ActiveActorController;
            if (aac.AA is Unit) {
              aac.MoveTo(tile);
            }
          }
        }
      }
    }
    
    public void Deselect () {
      TVM.Normal();
    }
  
  }

}