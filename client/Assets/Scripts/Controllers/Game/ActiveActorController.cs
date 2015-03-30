using UnityEngine;
using System.Collections.Generic;

namespace Wardraft.Game {

  public class ActiveActorController : MonoBehaviour {
  
    public ActiveActorVM AAVM;
    public ActiveActor AA;
    
    public void HoverOn () {
      if (PlayerController.yourself.selected != this) {
        if (AA.ownerID != GameData.PlayerID) AAVM.MouseoverEnemy();
        else AAVM.MouseoverOwn();
      }
    }
    
    public void HoverExit () {
      if (PlayerController.yourself.selected != this) {
        AAVM.Normal();
      }
    }
    
    public void Select () {
      if (AA.ownerID != GameData.PlayerID) AAVM.SelectEnemy();
      else AAVM.SelectOwn();
      AA.Select();
      PlayerController.yourself.Select(this);
      if (AA is Unit) MapController.current.DisplayOptions(AA);
    }
    
    public void Deselect () {
      AAVM.Normal();
      AA.Deselect();
    }
    
    public void MoveTo (Tile tile) {
      if (AA.canMove) {
        if (Map.current.TilesInUnitMoveRange(AA as Unit).Contains(tile)) {
          List<Tile> path = new List<Tile>();
          Map.current.BuildPath(AA.position, tile, ref path);
          AA.Move(path);
          MapController.current.MoveActor(AA, tile); 
        }
        else {
          Debug.Log("Unit cannot move to that tile");
        }
      }
    }
  
  }

}