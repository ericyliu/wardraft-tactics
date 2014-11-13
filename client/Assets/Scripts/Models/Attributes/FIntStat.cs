using UnityEngine;
using System.Collections;

[System.Serializable()]
public class FIntStat {

	public FInt current, normal, max;
	
	public FIntStat (FInt stat) {
		current = stat; normal = stat; max = stat;
	}
	
	public void repair () { max = normal; }
	
}
