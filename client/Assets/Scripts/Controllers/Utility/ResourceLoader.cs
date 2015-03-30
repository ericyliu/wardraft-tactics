﻿using UnityEngine;
using System.Collections.Generic;

public class ResourceLoader : MonoBehaviour {

  public Dictionary<string, System.Action> todo;

  public Dictionary<int, Object>  terrains;
  public Dictionary<int, Object>  actors;
  public Dictionary<string, Object> misc;
  
  public bool done = false;
  
  public static ResourceLoader current;

  void Start () {
    misc = new Dictionary<string, Object>();
    todo = new Dictionary<string, System.Action>() {
      { "Loading terrain files.", loadTerrains },
      { "Loading actor files.", loadActors }
    };
    int i = 1;
    foreach (KeyValuePair<string, System.Action> entry in todo) {
      Debug.Log("Loading " + i + "/" + todo.Count + ": " + entry.Key);
      entry.Value();
      i++;
    }
    done = true;
    current = this;
  }
  
  void loadTerrains () {
    terrains = new Dictionary<int, Object>();
    foreach (KeyValuePair<int,string> entry in Terrains.codes) {
      Object prefab = Resources.Load("Terrains/" + entry.Value);
      if (prefab == null) Debug.LogError("Cannot find: " + entry.Value + " prefab.");
      terrains.Add(entry.Key, prefab);
    }
    misc.Add("tileSelectionOutline", Resources.Load("Terrains/SelectionOutline"));
    misc.Add("tileMouseoverOutline", Resources.Load("Terrains/MouseoverOutline"));
  }
  
  void loadActors () {
    actors = new Dictionary<int, Object>();
    foreach (KeyValuePair<int,string> entry in Actors.codes) {
      Object prefab = Resources.Load("Actors/" + entry.Value);
      if (prefab == null) Debug.LogError("Cannot find: " + entry.Value + " prefab.");
      actors.Add(entry.Key, prefab);
    }
  }

}
