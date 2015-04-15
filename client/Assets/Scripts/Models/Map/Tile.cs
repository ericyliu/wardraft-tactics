using Wardraft.Game;

[System.Serializable]
public class Tile {

  public int        layers    = 3;
  public Actor[]    actors;
  public Terrain[]  terrains;
  public Point      position;
  public int        height;

  public Tile       from;
  public FInt       costSoFar = FInt.Create(5000);

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
    terrains[index].code = code;
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

  public Actor CreateActor (int code, string oid) {
    string type = "";
    if (ActorList.codes.ContainsKey(code)) {
      type = ActorList.codes[code];
    }
    Actor actor = ActorFactory.Create(type, Game.GetNextAid(), oid);
    actor.code = code;
    PlaceActor(actor, actor.layer);
    return actor;
  }

  public void PlaceActor (Actor actor, int layer) {
    actors[layer] = actor;
    if (actor != null) actor.position = this;
  }

  public void PlaceActiveActor (ActiveActor aa) {
    actors[aa.layer] = aa;
    aa.position = this;
  }

  public int GetLayer (Actor actor) {
    for (int i=0; i<layers; i++) {
      if (actors[i] != null && actors[i].Equals(actor)) return i;
    }
    return -1;
  }

  public bool Equals(Tile tile) {
    if (tile == null) return false;
    return tile.position == position;
  }

  public override string ToString() {
    return "Tile: (" + position.X + "," + position.Y + ")";
  }

}
