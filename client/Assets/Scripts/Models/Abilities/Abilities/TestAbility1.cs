[System.Serializable]
public class TestAbility1 : Ability {

  int damage = 1;

  public TestAbility1 () {
    range = 1;
    target = Enums.SpellTarget.Target;
  }

  //Deals 1 true damage to target
  public override string Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    if (aa_target != null) {
      aa_target.TakeDamage(FInt.Create(damage), apply_armor: false);
    }
    return string.Format("{0} dealt {1} true damage to {2}. (TestAbility1)", ActorList.codes[aa_source.code], damage, ActorList.codes[aa_target.code]);
  }

}
