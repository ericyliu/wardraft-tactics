using System.Collections.Generic;

public abstract class ActiveActor : Actor {

  public int ownerID, layer = 1;

  public Attributes attributes;

  public List<Buff> buffs;
  public List<Ability> abilities;

  public bool canAttack = true;
  public bool canMove = true;

  public Enums.ActiveActorState state;

  protected ActiveActor (int aid, int oid) : base(aid) {
    ownerID = oid;
    state = Enums.ActiveActorState.Alive;
  }

  #region commands
  public void Attack (ActiveActor target) {
    target.TakeDamage(attributes.damage.max);
  }

  public void Move (List<Tile> path) {
    for (int i=2; i<=path.Count; i++) {
      moveTo(position, path[path.Count-i]);
    }
  }

  public void Cast (Ability ability, ActiveActor target = null, Tile tile_target = null) {
    if (target != null) ability.Invoke(target);
    else if (tile_target != null) ability.Invoke(tile_target);
    else ability.Invoke();
  }

  public void Die () {
    state = Enums.ActiveActorState.Dead;
  }
  #endregion

  public void AddHealth (FInt heal_for) {
    attributes.health.current = attributes.health.current + heal_for;
    if (attributes.health.current > attributes.health.max) attributes.health.current = attributes.health.max;
  }

  public void TakeDamage (FInt damage_taken) {
    damage_taken = damage_taken - attributes.armor.max;
    if (damage_taken < FInt.Create(1)) damage_taken = FInt.Create(1);
    attributes.health.current = attributes.health.current - damage_taken;
    if (attributes.health.current <= 0) {
      Die();
    }
  }

  public void AddBuff (Buff buff) {
    if (!buffs.Contains(buff)) {
      buffs.Add(buff);
      buff.target = this;
      buff.Invoke();
    }
  }

  public void RemoveBuff (Buff buff) {
    buffs.Remove(buff);
    buff.Devoke();
  }

  public void ApplyAllBuffs () {
    ResetStats();
    foreach (Buff buff in buffs) {
      buff.Invoke();
    }
  }

  public virtual void ResetStats () {
    attributes.health.Repair();
    attributes.damage.Repair();
    attributes.armor.Repair();
    attributes.attackRange.Repair();
  }

  public void AssignStats (Attributes aa_attributes) {
    attributes = aa_attributes;
  }

  public bool ValidMove (Tile tile) {
    return (tile.terrains[layer].passable &&
            tile.actors[layer] != null);
  }

  private void moveTo (Tile start, Tile end) {
    start.PlaceActor(null, layer);
    end.PlaceActor(this, layer);
  }

}
