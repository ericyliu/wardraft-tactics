public static class Enums {

  private static int color_index;
  public enum Color {Red, Blue, Green, Yellow};

  public enum GameState {Unstarted, Loading, Loaded, UnitPick, Starting, Playing, Paused, Finished};
  public enum ActiveActorState {Alive, Dead};
  public enum AnimationState {Moving, Attacking, Standing, Dying, TakeDamage};

  public enum Layers {Underground, Ground, Air, Heightmap, Neutral, Players};
  
  public enum SpellTarget {Self, Target, Tile};

  public static Color GetNextColor () {
    var color = (Color)color_index;
    color_index++;
    return color;
  }

}
