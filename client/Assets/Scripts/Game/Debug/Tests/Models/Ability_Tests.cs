using UnityEngine;

public static partial class Tests {

  public static void Ability_Tests () {
    Debug.Log ("Ability Tests\n");
    Test test;

    string mapname = "testmap";
    var map = new Map();
    map.CreateMap(mapname);
    var unit1 = new TestUnit(0,"0");
    map.PlaceActiveActor(new Point(1,1), unit1);
    var unit2 = new TestUnit(1,"1");
    map.PlaceActiveActor(new Point(1,2), unit2);

    test = new Test("Able to cast targeted ability");
      unit1.Cast(unit1.abilities[1], aa_target: unit2);
      test.Assert("Takes correct damage").Equal(
        unit2.attributes.health.current,
        unit2.attributes.health.max - 1);
      unit2.ResetStats();
      test.Finish();

    test = new Test("Able to cast tile targeted ability");
      unit1.Cast(unit1.abilities[1], tile_target: unit2.position);
      test.Assert("Takes correct damage").Equal(
        unit2.attributes.health.current,
        unit2.attributes.health.max - 1);
      unit2.ResetStats();
      test.Finish();

    test = new Test("Able to cast self ability");
      unit2.attributes.health.current = unit2.attributes.health.max - 1;
      unit2.Cast(unit2.abilities[0]);
      test.Assert("Heals for correct health").Equal(
        unit2.attributes.health.current,
        unit2.attributes.health.max);
      test.Finish();
  }

}

