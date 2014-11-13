using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ActiveActor : Actor {

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
    target.takeDamage(damage.max);
  }
  
  public void move (List<Tile> path) {
    for (int i=1; i<=path.Count; i++) {
      moveTo(path[path.Count-i]);
    }
  }
  
  public void cast (Ability ability, ActiveActor target = null, Tile tile_target = null) {
    if (target != null) ability.invoke(target);
    else if (tile_target != null) ability.invoke(tile_target);
    else ability.invoke();
  }
  
  public void die () {
    state = Enums.ActiveActorState.Dead;
  }
  #endregion
  
  public void addHealth (FInt heal_for) {
    health.current = health.current + heal_for;
    if (health.current > health.max) health.current = health.max;
  }
  
  public void takeDamage (FInt damage_taken) {
    damage_taken = damage_taken - armor.max;
    if (damage_taken < FInt.Create(1)) damage_taken = FInt.Create(1);
    health.current = health.current - damage_taken;
    if (health.current <= 0) {
      die();
    }
  }
  
  public void addBuff (Buff buff) {
    if (!buffs.Contains(buff)) {
      buffs.Add(buff);
      buff.target = this;
      buff.invoke();
    }
  }
  
  public void removeBuff (Buff buff) {
    buffs.Remove(buff);
    buff.devoke();
  }
  
	public void applyAllBuffs () {
    resetStats();
		foreach (Buff buff in buffs) {
			buff.invoke();
		}
	}
  
  public virtual void resetStats () {
    health.max = health.normal;
    damage.max = damage.normal;
    armor.max = armor.normal;
    attackRange.max = attackRange.current;
  }
  
  public virtual void assignStats (int health_stat = 0, int damage_stat = 0, int armor_stat = 0,
                                   int build_stat = 0, int range_stat = 0, int speed_stat = 0,
                                   int mana_stat = 0) {
    health = new FIntStat(FInt.Create(health_stat));
    damage = new FIntStat(FInt.Create(damage_stat));
    armor = new FIntStat(FInt.Create(armor_stat));
    buildTime = new FIntStat(FInt.Create(build_stat));
    attackRange = new IntStat(range_stat);
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
