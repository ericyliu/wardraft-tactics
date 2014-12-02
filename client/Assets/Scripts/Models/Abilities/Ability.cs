[System.Serializable]
public abstract class Ability {

  public ActiveActor aa_source;
  public Tile tile_source;

  public virtual void Invoke () {}

  public virtual void Invoke (ActiveActor target) {}

  public virtual void Invoke (Tile target) {}

}
