using System.Collections.Generic;

public static class ActorList {

  public static Dictionary<int, ActorInfo> codes = new Dictionary<int, ActorInfo> {
    // Testing
    {100, new ActorInfo(100, 0, "TestUnit")},
    {1, new ActorInfo(1, 50, "Bunny")},
    // Buildings
    {500, new ActorInfo(500, 50, "Gateway")}
  };

}
