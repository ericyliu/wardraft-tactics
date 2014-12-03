[System.Serializable]
public abstract class Ability {

  public ActiveActor  aa_source;
  public Tile         tile_source;
  public int          range;

  public virtual void Invoke (ActiveActor aa_target = null, Tile tile_target = null) {}

}
