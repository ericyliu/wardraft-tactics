using UnityEngine;
using System.Collections;

public static partial class Tests {

  public static void Map_Tests () {
    Debug.Log("Map_Tests");
    
    string mapname = "testmap";
    
    Test test;
    Map map = new Map();
    map.createMap(mapname);
    
    test = new Test("Testing corners of map are TestTerrain");
    TestTerrain test_terrain = new TestTerrain();
    test.assert("upper left").Equal(map.mapTiles[0,0].terrains[0].GetType(),test_terrain.GetType());
    test.assert("upper right").Equal(map.mapTiles[19,0].terrains[0].GetType(),test_terrain.GetType());
    test.assert("lower left").Equal(map.mapTiles[0,19].terrains[0].GetType(),test_terrain.GetType());
    test.assert("lower right").Equal(map.mapTiles[19,19].terrains[0].GetType(),test_terrain.GetType());
    test.finish();
    
    test = new Test("Tiles are height 0");
    test.assert("is height 0").Equal(map.mapTiles[0,0].height,0);
    test.finish();  
  }
  
}
