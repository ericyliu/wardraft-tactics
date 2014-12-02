public class TestUnit : Unit {

	public TestUnit (int aid, int oid) : base(aid, oid) {
    type_id = 0;
    var attr = new Attributes(health_stat: 1, damage_stat: 1,
                              armor_stat: 1,  build_stat: 1,
                              range_stat: 1,  speed_stat: 1,
                              mana_stat: 1);
    AssignStats(attr);
  }

}
