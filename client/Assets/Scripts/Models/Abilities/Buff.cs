using UnityEngine;
using System.Collections;

public class Buff {
	
	public int source;
  public ActiveActor target;
	
	public virtual void invoke () {
		
	}
  
  public virtual void devoke () {
  
  }
  
  #region operators
  public override bool Equals (object obj)
  {
    if (obj is Buff) {
      return (((Buff)obj).source == source && ((Buff)obj).target.id == target.id);
    }
    else return false;
  }
  
  public override int GetHashCode ()
  {
    int hash = 17;
    hash = hash * 23 + source.GetHashCode();
    hash = hash * 23 + target.id.GetHashCode();
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
