using UnityEngine;
using System.Collections;

[System.Serializable()]
public abstract class Command {

	public abstract void invoke ();
}
