using UnityEngine;
using System.Collections;

[System.Serializable()]
public class FIntStat {

	public FInt current, normal, max;
	
	public FIntStat (FInt current_stat, FInt normal_stat, FInt max_stat) {
		current = current_stat; normal = normal_stat; max = max_stat;
	}
	
	public void repair () { max = normal; }
	
}
