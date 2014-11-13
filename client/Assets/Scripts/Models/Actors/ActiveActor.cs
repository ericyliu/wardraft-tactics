using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveActor : Actor {

	public int ownerID, layer;
	
	public FIntStat health, attack, armor, buildTime;
	
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
	
	public void applyBuffs () {
		foreach (Buff buff in buffs) {
			buff.invoke();
		}
	}
  
  public void pathTo (List<Tile> path) {
    for (int i=1; i<=path.Count; i++) {
      moveTo(path[path.Count-i]);
    }
  }
  
  public bool validMove (Tile tile) {
    if (tile.actors[layer] != null) return false; //occupied
    if (tile.terrains[layer].passable) return false; //passable
    return true;
  }
  
  public void die () {
    state = Enums.ActiveActorState.Dead;
    base.animation = Enums.AnimationState.Dying;
  }
  
  private void moveTo (Tile tile) {
    base.position = tile.position;
  }
	
}
