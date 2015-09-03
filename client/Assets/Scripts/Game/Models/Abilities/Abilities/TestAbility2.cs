using UnityEngine;
using Wardraft.Game;

[System.Serializable]
public class TestAbility2 : Ability {

  int damage = 1;

  public TestAbility2 () {
    name = "Damage Tile";
    range = 1;
    target = GameEnums.SpellTarget.Tile;
  }

  //Deals 1 true damage to all AA on tile
  public override string Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    foreach (Actor actor in tile_target.actors) {
      if (actor != null && actor is Unit) {
        aa_target = actor as ActiveActor;
        if (aa_target.state == GameEnums.ActiveActorState.Alive) {
          aa_target.TakeDamage(FInt.Create(damage), apply_armor: false);
          string targetName = string.Format("Actor:{0}#{1}", aa_target.code, aa_target.id);
          ActiveActorController aac = GameObject.Find(targetName).GetComponentInChildren<ActiveActorController>();
          aac.TakeDamage();
        }
      }
    }
    return string.Format("{0} dealt {1} true damage to all units on tile {2}.", ActorList.codes[aa_source.code], damage, tile_target.position);
  }

}
