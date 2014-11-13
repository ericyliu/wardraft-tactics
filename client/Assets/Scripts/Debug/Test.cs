using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test {
  
  private string message;
  public bool truth;
  public string failure;
  
  public Test(string test_message) {
    message = test_message;
    truth = true;
  }
  
  public Assertion assert (string name) {
    Assertion assertion = new Assertion(name, this);
    return assertion;
  }
  
  public void finish () {
    if (truth) success(message + " - passed");
    else {
      error(message);
      error("            " + failure + " - failed");
    }
  }
 
  private void success (string text) {
    Debug.Log("<color=green>          " + text + "</color>");
  }
  
  private void error (string text) {
    Debug.Log("<color=red>          " + text + "</color>");
  }
  
}

