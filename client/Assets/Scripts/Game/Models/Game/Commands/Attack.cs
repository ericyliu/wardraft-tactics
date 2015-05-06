[System.Serializable]
public class Attack : Command {

  readonly ActiveActor source;
  readonly ActiveActor target;

  public Attack (ActiveActor attack_source, ActiveActor attack_target) {
    source = attack_source;
    target = attack_target;
  }

  public override void Invoke () {
    source.Attack(target);
  }

}
