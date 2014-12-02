[System.Serializable]
public abstract class Actor {

  public int type_id;
	public int id;

	public Point position;
	public Point size;

	protected Actor (int aid) {
		id = aid;
    SetSize();
	}

	public void SetPosition (int x, int y) {
		position = new Point(x,y);
	}

	public void SetSize (int x = 1, int y = 1) {
		size = new Point(x,y);
	}

  #region operators
  public override bool Equals (object obj)
  {
    var actor = obj as Actor;
    return (actor != null && actor.type_id == type_id && actor.id == id);
  }

  public override int GetHashCode ()
  {
    int hash = 17;
    hash = hash * 23 + id.GetHashCode();
    hash = hash * 23 + type_id.GetHashCode();
    return hash;
  }

  public static bool operator ==(Actor a1, Actor a2) {
    return a1.Equals(a2);
  }
  public static bool operator !=(Actor a1, Actor a2) {
    return !a1.Equals(a2);
  }
  #endregion

}
