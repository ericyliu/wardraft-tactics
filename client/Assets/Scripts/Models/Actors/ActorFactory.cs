using UnityEngine;

public static class ActorFactory {
  
  public static Actor Create (string type, int aid, string oid) {
    switch (type) {
      case "Bunny":
        return new Bunny(aid, oid);
      case "TestUnit":
        return new TestUnit(aid, oid);
      default:
        Debug.LogError("Actor Type: " + type + " not found.");
        return new TestUnit(aid, oid);
    }
  }
  
}
