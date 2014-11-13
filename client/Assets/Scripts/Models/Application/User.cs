using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class User {

  public int level;
	public string displayName, id, avatar_path;
  
  public List<string> availableUnits = new List<string>();
  
  public User(string name, string user_id) {
    displayName = name;
    id = user_id;
  }
  
  public void addUnit (string unit) {
    availableUnits.Add(unit);
    availableUnits.Sort();
  }
  
  public void removeUnit (string unit) {
    availableUnits.Remove(unit);
  }
  
}
