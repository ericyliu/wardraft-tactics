using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test {
  
  private string message;
  private bool truth;
  private string failure;
  private Dictionary<string, System.Object> objects;
  
  public Test(string test_message) {
    message = test_message;
    objects = new Dictionary<string, System.Object>();
    truth = true;
  }
  
  public void assert (string name, bool statement) {
    if (statement == false) {
      if (truth) {
        truth = false;
        failure = name;
      }
    }
  }
  
  public void finish () {
    if (truth) success(message + " - passed");
    else {
      error(message);
      error("            " + failure + " - failed");
    }
    cleanup();
  }
  
  public System.Object add (string name, System.Object obj) {
    objects.Add(name,obj);
    return obj;
  }
  
  private void cleanup() {
    objects = null;
  }
  
  private void success (string text) {
    Debug.Log("<color=green>" + text + "</color>");
  }
  
  private void error (string text) {
    Debug.Log("<color=red>" + text + "</color>");
  }
  
}

