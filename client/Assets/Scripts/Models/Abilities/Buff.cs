using UnityEngine;
using System.Collections;

[System.Serializable()]
public class Buff {
	
	public int source;
  public int id;
  public ActiveActor target;
  
  public bool stack = false;
  
  public Buff (int buff_source) {
    source = buff_source;
  }
	
	public virtual void invoke () {
		//does something to target
	}
  
  public virtual void devoke () {
    //does something to target
  }
  
  #region operators
  public override bool Equals (object obj)
  {
    if (obj is Buff) {
      if (stack && ((Buff)obj).stack)
        return (((Buff)obj).source == source  && ((Buff)obj).id == id);
      else
        return (((Buff)obj).id == id); 
    }
    else return false;
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
