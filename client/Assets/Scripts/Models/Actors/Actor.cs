using UnityEngine;
using System.Collections;

[System.Serializable()]
public class Actor {
	
	public int id;
	public Point position;
	public Point size;
  
  public Enums.AnimationState animation;
	
	public Actor (int aid) {
		id = aid;
    animation = Enums.AnimationState.Standing;
	}
	
	public void setPosition (int x, int y) {
		size = new Point(x,y);
	}
	
	public void setSize (int x, int y) {
		size = new Point(x,y);
	}

}
