using UnityEngine;
using Wardraft.Game;

[System.Serializable]
public class TestAbility2 : Ability {

  int damage = 1;

  public TestAbility2 () {
    range = 1;
    target = Enums.SpellTarget.Tile;
  }

  //Deals 1 true damage to all AA on tile
  public override string Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    foreach (Actor actor in tile_target.actors) {
      if (actor != null && actor is Unit) {
        aa_target = actor as ActiveActor;
        if (aa_target.state == Enums.ActiveActorState.Alive) {
          aa_target.TakeDamage(FInt.Create(damage), apply_armor: false);
          string name = string.Format("Actor:{0}#{1}", aa_target.code, aa_target.id);
          ActiveActorController aac = GameObject.Find(name).GetComponentInChildren<ActiveActorController>();
          aac.TakeDamage();
        }
      }
    }
    return string.Format("{0} dealt {1} true damage to all units on tile {2}. (TestAbility2)", ActorList.codes[aa_source.code], damage, tile_target.position);
  }

}
