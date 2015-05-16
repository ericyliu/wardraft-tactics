using UnityEngine;
using System.Collections;

public class Chat {

  public string Sender;
  public string Message;
  public string Action;
  public string DateTime;
  
  public Chat (Hashtable chat) {
    this.Sender = chat["sender"].ToString();
    if (chat["text"] == null) this.Message = "";
    else this.Message = chat["text"].ToString();
    this.Action = chat["action"].ToString();
    this.DateTime = chat["time"].ToString();
  }
  
  public string ToChatString () {
    if (Action == "join") return "<color=#777777FF>[" + Sender + "] has joined the room.</color>";
    if (Action == "leave") return "<color=#777777FF>[" + Sender + "] has left the room.</color>";
    if (Action == "message") return "<b>" + Sender + "</b>: " + Message;
    return Message;
  }

}
