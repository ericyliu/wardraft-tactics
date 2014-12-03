[System.Serializable]
public class TestAbility2 : Ability {

  public TestAbility2 () {
    range = 1;
  }

  //Deals 1 true damage to all AA on tile
  public override void Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    if (tile_target != null) {
      foreach (Actor actor in tile_target.actors) {
        if (actor != null) {
          aa_target = actor as ActiveActor;
          if (aa_target != null) {
            aa_target.TakeDamage(FInt.Create(1), apply_armor: false);
          }
        }
      }
    }
  }

}
