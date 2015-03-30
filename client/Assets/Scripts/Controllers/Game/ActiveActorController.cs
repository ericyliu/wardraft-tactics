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
  
  }

}