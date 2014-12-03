using UnityEngine;

public static partial class Tests {

  public static void Attack_Tests () {
    Debug.Log ("Attack Tests\n");
    Test test;

    string mapname = "testmap";
    var map = new Map();
    map.CreateMap(mapname);
    var unit1 = new TestUnit(0,0);
    map.PlaceActiveActor(new Point(1,1), unit1);
    var unit2 = new TestUnit(1,1);
    map.PlaceActiveActor(new Point(1,2), unit2);

    test = new Test("Able to attack target");
      unit1.Attack(unit2);
      test.Assert("Takes correct damage").Equal(
        unit2.attributes.health.current,
        unit2.attributes.health.max - 1);
      test.Assert("Cannot attack again").Equal(
        unit1.canAttack,
        false);
      unit2.ResetStats();
      test.Finish();
  }

}

