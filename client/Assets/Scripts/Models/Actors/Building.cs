public abstract class Building : ActiveActor {

  public int[] buildList = new int[0];

  protected Building (int aid, string oid) : base(aid,oid) {
    canAttack = false;
    canMove = false;
  }

}
