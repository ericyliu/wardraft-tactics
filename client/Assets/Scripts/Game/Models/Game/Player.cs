using System.Collections.Generic;

[System.Serializable]
public class Player {

  public string id;

  public string name;
  public Enums.Color color;

	public int gold, pauses;
  public IntStat population;
  public bool alive;

  public List<string> buildableUnits;
  public List<ActiveActor> ownedActives;

  public Player (User user) {
    id = user.id;
  
    name = user.displayName;
    color = Enums.GetNextColor();

    gold = GameValues.STARTING_GOLD;
    pauses = GameValues.MAX_PAUSES;
    population = new IntStat(0);
    population.normal = GameValues.MAX_POP;
    population.max = GameValues.STARTING_POP;
    alive = true;

    buildableUnits = new List<string>();
    ownedActives = new List<ActiveActor>();
  }



}
