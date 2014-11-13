using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Map {

	public Tile[,] mapTiles;
	
	#region public
	
	public Map () {
		
	}
  
	public void createMap (string mapname) {
    JsonMapData data = readData(mapname);
    setUpTiles(data.width, data.height);
    setUpTerrain(data);
	}
	
	public List<Tile> tilesInRange (Tile tile, int range) {
		List<Tile> tiles = new List<Tile>();
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
	
	public List<Tile> tilesInUnitMoveRange (Unit unit) {
		return tilesInMoveRange(getTile(unit.position),
                            unit.layer,
                            unit.speed.current);
	}
  
  public List<Tile> tilesInAttackRange (ActiveActor active_actor) {
     List<Tile> tiles = new List<Tile>();
     tiles = tilesInRange(getTile(active_actor.position),
                          active_actor.attackRange.max);
     return tiles;
  }
  
  public void buildPath (Tile start, Tile finish, ref List<Tile> path) {
    path.Add(finish);
    if (!start.Equals(finish)) {
      buildPath(start, finish.from, ref path);
    }
  }
  
  public Tile getTile(Point position) {
    return mapTiles[position.X, position.Y];
  }
	
	#endregion
	
	#region private
	
  private JsonMapData readData (string mapname) {
    string path = AppValues.ROOTDIR + "Assets/Resources/Maps/" + mapname + ".json";
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
        mapTiles[x,y].setTerrain(i, tiles[j]);
        if (i==1) {
          mapTiles[x,y].setHeight(data.properties.height);
        }
      }
    }
  }
  
	private void applyTileEffects () {
		foreach (Tile tile in mapTiles) {
			if (tile.actors != null && tile.actors.Length > 0) {
				tile.applyEffects();
			}
		}
	}
	
	private List<Tile> tilesInMoveRange (Tile start, int layer,
                                       FInt movement_points) {
		List<Tile> tiles = new List<Tile>();
		List<Tile> one_away = tilesInRange(start, 1);
		foreach (Tile tile in one_away) {
			Terrain terrain = tile.terrains[layer];
			if (terrain.passable && terrain.speedCost < movement_points) {
				tile.from = start;
        tiles.Add(tile);
				tiles.AddRange(
					tilesInMoveRange(tile,
                           layer, 
                           movement_points-terrain.speedCost)
				);
			}
		}
		return tiles;
	}
	
	private bool checkTilesInRange (Tile tile1, Tile tile2, int range) {
		int distance_x = Mathf.Abs(tile2.position.X - tile1.position.X);
		int distance_y = Mathf.Abs(tile2.position.Y - tile1.position.Y);
		return range >= distance_x + distance_y;
	}
	
	#endregion
	
	
}
