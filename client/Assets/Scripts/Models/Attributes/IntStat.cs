using UnityEngine;
using System.Collections;

[System.Serializable()]
public class IntStat {

	public int current, normal, max;
	
	public IntStat (int current_stat, int normal_stat, int max_stat) {
		current = current_stat; normal = normal_stat; max = max_stat;
	}
	
	public void repair () { max = normal; }
	
}
