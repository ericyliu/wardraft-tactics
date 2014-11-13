using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[System.Serializable()]
public class Tile {

	public int layers = 3;
	public Actor[] actors;
	public Terrain[] terrains;
	public Point position;
  public int height;
	
	public Tile from;
	
	public Tile (Point tile_pos) {
	  actors = new Actor[layers];
		terrains = new Terrain[layers];
		position = tile_pos;
	}
	
  public void setTerrain (int index, int code) {
    string type = "";
    try { type = Terrains.codes[code]; }
    catch {}
    terrains[index] = TerrainFactory.create(type);
  }
  
  public void setHeight (int tile_height) {
    height = tile_height;
  }
  
	public void applyEffects () {
		for (int i=0; i<layers; i++) {
			foreach (Aura aura in terrains[i].auras) {
				if (actors[i] is ActiveActor) {
					aura.apply(actors[i] as ActiveActor);
				}
			}
		}
	}
  
  public bool Equals(Tile tile) {
    return tile.position == position;
  }
	
}
