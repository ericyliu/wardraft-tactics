public static class AbilityFactory {

  public static Ability Create (string name) {
    switch (name) {
      case "TestAbility0":
        return new TestAbility0();
      case "TestAbility1":
        return new TestAbility1();
      case "TestAbility2":
        return new TestAbility2();
      default:
        return new TestAbility0();
    }
  }

}
