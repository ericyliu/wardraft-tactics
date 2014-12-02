[System.Serializable]
public class Cast : Command {

  readonly ActiveActor source;
  readonly ActiveActor target;
  readonly Tile tile_target;
  readonly Ability ability;

  public Cast (ActiveActor cast_source, Ability cast_ability,
               ActiveActor cast_target, Tile cast_tile_target) {
    source = cast_source;
    ability = cast_ability;
    target = cast_target;
    tile_target = cast_tile_target;
  }

  public override void Invoke ()
  {
    source.Cast(ability, target: target, tile_target: tile_target);
  }


}
