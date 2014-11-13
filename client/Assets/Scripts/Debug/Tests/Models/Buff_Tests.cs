using UnityEngine;
using System.Collections;

public static partial class Tests {

  public static void Buff_Tests () {
    Debug.Log("Buff Tests");
    
    Test test = new Test("Buff equality is correct");
      Buff buff1 = new Buff(0,0);
      buff1.stack = true;
      Buff buff2 = new Buff(0,0);
      test.assert ("Unstackable buffs with same id, same source are equal").Equal(buff1,buff2);
      buff1.stack = true;
      buff2.stack = true;
      test.assert("Stackable buffs with same id, same source are equal").Equal(buff1,buff2);
      buff2 = new Buff(1,0);
      test.assert("Unstackable buffs with same id, different source are equal").Equal(buff1,buff2);
      buff1.stack = true;
      buff2.stack = true;
      test.assert("Stackable buffs with same id, different source are not equal").NotEqual(buff1,buff2);
      buff1 = new Buff(0,0);
      buff2 = new Buff(0,1);
      test.assert("Unstackable buffs with different id are not equal").NotEqual(buff1,buff2);
      buff1.stack = true;
      buff2.stack = true;
      test.assert("Stackable buffs with different id are not equal").NotEqual(buff1,buff2);
      test.finish();
  }
  
}
