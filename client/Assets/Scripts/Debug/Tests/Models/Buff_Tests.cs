using UnityEngine;

public static partial class Tests {

  public static void Buff_Tests () {
    Debug.Log("Buff Tests\n");

    var test = new Test("Buff equality is correct");
      Buff buff1 = new TestBuff0(0);
      Buff buff2 = new TestBuff0(0);
      test.Assert("Unstackable buffs with same id, same source are equal").Equal(buff1,buff2);
      buff2 = new TestBuff0(1);
      test.Assert("Unstackable buffs with same id, different source are equal").Equal(buff1,buff2);
      buff2 = new TestBuff1(0);
      test.Assert("Buffs with different id are not equal").NotEqual(buff1,buff2);

      buff1 = new TestBuff1(0);
      test.Assert("Stackable buffs with same id, same source are equal").Equal(buff1,buff2);
      buff2 = new TestBuff1(1);
      test.Assert("Stackable buffs with same id, different source are not equal").NotEqual(buff1,buff2);
      test.Finish();
  }

}
