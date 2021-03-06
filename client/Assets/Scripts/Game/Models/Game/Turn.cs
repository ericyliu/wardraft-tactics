﻿using System.Collections.Generic;

[System.Serializable]
public class Turn {

	public int number;
  public Player player;

  public List<Command> commands;

  public Turn (int turn_number,  Player turn_player) {
    number = turn_number;
    player = turn_player;
  }

  public void AddCommand (Command command) {
    commands.Add(command);
  }

  public void DoCommands () {
    foreach (Command command in commands) {
      command.Invoke();
    }
  }

  #region operators
  public override bool Equals (object obj)
  {
    var turn = obj as Turn;
    return turn != null && turn.number == number && turn.player.name == player.name;
  }

  public override int GetHashCode ()
  {
    int hash = 17;
    hash = hash * 23 + number.GetHashCode();
    hash = hash * 23 + player.name.GetHashCode();
    return hash;
  }

  public static bool operator ==(Turn t1, Turn t2) {
    return t1.Equals(t2);
  }
  public static bool operator !=(Turn t1, Turn t2) {
    return !t1.Equals(t2);
  }
  #endregion

}
