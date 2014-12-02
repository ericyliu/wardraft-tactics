using UnityEngine;
using System.Collections.Generic;

public class Draft {

  private Game game;

  private Dictionary<string, List<string>>  available_picks;
  private Dictionary<string, List<string>>  picked;
  private readonly List<string>             unavailable;

  private List<string>                      pick_order;
  private int                               pick_index;
  private int                               max_pick;
  private int                               player_count;
  private string                            picking_player;


  public Draft (List<User> users, Game current_game) {
    game = current_game;
    available_picks = new Dictionary<string, List<string>>();
    picked = new Dictionary<string, List<string>>();
    unavailable = new List<string>();
    pick_order = new List<string>();
    pick_index = 0;
    max_pick = users.Count * GameValues.NUM_PICKS;
    player_count = users.Count;

    foreach (User user in users) {
      available_picks.Add(user.displayName,user.availableUnits);
      picked.Add(user.displayName,new List<string>());
      pick_order.Add(user.displayName);
    }
  }

  public string NextPick () {
    if (pick_index < max_pick) {
      picking_player = pick_order[pick_index%player_count];
      pick_index++;
      return picking_player;
    }
    return "";
  }

  public void AddPick (string name, string pick) {
    picked[name].Add(pick);
    picked[name].Sort();
    removePick(pick);
  }

  public List<string> ReturnPicks (string name) {
    return picked[name];
  }

  public void PrintPickOrder () {
    string str = "[";
    foreach (string name in pick_order) {
      str += name + ",";
    }
    str.Remove (str.Length-2);
    str += "]";
    Debug.Log("Pick Order: " + str);
  }

  private void donePick () {
    game.OnUnitPickDone(this);
  }

  private void removePick (string pick) {
    unavailable.Add(pick);
  }

}
