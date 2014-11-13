using UnityEngine;
using System.Collections;

[System.Serializable()]
public class Attack : Command {

  ActiveActor source;
  ActiveActor target;

  public Attack (ActiveActor attack_source, ActiveActor attack_target) {
    source = attack_source;
    target = attack_target;
  }

  public override void invoke () {
    source.attack(target);
  }
	
}
