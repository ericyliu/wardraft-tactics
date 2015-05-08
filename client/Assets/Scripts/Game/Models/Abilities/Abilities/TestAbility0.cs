using UnityEngine;

[System.Serializable]
public class TestAbility0 : Ability {

  int damage = 1;

  public TestAbility0 () {
    range = 0;
    target = GameEnums.SpellTarget.Self;
    isDamageDealt = false;
  }

  //Heals self for 1
  public override string Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    aa_source.AddHealth(FInt.Create(damage));
    return string.Format("{0} has healed itself for {1} health. (TestAbility0)", ActorList.codes[aa_source.code], damage);
  }

}
