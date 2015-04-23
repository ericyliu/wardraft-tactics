using UnityEngine;
using Wardraft.Game;


public static class ActorFactory {
  
  public static Actor Create (string type, int aid, string oid) {
    Actor actor = null;
    switch (type) {
      case "Gateway":
        actor =  new Gateway(aid, oid);
        break;
      case "Bunny":
        actor = new Bunny(aid, oid);
        break;
      case "TestUnit":
        actor = new TestUnit(aid, oid);
        break;
      default:
        Debug.LogError("Actor Type: " + type + " not found.");
        break;
    }
    if (actor != null) {
      Game.current.actors.Add(actor);
      if (actor is ActiveActor) Game.current.GetPlayer(oid).ownedActives.Add(actor as ActiveActor);
    }
    return actor;
  }
  
}
