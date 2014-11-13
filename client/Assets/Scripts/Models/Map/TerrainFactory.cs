using UnityEngine;
using System.Collections;

public static class TerrainFactory {

  public static Terrain create (string type) {
    switch (type) {
      case "Air":
        return new Air() as Terrain;
      case "Cloud":
        return new Cloud() as Terrain;
      case "Dirt":
        return new Dirt() as Terrain;
      case "Earth":
        return new Earth() as Terrain;
      case "Grass":
        return new Grass() as Terrain;
      case "River":
        return new River() as Terrain;
      case "Road":
        return new Road() as Terrain;
      case "TestTerrain":
        return new TestTerrain() as Terrain;
      default:
          return new MissingTerrain() as Terrain;
    }
  }

}
