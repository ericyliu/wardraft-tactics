using UnityEngine;
using System.Collections;

public class Building : ActiveActor {

	public Unit[] buildList;

	public Building (int aid, int oid) : base(aid,oid) {
		base.CanAttack = false;
		base.CanMove = false;
	}
	
}
