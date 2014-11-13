using UnityEngine;
using System.Collections;

public static partial class Tests {

  public static void Buff_Tests () {
    Debug.Log("Buff Tests");
    
    Test test = new Test("Buff equality is correct");
      Buff buff1 = new TestBuff0(0) as Buff;
      Buff buff2 = new TestBuff0(0) as Buff;
      test.assert("Unstackable buffs with same id, same source are equal").Equal(buff1,buff2);
      buff2 = new TestBuff0(1) as Buff;
      test.assert("Unstackable buffs with same id, different source are equal").Equal(buff1,buff2);
      buff2 = new TestBuff1(0) as Buff;
      test.assert("Buffs with different id are not equal").NotEqual(buff1,buff2);
      
      buff1 = new TestBuff1(0) as Buff;
      test.assert("Stackable buffs with same id, same source are equal").Equal(buff1,buff2);
      buff2 = new TestBuff1(1) as Buff;
      test.assert("Stackable buffs with same id, different source are not equal").NotEqual(buff1,buff2);
      test.finish();
  }
  
}
