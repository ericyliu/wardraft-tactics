using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Map {

  public Tile[,] mapTiles;
  public static Map map;

  #region public

  public void CreateMap (string mapname) {
    JsonMapData data = readData(mapname);
    setUpTiles(data.width, data.height);
    setUpTerrain(data);
    map = this;
  }

  public List<Tile> TilesInRange (Tile tile, int range) {
    var tiles = new List<Tile>();
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

  public List<Tile> TilesInUnitMoveRange (Unit unit) {
    return tilesInMoveRange(unit.position,
                            unit.layer,
                            unit.attributes.speed.current,
                            FInt.Create(0));
  }

  public List<Tile> TilesInAttackRange (ActiveActor active_actor) {
     List<Tile> tiles = TilesInRange(active_actor.position,
                          active_actor.attributes.attackRange.max);
     return tiles;
  }

  public void BuildPath (Tile start, Tile finish, ref List<Tile> path) {
    path.Add(finish);
    if (!start.Equals(finish)) {
      BuildPath(start, finish.from, ref path);
    }
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

  #endregion

  #region private

  private JsonMapData readData (string mapname) {
    string path = UnityEngine.Application.dataPath + "/Resources/Maps/" + mapname + ".json";
    System.Console.WriteLine (path);
    string data_text = System.IO.File.ReadAllText(path);
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
    for (int i=0; i<data.layers.Count; i++) {
      List<int> tiles = data.layers[i].data;
      for (int j=0; j<tiles.Count; j++) {
        int x = j%data.width;
        int y = j/data.width;
        mapTiles[x,y].SetTerrain(i, tiles[j]);
        if (i==1) {
          mapTiles[x,y].SetHeight(data.properties.height);
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

  private List<Tile> tilesInMoveRange (Tile start, int layer,
                                       FInt movement_left,
                                       FInt movement_used) {
    var tiles = new List<Tile>();
    List<Tile> one_away = TilesInRange(start, 1);
    foreach (Tile tile in one_away) {
      Terrain terrain = tile.terrains[layer];
      if (terrain.passable &&
          movement_left - terrain.speedCost >= FInt.Create(0) &&
          movement_used + terrain.speedCost < tile.costSoFar) {
        tile.from = start;
        tile.costSoFar = movement_used + terrain.speedCost;
        tiles.Add(tile);
        tiles.AddRange(
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
