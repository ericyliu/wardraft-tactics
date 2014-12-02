[System.Serializable]
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

  public void SetTerrain (int index, int code) {
    string type = "";
    if (Terrains.codes.ContainsKey(code)) {
      type = Terrains.codes[code];
    }
    terrains[index] = TerrainFactory.Create(type);
  }

  public void SetHeight (int tile_height) {
    height = tile_height;
  }

  public void ApplyEffects () {
    for (int i=0; i<layers; i++) {
      foreach (Aura aura in terrains[i].auras) {
        var aa = actors[i] as ActiveActor;
        if (aa != null) aura.Invoke(aa);
      }
    }
  }

  public bool Equals(Tile tile) {
    return tile.position == position;
  }

}
