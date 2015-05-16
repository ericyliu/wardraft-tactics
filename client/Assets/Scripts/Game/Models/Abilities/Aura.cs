using System.Collections.Generic;

public abstract class Aura : Ability {

  public List<Buff> buffs;

  public override string Invoke (ActiveActor aa_target = null, Tile tile_target = null) {
    if (aa_target != null) {
      foreach (Buff buff in buffs) {
        aa_target.AddBuff(buff);
      }
    }
    return "";
  }

}
