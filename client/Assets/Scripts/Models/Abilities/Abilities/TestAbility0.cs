[System.Serializable]
public class TestAbility0 : Ability {

  public TestAbility0 () {
    range = 0;
    target = Enums.SpellTarget.Self;
  }

  //Heals self for 1
  public override void Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    aa_source.AddHealth(FInt.Create(1));
  }

}
