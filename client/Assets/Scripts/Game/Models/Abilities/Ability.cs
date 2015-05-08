[System.Serializable]
public abstract class Ability {

  public ActiveActor        aa_source;
  public Tile               tile_source;
  public int                range;
  public int                aoeRange;
  public int                code;
  public GameEnums.SpellTarget  target;
  public bool               isDamageDealt = true;
  
  public bool               canTargetDead = false;

  public virtual string Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    return "";
  }

}
