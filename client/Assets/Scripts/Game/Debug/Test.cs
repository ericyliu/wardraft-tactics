using UnityEngine;

public class Test {

  private string message;
  public bool truth;
  public string failure;

  public Test(string test_message) {
    message = test_message;
    truth = true;
  }

  public Assertion Assert (string name) {
    var assertion = new Assertion(name, this);
    return assertion;
  }

  public void Finish () {
    if (truth) success(message + " - passed");
    else {
      error(message);
      error("            " + failure + " - failed");
    }
  }

  private void success (string text) {
    Debug.Log("<color=green>          " + text + "</color>\n");
  }

  private void error (string text) {
    Debug.Log("<color=red>          " + text + "</color>\n");
  }

}

