using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Terrains {

  public static Dictionary<int, string> codes = new Dictionary<int, string>()
  {
    // Testing
    {1, "TestTerrain"},
    
    // Underground
    {100, "Earth"},
    
    // Ground
    {200, "Dirt"},
    {201, "Road"},
    {202, "River"},
    {203, "Grass"},
    
    // Air
    {300, "Air"},
    {301, "Cloud"}
  };
	
}
