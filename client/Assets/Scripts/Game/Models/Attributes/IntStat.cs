﻿[System.Serializable]
public class IntStat {

	public int current, normal, max;

	public IntStat (int stat) {
		current = stat; normal = stat; max = stat;
	}

	public void Repair () { max = normal; }

}
