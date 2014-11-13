using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Aura : Ability {

	public int range;
	public List<Buff> buffs;
	
	public void apply (ActiveActor target) {
		foreach (Buff buff in buffs) {
			target.buffs.Add(buff);
		}
	}
}
