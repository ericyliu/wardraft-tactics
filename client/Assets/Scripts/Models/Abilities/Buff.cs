[System.Serializable]
public abstract class Buff {

	public int source;
  public int id;
  public ActiveActor target;

  public bool stack;

  protected Buff (int buff_source, int buff_id) {
    source = buff_source;
    id = buff_id;
  }

	public virtual void Invoke () {
		//does something to target
	}

  public virtual void Devoke () {
    //does something to target
  }

  #region operators
  public override bool Equals (object obj)
  {
    if (obj == null) return false;
    var buff = obj as Buff;
    if (buff != null) {
      if (stack && buff.stack)
        return (buff.source == source  && (buff.id == id));
      return buff.id == id;
    }
    return false;
  }

  public override int GetHashCode ()
  {
    int hash = 17;
    if (stack) hash = hash * 23 + source.GetHashCode();
    hash = hash * 23 + id.GetHashCode();
    return hash;
  }

  public static bool operator ==(Buff b1, Buff b2) {
    return b1.Equals(b2);
  }
  public static bool operator !=(Buff b1, Buff b2) {
    return !b1.Equals(b2);
  }
  #endregion

}
