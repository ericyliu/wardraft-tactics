using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Unit : ActiveActor {

	public FIntStat speed, mana;

	public Unit(int aid, int oid) : base (aid,oid) {
		
	}
  
  public override void resetStats () {
    base.resetStats();
    speed.max = speed.normal;
    mana.max = mana.normal;
  }
  
  public override void assignStats (int health_stat = 0, int damage_stat = 0,
                                    int armor_stat = 0, int build_stat = 0,
                                    int range_stat = 0, int speed_stat = 0,
                                    int mana_stat = 0) {
    base.assignStats();
    speed = new FIntStat(FInt.Create(1));
    mana = new FIntStat(FInt.Create(1));
  }

}
