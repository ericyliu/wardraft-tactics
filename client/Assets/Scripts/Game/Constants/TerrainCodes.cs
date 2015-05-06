using System.Collections.Generic;

public static class Terrains {

  public static Dictionary<int, string> codes = new Dictionary<int, string> {
    // Testing
    {254, "MissingTerrain"},
    {25, "TestTerrain"},

    // Underground
    {21, "Earth"},

    // Ground
    {3, "Dirt"},
    {17, "Road"},
    {207, "River"},
    {1, "Grass"},
    {2, "Stone"},
    {5, "Wood"},

    // Air
    {50, "Air"},
    {23, "Cloud"}
  };

}
