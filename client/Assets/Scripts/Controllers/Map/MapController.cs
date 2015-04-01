using UnityEngine;
using System.Collections.Generic;

namespace Wardraft.Game {

  public class MapController : MonoBehaviour {
  
    public MapViewModel MVM;
    public Map map;
      
    public static MapController current;
  
  
    void Start () {
      checkDependencies();
      current = this;
    }
  
    public void LoadMap (Map map) {
      this.map = map;
      MVM.LoadMap();
    }
    
    public void DisplayOptions (Actor actor) {
     MVM.RemoveTileHighlights();
      if (actor is ActiveActor) {
        HashSet<Tile> tiles = Map.current.TilesInUnitMoveRange(actor as Unit);
        HashSet<Tile> attackTiles;
        if (actor is Unit) attackTiles = Map.current.TilesInMoveAttackRange(actor as Unit);
        else attackTiles = Map.current.TilesInAttackRange(actor as ActiveActor);
        MVM.DisplayAttackOptions(attackTiles);
        MVM.DisplayMovementOptions(tiles);
      }
    }
    
    public void MoveActor (Actor actor, Tile tile) {
      MVM.MoveActor(actor, tile);
      DisplayOptions(actor);
    }
    
    void checkDependencies () {
      if (MVM == null) Debug.LogError("Please set MapViewModel in MapController.");
    }
  
  }

}
