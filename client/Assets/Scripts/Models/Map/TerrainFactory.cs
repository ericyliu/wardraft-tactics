namespace Wardraft.Game {

public static class TerrainFactory {

  public static Terrain Create (string type) {
    switch (type) {
      case "Air":
        return new Air();
      case "Cloud":
        return new Cloud();
      case "Dirt":
        return new Dirt();
      case "Earth":
        return new Earth();
      case "Grass":
        return new Grass();
      case "River":
        return new River();
      case "Road":
        return new Road();
      case "TestTerrain":
        return new TestTerrain();
      default:
        return new MissingTerrain();
    }
  }

}

}