using UnityEngine;
using System.Collections;

public class TestBuff1 : Buff {

  public TestBuff1 (int buff_source) : base(buff_source, 1) {
    stack = true;
  }
  
}
