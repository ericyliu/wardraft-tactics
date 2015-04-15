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
      if (actor is Unit) {
        HashSet<Tile> tiles = Map.current.TilesInUnitMoveRange(actor as Unit);
        HashSet<Tile> attackTiles = Map.current.TilesInMoveAttackRange(actor as Unit);
        MVM.DisplayAttackOptions(attackTiles);
        MVM.DisplayMovementOptions(tiles);
      }
      if (actor is Building) {
        HashSet<Tile> attackTiles = Map.current.TilesInAttackRange(actor as ActiveActor);
        MVM.DisplayAttackOptions(attackTiles);
      }
    }
    
    public void MoveActor (Actor actor, Tile tile) {
      MVM.MoveActor(actor, tile);
      DisplayOptions(actor);
    }
    
    public string CreateActor (int code, Tile tile, int oid) {
      Actor actor = tile.CreateActor(code, oid);
      MVM.CreateActor(actor, tile, actor.layer);
    }
    
    void checkDependencies () {
      if (MVM == null) Debug.LogError("Please set MapViewModel in MapController.");
    }
  
  }

}
