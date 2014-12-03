[System.Serializable]
public class TestAbility1 : Ability {

  public TestAbility1 () {
    range = 1;
  }

  //Deals 1 true damage to target
  public override void Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    if (aa_target != null) {
      aa_target.TakeDamage(FInt.Create(1), apply_armor: false);
    }
  }

}
