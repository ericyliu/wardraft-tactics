using UnityEngine;
using System.Collections;

[System.Serializable()]
public class Actor {
	
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

}
