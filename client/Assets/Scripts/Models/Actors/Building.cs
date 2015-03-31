public abstract class Building : ActiveActor {

  public Unit[] buildList;

  protected Building (int aid, string oid) : base(aid,oid) {
    canAttack = false;
    canMove = false;
  }

}
