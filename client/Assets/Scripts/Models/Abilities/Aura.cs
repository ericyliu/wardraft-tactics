using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Aura : Ability {

	public int range;
	public List<Buff> buffs;
	
	public override void invoke (ActiveActor target) {
		foreach (Buff buff in buffs) {
			target.addBuff(buff);
		}
	}
  
}
