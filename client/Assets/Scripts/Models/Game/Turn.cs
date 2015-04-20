using System.Collections.Generic;

[System.Serializable]
public class Turn {

	public int number;
  public string playerId;

  public List<Command> commands;

  public Turn (int turn_number,  string player_id) {
    number = turn_number;
    playerId = player_id;
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
    return turn != null && turn.number == number && turn.playerId == playerId;
  }

  public override int GetHashCode ()
  {
    int hash = 17;
    hash = hash * 23 + number.GetHashCode();
    hash = hash * 23 + playerId.GetHashCode();
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
