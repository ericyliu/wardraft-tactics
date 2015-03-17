using UnityEngine;
using System.Collections;

namespace Wardraft.Game {

  public class MapViewModel : MonoBehaviour {
  
    ResourceLoader RL;

    public void LoadMap () {
      Tile tile = Map.current.mapTiles[0,0];
      for (int i=0; i<tile.layers; i++) {
        
      }
    }
    
    public void MoveActor (Actor actor, Tile tile) {
      GameObject tileObject = GameObject.Find("Tile:" + tile.position.X + "," + tile.position.Y);
      GameObject actorObject = GameObject.Find("Actor:" + actor.code + "#" + actor.id);
      if (tileObject == null || actorObject == null) {
        Debug.LogError("Could not find tile and actor GameObjects.");
      }
      actorObject.transform.SetParent(actorObject.transform,false);
    }
    
    void Start () {
      RL = GameObject.Find("Game").GetComponent<ResourceLoader>();
    }
    
    void clearMap () {
      Transform garbage = GameObject.Find("Garbage").transform;
      for (int i=0; i<transform.childCount; i++) {
        transform.GetChild(i).SetParent(garbage);
      }
    }
    
    void createTiles () {
      Tile[,] tiles = Map.current.mapTiles;
      for (int i=0; i<tiles.GetLength(0); i++) {
        for (int j=0; j<tiles.GetLength(1); i++) { 
          createTile(tiles[i,j], i, j);
        }
      }
    }
    
    void createTile (Tile tile, int x, int y) {
      GameObject tileObject = new GameObject();
      tileObject.name = "Tile:" + x + "," + y;
      tileObject.transform.SetParent(transform,false);
      tileObject.transform.position = new Vector3(x, .5f + tile.height, y);
      for (int i=0; i<tile.layers; i++) {
        createTerrain(tile.terrains[i], tileObject.transform, i);
        createActor(tile.actors[i], tileObject.transform, i);
      }
    }
    
    void createTerrain (Terrain terrain, Transform tile, int layer) {
      if (layer == 0) return; //underground
      if (terrain != null) {
        GameObject terrainObject = Instantiate(RL.terrains[terrain.code]) as GameObject;
        terrainObject.name = "Terrain:" + terrain.code + "#";
        terrainObject.transform.SetParent(tile,false);
        terrainObject.transform.position = new Vector3(0,layer-1f,0);
      }
    }
    
    void createActor (Actor actor, Transform tile, int layer) {
      if (layer == 0) return; //underground
      if (actor != null) {
        GameObject actorObject = Instantiate(RL.actors[actor.code]) as GameObject;
        actorObject.name = "Actor:" + actor.code + "#" + actor.id;
        actorObject.transform.SetParent(tile,false);
        actorObject.transform.position = new Vector3(0,layer-.5f,0);
      }
    }
  
  }

}