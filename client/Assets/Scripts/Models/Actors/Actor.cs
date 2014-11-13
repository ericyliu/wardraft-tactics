using UnityEngine;
using System.Collections;

[System.Serializable()]
public abstract class Actor {
	
  public int type_id;
	public int id;
  
	public Point position;
	public Point size;
	
	public Actor (int aid) {
		id = aid;
	}
	
	public void setPosition (int x, int y) {
		size = new Point(x,y);
	}
	
	public void setSize (int x, int y) {
		size = new Point(x,y);
	}
  
  #region operators
  public override bool Equals (object obj)
  {
    if (obj is Actor) {
      return (((Actor)obj).type_id == type_id && ((Actor)obj).id == id);
    }
    else return false;
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
