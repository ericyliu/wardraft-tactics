using System.Collections.Generic;

public class Aura : Ability {

	public int range;
	public List<Buff> buffs;

	public override void Invoke (ActiveActor target) {
		foreach (Buff buff in buffs) {
			target.AddBuff(buff);
		}
	}

}
