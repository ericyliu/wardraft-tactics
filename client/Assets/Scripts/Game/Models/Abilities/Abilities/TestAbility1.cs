using UnityEngine;
using Wardraft.Game;

[System.Serializable]
public class TestAbility1 : Ability {

  int damage = 10;

  public TestAbility1 () {
    range = 1;
    target = GameEnums.SpellTarget.Target;
  }

  //Deals 1 true damage to target
  public override string Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    if (aa_target != null) {
      if (aa_target.state == GameEnums.ActiveActorState.Alive) {
        aa_target.TakeDamage(FInt.Create(damage), apply_armor: false);
        string name = string.Format("Actor:{0}#{1}", aa_target.code, aa_target.id);
        ActiveActorController aac = GameObject.Find(name).GetComponentInChildren<ActiveActorController>();
        aac.TakeDamage();
        return string.Format("{0} dealt {1} true damage to {2}. (TestAbility1)", ActorList.codes[aa_source.code], damage, ActorList.codes[aa_target.code]);
      }
      else return "Cannot cast on a dead target.";
    }
    return "error";
  }

}
