using System;

public class Assertion {

  public string message;
  public Test test;

  public Assertion (string assert_message, Test assert_test) {
    message = assert_message;
    test = assert_test;
  }

  public void Equal(Object obj1, Object obj2) {
    if (obj1.Equals(obj2)) success();
    else fail();

  }

  public void NotEqual(Object obj1, Object obj2) {
    if (!obj1.Equals(obj2)) success();
    else fail();
  }

  private void success () {

  }

  private void fail () {
    if (test.truth) {
      test.failure = message;
      test.truth = false;
    }
  }

}
