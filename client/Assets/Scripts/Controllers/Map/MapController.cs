using UnityEngine;
using System.Collections.Generic;

namespace Wardraft.Game {

  public class MapController : MonoBehaviour {
  
    public MapViewModel MVM;
    
    public static MapController current;
  
    void Start () {
      checkDependencies();
      current = this;
    }
  
    public void LoadMap () {
      MVM.LoadMap();
    }
    
    public void DisplayOptions (Actor actor) {
     MVM.RemoveTileHighlights();
      if (actor is Unit) {
        HashSet<Tile> tiles = Map.current.TilesInUnitMoveRange(actor as Unit);
        Tile savePos = actor.position;
        HashSet<Tile> attackTiles = new HashSet<Tile>();
        foreach (Tile tile in tiles) {
          actor.position = tile;
          attackTiles.UnionWith(Map.current.TilesInAttackRange(actor as ActiveActor));
        }
        MVM.DisplayAttackOptions(attackTiles);
        MVM.DisplayMovementOptions(tiles);
        actor.position = savePos;
      }
    }
    
    void checkDependencies () {
      if (MVM == null) Debug.LogError("Please set MapViewModel in MapController.");
    }
  
  }

}
