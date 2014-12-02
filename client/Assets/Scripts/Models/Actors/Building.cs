public abstract class Building : ActiveActor {

  public Unit[] buildList;

  protected Building (int aid, int oid) : base(aid,oid) {
    canAttack = false;
    canMove = false;
  }

}
