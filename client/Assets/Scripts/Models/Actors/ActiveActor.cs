using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveActor : Actor {

	public int ownerID, layer;
	
	public FIntStat health, damage, armor, buildTime;
	
	public IntStat attackRange;
	
	public List<Buff> buffs;
	public List<Ability> abilities;
	
	public bool CanAttack = true;
	public bool CanMove = true;
  
  public Enums.ActiveActorState state;
		
	public ActiveActor (int aid, int oid) : base(aid) {
		ownerID = oid;
    state = Enums.ActiveActorState.Alive;
	}
	
  #region commands
  public void attack (ActiveActor target) {
    
  }
  
  public void move (List<Tile> path) {
    for (int i=1; i<=path.Count; i++) {
      moveTo(path[path.Count-i]);
    }
  }
  
  public void die () {
    state = Enums.ActiveActorState.Dead;
  }
  #endregion
  
  public void addBuff (Buff buff) {
    buffs.Add(buff);
    buff.target = this;
    buff.invoke();
  }
  
  public void removeBuff (Buff buff) {
    buffs.Remove(buff);
    buff.devoke();
  }
  
	public void applyAllBuffs () {
		foreach (Buff buff in buffs) {
			buff.invoke();
		}
	}
  
  public bool validMove (Tile tile) {
    if (tile.actors[layer] != null) return false; //occupied
    if (tile.terrains[layer].passable) return false; //passable
    return true;
  }
  
  private void moveTo (Tile tile) {
    base.position = tile.position;
  }
	
}
