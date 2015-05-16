using UnityEngine;
using System.Collections.Generic;

public class Chatroom {

  public int ID;
  public string Name;
  public string RoomType;
  public List<Chat> ChatLog = new List<Chat>();
  public List<string> Users = new List<string>();
  
  public Chatroom (int id, string type) {
    this.ID = id;
    this.RoomType = type;
  }
  
  public string ChatLogToString () {
    string text = "";
    foreach (Chat chat in ChatLog) {
      text += chat.ToChatString() + "\n";
    }
    return text;
  }

}
