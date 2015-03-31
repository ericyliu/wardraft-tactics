using UnityEngine;
using System.Collections.Generic;

namespace Wardraft.Game {

  public class MapViewModel : MonoBehaviour {
  
    public ResourceLoader RL;
    List<GameObject> highlightedTiles;

    public void LoadMap () {
      Debug.Log("Loading Map");
      clearMap();
      createTiles();
      highlightedTiles = new List<GameObject>();
    }
    
    public void MoveActor (Actor actor, Tile tile) {
      GameObject tileObject = GameObject.Find("Tile:" + tile.position.X + "," + tile.position.Y);
      GameObject actorObject = GameObject.Find("Actor:" + actor.code + "#" + actor.id);
      if (tileObject == null || actorObject == null) {
        Debug.LogError("Could not find tile and actor GameObjects.");
      }
      actorObject.transform.SetParent(tileObject.transform,true);
    }
    
    public void DisplayAttackOptions (HashSet<Tile> tiles) {
      foreach (Tile tile in tiles) {
        GameObject tileObject = GameObject.Find("Tile:" + tile.position.X + "," + tile.position.Y);
        GameObject outline = Instantiate(ResourceLoader.current.misc["tileAttackOutline"]) as GameObject;
        outline.name = "AttackOutline";
        outline.transform.SetParent(tileObject.transform,false);
        highlightedTiles.Add(tileObject);
      }
    }
    
    public void DisplayMovementOptions (HashSet<Tile> tiles) {
      foreach (Tile tile in tiles) {
        GameObject tileObject = GameObject.Find("Tile:" + tile.position.X + "," + tile.position.Y);
        GameObject outline = Instantiate(ResourceLoader.current.misc["tileMovementOutline"]) as GameObject;
        outline.name = "MovementOutline";
        outline.transform.SetParent(tileObject.transform,false);
        highlightedTiles.Add(tileObject);
        Transform attackOutline = tileObject.transform.FindChild("AttackOutline");
        if (attackOutline != null) Object.DestroyImmediate(attackOutline.gameObject);
      }
    }
    
    public void RemoveTileHighlights () {
      foreach (GameObject tile in highlightedTiles) {
        Transform attackOutline = tile.transform.FindChild("AttackOutline");
        Transform movementOutline = tile.transform.FindChild("MovementOutline"); 
        if (attackOutline != null) Object.DestroyImmediate(attackOutline.gameObject);
        if (movementOutline != null) Object.DestroyImmediate(movementOutline.gameObject);
      }
      highlightedTiles.Clear();
    }
    
    void Start () {
      checkDependencies();
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
        for (int j=0; j<tiles.GetLength(1); j++) {
          createTile(tiles[i,j], i, j);
        }
      }
    }
    
    void createTile (Tile tile, int x, int y) {
      GameObject tileObject = new GameObject();
      tileObject.name = "Tile:" + x + "," + y;
      tileObject.transform.position = new Vector3(x, .5f + (tile.height/2f), y);
      tileObject.transform.SetParent(transform,false);
      for (int i=0; i<tile.layers; i++) {
        createTerrain(tile.terrains[i], tileObject.transform, i, tile);
        createActor(tile.actors[i], tileObject.transform, i);
      }
    }
    
    void createTerrain (Terrain terrain, Transform tileTransform, int layer, Tile tile) {
      if (layer != 1) return; //underground
      if (terrain != null) {
        GameObject terrainObject = Instantiate(RL.terrains[terrain.code]) as GameObject;
        terrainObject.name = "Terrain:" + terrain.code;
        terrainObject.transform.SetParent(tileTransform,false);
        terrainObject.transform.localPosition = new Vector3(0,layer-1f,0);
        terrainObject.GetComponent<TileController>().tile = tile;
      }
    }
    
    void createActor (Actor actor, Transform tile, int layer) {
      if (layer == 0) return; //underground
      if (actor != null) {
        GameObject actorObject = Instantiate(RL.actors[actor.code]) as GameObject;
        actorObject.name = "Actor:" + actor.code + "#" + actor.id;
        actorObject.transform.SetParent(tile,false);
        actorObject.transform.localPosition = new Vector3(0,.5f,0);
        
        if (actor is ActiveActor) {
          ActiveActorController AAC = actorObject.GetComponent<ActiveActorController>();
          AAC.AA = actor as ActiveActor;
        }
        
      }
    }
    
    void checkDependencies () {
      if (RL == null) Debug.LogError("Please set ResourceLoader in MapViewModel");
    }
  
  }

}