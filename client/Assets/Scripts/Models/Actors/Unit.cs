using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : ActiveActor {

	public FIntStat speed, mana;

	public Unit(int aid, int oid) : base (aid,oid) {
		
	}
  
  public override void resetStats () {
    base.resetStats();
    speed.max = speed.normal;
    mana.max = mana.normal;
  }

}
