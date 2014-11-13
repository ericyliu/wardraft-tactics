using UnityEngine;
using System.Collections;

[System.Serializable()]
public class Cast : Command {

  ActiveActor source;
  ActiveActor target;
  Tile tile_target;
  Ability ability;
  
  public Cast (ActiveActor cast_source, Ability cast_ability,
               ActiveActor cast_target, Tile cast_tile_target) {
    source = cast_source;
    ability = cast_ability;
    target = cast_target;
    tile_target = cast_tile_target;
  }
  
  public override void invoke ()
  {
    source.cast(ability, target:this.target,tile_target:this.tile_target);
  }

	
}
