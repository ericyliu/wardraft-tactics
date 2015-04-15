public abstract class Building : ActiveActor {

  public int[] buildList = new int[0];
  public Tile rallyPoint;

  protected Building (int aid, string oid) : base(aid,oid) {
    canAttack = false;
    canMove = false;
  }
  
  public void SetDefaultRallyPoint () {
    rallyPoint = Map.current.findOpenAdjacentTile(position);
  }

}
