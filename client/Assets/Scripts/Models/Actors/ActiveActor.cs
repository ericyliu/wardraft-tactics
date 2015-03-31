using System.Collections.Generic;

public abstract class ActiveActor : Actor {

  public string ownerID;

  public Attributes attributes;

  public List<Buff> buffs;
  public List<Ability> abilities;

  public bool canAttack = true;
  public bool canMove = true;

  public Enums.ActiveActorState state;
  
  public bool selected = false;

  protected ActiveActor (int aid, string oid) : base(aid) {
    ownerID = oid;
    state = Enums.ActiveActorState.Alive;
    buffs = new List<Buff>();
    abilities = new List<Ability>();
  }

  #region commands
  public void Attack (ActiveActor target) {
    target.TakeDamage(attributes.damage.max);
    canAttack = false;
  }

  public void Move (List<Tile> path) {
    for (int i=2; i<=path.Count; i++) {
      moveTo(position, path[path.Count-i]);
    }
  }

  public void Cast (Ability ability, ActiveActor aa_target = null, Tile tile_target = null) {
    ability.Invoke(aa_target: aa_target, tile_target: tile_target);
  }

  public void Die () {
    state = Enums.ActiveActorState.Dead;
  }
  
  public void Select () {
    selected = true;
  }
  
  public void Deselect () {
    selected = false;
  }
  #endregion

  public void AddHealth (FInt heal_for) {
    attributes.health.current = attributes.health.current + heal_for;
    if (attributes.health.current > attributes.health.max) attributes.health.current = attributes.health.max;
  }

  public void TakeDamage (FInt damage_taken, bool apply_armor = true) {
    if (apply_armor) damage_taken = damage_taken - attributes.armor.max;
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
    canAttack = true;
    canMove = true;
  }

  public void AssignStats (Attributes aa_attributes) {
    attributes = aa_attributes;
  }

  public bool ValidMove (Tile tile) {
    return (tile.terrains[layer].passable &&
            tile.actors[layer] != null);
  }

  public void AddAbilityByID (int id) {
    Ability ability = AbilityFactory.Create(AbilityList.abilities[id]);
    ability.aa_source = this;
    abilities.Add(ability);
  }

  private void moveTo (Tile start, Tile end) {
    start.PlaceActor(null, layer);
    end.PlaceActor(this, layer);
  }

}
