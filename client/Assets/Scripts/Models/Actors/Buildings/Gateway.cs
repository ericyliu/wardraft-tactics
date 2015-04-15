[System.Serializable]
public class Gateway : Building {

  public Gateway (int aid, string oid) : base (aid,oid) {
    code = 500;
    var attr = new Attributes(health_stat: 20, damage_stat: 0,
                              armor_stat: 1,  build_stat: 1,
                              range_stat: 0,  speed_stat: 0,
                              mana_stat: 0);
    AssignStats(attr);
  }
  
}