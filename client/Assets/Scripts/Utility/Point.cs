public struct Point
{
	public int X, Y;
	
	public Point(int px, int py)
	{
		X = px;
		Y = py;
	}
  
  #region operators
  public override bool Equals (object obj)
  {
    if (obj is Point) {
      return (((Point)obj).X == X && ((Point)obj).Y == Y);
    }
    else return false;
  }
  
  public override int GetHashCode ()
  {
    int hash = 17;
    hash = hash * 23 + X.GetHashCode();
    hash = hash * 23 + Y.GetHashCode();
    return hash;
  }
  
  public static bool operator ==(Point p1, Point p2) {
    return p1.Equals(p2);
  }
  public static bool operator !=(Point p1, Point p2) {
    return !p1.Equals(p2);
  }
  #endregion
}
