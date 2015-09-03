using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
using Wardraft.Game;

public class Map {

  public Tile[,] mapTiles;
  public static Map current;
  public int layers = 3;

  #region public

  public void CreateMap (string mapname) {
    Debug.Log("Loading Map Data...");
    JsonMapData data = readData(mapname);
    current = this;
    setUpTiles(data.width, data.height);
    setUpTerrain(data);
    setUpNeutrals(data);
    setUpPlayersActors(data);
  }

  public HashSet<Tile> TilesInRange (Tile tile, int range) {
    var tiles = new HashSet<Tile>();
    Point position = tile.position;
    for (int x=position.X-range; x<=position.X+range; x++) {
      for (int y=position.Y-range; y<=position.Y+range; y++) {
        if (x >= 0 && x < mapTiles.GetLength(0) &&
          y >= 0 && y < mapTiles.GetLength(1)) {
          if (checkTilesInRange(tile, mapTiles[x,y], range)) {
            tiles.Add(mapTiles[x,y]);
          }
        }
      }
    }
    return tiles;
  }

  public HashSet<Tile> TilesInUnitMoveRange (Unit unit) {
    foreach (Tile tile in mapTiles) {
      tile.costSoFar = FInt.Create(5000);
    }
    return tilesInMoveRange(unit.position,
                            unit.layer,
                            unit.attributes.speed.current,
                            FInt.Create(0));
  }

  public HashSet<Tile> TilesInAttackRange (ActiveActor active_actor) {
    HashSet<Tile> tiles = TilesInRange(active_actor.position,
                          active_actor.attributes.attackRange.max);
    return tiles;
  }

  public void BuildPath (Tile start, Tile finish, ref List<Tile> path) {
    path.Add(finish);
    if (!start.Equals(finish)) {
      BuildPath(start, finish.from, ref path);
    }
  }
  
  public HashSet<Tile> TilesInMoveAttackRange (Unit unit) {
    HashSet<Tile> tiles = new HashSet<Tile>();
    HashSet<Tile> moveTiles = TilesInUnitMoveRange(unit);
    foreach (Tile tile in moveTiles) {
      Tile prevPos = unit.position;
      unit.position = tile;
      tiles.UnionWith(TilesInAttackRange(unit));
      unit.position = prevPos;
    }
    return tiles;
  }
  
  public bool IsWithinAttackRange (ActiveActor source, ActiveActor target) {
    HashSet<Tile> tiles = new HashSet<Tile>();
    if (source is Unit) tiles = TilesInMoveAttackRange(source as Unit);
    else tiles = TilesInAttackRange(source);
    foreach (Tile tile in tiles) {
      if (tile.GetLayer(target) != -1) return true;
    }
    return false;
  }

  public Tile GetTile(Point position) {
    return mapTiles[position.X, position.Y];
  }

  public void PlaceActor(Point position, Actor actor, int layer = 1) {
    GetTile(position).PlaceActor(actor, layer);
  }

  public void PlaceActiveActor(Point position, ActiveActor aa) {
    GetTile(position).PlaceActiveActor(aa);
  }
  
  public Tile findOpenAdjacentTile(Tile tile) {
    for (int x=tile.position.X-1; x<=tile.position.X+1; x++) {
      for (int y=tile.position.Y-1; y<=tile.position.Y+1; y++) {
        Tile t = GetTile(new Point(x,y));
        if (t != null) return t;
      }
    }
    return null;
  }

  #endregion

  #region private

  private JsonMapData readData (string mapname) {
    string data_text = (Resources.Load("Maps/" + mapname) as TextAsset).text;
    JsonMapData data = JsonConvert.DeserializeObject<JsonMapData>(data_text);
    return data;
  }

  private void setUpTiles (int width, int height) {
    mapTiles = new Tile[width,height];
    for (int x=0; x<width; x++) {
      for (int y=0; y<width; y++) {
        mapTiles[x,y] = new Tile(new Point(x,y));
      }
    }
  }

  private void setUpTerrain (JsonMapData data) {
    for (int i=0; i<layers; i++) {
      List<int> tiles = data.layers[i].data;
      for (int j=0; j<tiles.Count; j++) {
        int x = j%data.width;
        int y = j/data.width;
        mapTiles[x,y].SetTerrain(i, tiles[j]);
        if (i==1) {
          mapTiles[x,y].SetHeight(data.layers[(int)GameEnums.Layers.Heightmap].data[j]);
        }
      }
    }
  }
  
  private void setUpNeutrals (JsonMapData data) {
    List<int> neutrals = data.layers[(int)GameEnums.Layers.Neutral].data;
    for (int j=0; j<neutrals.Count; j++) {
      if (neutrals[j] != 0) {
        int x = j%data.width;
        int y = j%data.width;
        mapTiles[x,y].CreateActor(neutrals[j], "neutral");
      }
    }
  }
  
  private void setUpPlayersActors (JsonMapData data) {
    int numLayers = data.layers.Count;
    int numPlayers = numLayers - (int)GameEnums.Layers.Players;
    for (int i=0; i<numPlayers; i++) {
      List<int> playersActors = data.layers[i+(int)GameEnums.Layers.Players].data;
      for (int j=0; j<playersActors.Count; j++) {
        if (playersActors[j] != 0) {
          int x = j%data.width;
          int y = j%data.width;
          Actor actor = mapTiles[x,y].CreateActor(playersActors[j], Game.current.players[i].id);
          Game.current.players[i].ownedActives.Add(actor as ActiveActor);
        }
      }
    }
  }

  private void applyTileEffects () {
    foreach (Tile tile in mapTiles) {
      if (tile.actors != null && tile.actors.Length > 0) {
        tile.ApplyEffects();
      }
    }
  }

  private HashSet<Tile> tilesInMoveRange (Tile start, int layer,
                                       FInt movement_left,
                                       FInt movement_used) {
    var tiles = new HashSet<Tile>();
    HashSet<Tile> one_away = TilesInRange(start, 1);
    foreach (Tile tile in one_away) {
      Wardraft.Game.Terrain terrain = tile.terrains[layer];
      if (terrain.passable &&
          movement_left - terrain.speedCost >= FInt.Create(0) &&
          movement_used + terrain.speedCost < tile.costSoFar) {
        tile.from = start;
        tile.costSoFar = movement_used + terrain.speedCost;
        tiles.Add(tile);
        tiles.UnionWith(
          tilesInMoveRange(tile,
                           layer,
                           movement_left - terrain.speedCost,
                           movement_used + terrain.speedCost)
        );
      }
    }
    return tiles;
  }

  private bool checkTilesInRange (Tile tile1, Tile tile2, int range) {
    if (tile1.Equals(tile2)) return false;
    int distance_x = Mathf.Abs(tile2.position.X - tile1.position.X);
    int distance_y = Mathf.Abs(tile2.position.Y - tile1.position.Y);
    return range >= distance_x + distance_y;
  }

  #endregion


}
