using UnityEngine;
using System.Collections.Generic;

public static partial class Tests {

  public static void Pathing_Tests () {
    Debug.Log("Pathing Tests\n");

    string mapname = "testmap";
    Test test;
    var map = new Map();
    map.CreateMap(mapname);

    test = new Test("Finds the correct moves");
      Unit unit = new TestUnit(0,"0");
      var start = new Point(1,1);
      var finish = new Point(6,1);
      map.PlaceActiveActor(start, unit);
      test.Assert("Starts at 1,1").Equal(unit, map.GetTile(start).actors[1]);
      List<Tile> tiles = map.TilesInUnitMoveRange(unit);
      test.Assert("Has 6,1 as possible move").Equal(tiles.Contains(map.GetTile(finish)), true);
      test.Assert("Has 1,6 as possible move").Equal(tiles.Contains(map.GetTile(new Point(1,6))), true);
      test.Finish();

    test = new Test("Builds the correct path");
      var path = new List<Tile>();
      map.BuildPath(map.GetTile(start), map.GetTile(finish), ref path);
      test.Assert("Goes through 5,1").Equal(path.Contains(map.GetTile(new Point(3,1))), true);
      test.Assert("Goes through 4,1").Equal(path.Contains(map.GetTile(new Point(4,1))), true);
      test.Assert("Goes through 3,1").Equal(path.Contains(map.GetTile(new Point(3,1))), true);
      test.Assert("Goes through 2,1").Equal(path.Contains(map.GetTile(new Point(2,1))), true);
      test.Finish();

    test = new Test("Moves unit correctly");
      unit.Move(path);
      test.Assert("Ends at 6,1").Equal(unit, map.GetTile(finish).actors[1]);
      test.Finish();
  }

}
