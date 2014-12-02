using UnityEngine;

public static partial class Tests {

  public static void Map_Tests () {
    Debug.Log("Map_Tests");

    string mapname = "testmap";

    Test test;
    var map = new Map();
    map.CreateMap(mapname);

    test = new Test("Testing corners of map are TestTerrain");
    var test_terrain = new TestTerrain();
    test.Assert("upper left").Equal(map.mapTiles[0,0].terrains[0].GetType(),test_terrain.GetType());
    test.Assert("upper right").Equal(map.mapTiles[19,0].terrains[0].GetType(),test_terrain.GetType());
    test.Assert("lower left").Equal(map.mapTiles[0,19].terrains[0].GetType(),test_terrain.GetType());
    test.Assert("lower right").Equal(map.mapTiles[19,19].terrains[0].GetType(),test_terrain.GetType());
    test.Finish();

    test = new Test("Tiles are height 0");
    test.Assert("is height 0").Equal(map.mapTiles[0,0].height,0);
    test.Finish();
  }

}
