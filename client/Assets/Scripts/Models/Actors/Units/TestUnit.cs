public class TestUnit : Unit {

  public TestUnit (int aid, int oid) : base(aid, oid) {
    code = 0;
    var attr = new Attributes(health_stat: 10, damage_stat: 1,
                              armor_stat: 1,  build_stat: 1,
                              range_stat: 1,  speed_stat: 5,
                              mana_stat: 1);
    AddAbilityByID(0);
    AddAbilityByID(1);
    AddAbilityByID(2);
    AssignStats(attr);
  }

}
