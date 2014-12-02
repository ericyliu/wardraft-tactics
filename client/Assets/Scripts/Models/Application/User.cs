using System.Collections.Generic;

public class User {

  public int level;
  public string displayName, id, avatar_path;

  public List<string> availableUnits = new List<string>();

  public User(string name, string user_id) {
    displayName = name;
    id = user_id;
  }

  public void AddUnit (string unit) {
    availableUnits.Add(unit);
    availableUnits.Sort();
  }

  public void RemoveUnit (string unit) {
    availableUnits.Remove(unit);
  }

}
