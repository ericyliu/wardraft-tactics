using UnityEngine;
using System.Collections;

public class TestUnit : Unit {

	public TestUnit (int aid, int oid) : base(aid, oid) {
    type_id = 0;
    assignStats(health_stat:1, damage_stat:1, armor_stat: 1,
                build_stat: 1, range_stat: 1, speed_stat: 1,
                mana_stat: 1);
  }
	
}
