using UnityEngine;
using System.Collections;

[System.Serializable()]
public abstract class Ability {

  public ActiveActor aa_source;
  public Tile tile_source;
  
  public virtual void invoke () {}
  
  public virtual void invoke (ActiveActor target) {}
  
  public virtual void invoke (Tile target) {}
	
}
