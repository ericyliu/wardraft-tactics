using System.Collections.Generic;

[System.Serializable]
public class Move : Command {

  readonly ActiveActor source;
	readonly List<Tile> path;

  public Move (ActiveActor move_source, List<Tile> move_path) {
    source = move_source;
    path = move_path;
  }

  public override void Invoke ()
  {
    source.Move(path);
  }

}
