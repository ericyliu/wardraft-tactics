public static class AbilityFactory {

  public static Ability Create (string name) {
    switch (name) {
      case "Heal Self":
        return new TestAbility0();
      case "Damage Target":
        return new TestAbility1();
      case "Damage Tile":
        return new TestAbility2();
      default:
        return null;
    }
  }

}
