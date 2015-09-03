using System.Collections.Generic;

public static class AbilityList  {

  public static Dictionary<int, string> buffs = new Dictionary<int, string> {
    {0,"TestBuff0"},
    {1,"TestBuff1"}
  };

  public static Dictionary<int, string> abilities = new Dictionary<int, string> {
    {0,"Heal Self"},
    {1,"Damage Target"},
    {2,"Damage Tile"}
  };

}
