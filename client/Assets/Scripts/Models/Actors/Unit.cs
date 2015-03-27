public abstract class Unit : ActiveActor {

  protected Unit(int aid, string oid) : base (aid,oid) {

  }

  public override void ResetStats () {
    base.ResetStats();
    attributes.speed.max = attributes.speed.normal;
    attributes.mana.max = attributes.mana.normal;
  }

}
