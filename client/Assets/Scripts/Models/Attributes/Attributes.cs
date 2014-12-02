public class Attributes {

  public FIntStat health, damage, armor, buildTime, mana, speed;
  public IntStat attackRange;
  public string[] buildList;

  public Attributes ( int health_stat = 0, int damage_stat = 0,
                      int armor_stat = 0, int build_stat = 0,
                      int range_stat = 0, int speed_stat = 0,
                      int mana_stat = 0, int[] build_list = null ) {
    health = new FIntStat(FInt.Create(health_stat));
    damage = new FIntStat(FInt.Create(damage_stat));
    armor = new FIntStat(FInt.Create(armor_stat));
    buildTime = new FIntStat(FInt.Create(build_stat));
    attackRange = new IntStat(range_stat);
    speed = new FIntStat(FInt.Create(speed_stat));
    mana = new FIntStat(FInt.Create(mana_stat));
    buildList = new string[build_list.Length];
    if (build_list != null) {
      for (int i=0; i<build_list.Length; i++) {
        buildList[i] = ActorList.units[build_list[i]];
      }
    }
    else buildList = new string[0];
  }

}
