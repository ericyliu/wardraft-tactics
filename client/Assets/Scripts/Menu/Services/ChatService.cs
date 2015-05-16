using UnityEngine;
using System.Collections;
using System;
using Newtonsoft.Json.Linq;

namespace Wardraft.Service {

  public static class ChatService {
  
    public static void RegisterServices () {
      WebsocketService.current.RegisterService("chat/create", OnCreate);
      WebsocketService.current.RegisterService("chat/message", OnMessage);
      WebsocketService.current.RegisterService("chat/join", OnJoin);
    }
    
    // Service Calls
    
    public static void CreateChatroom () {
      WebsocketService.current.Send("chat/create");
    }
    
    public static void JoinChatroom (int id) {
      Hashtable data = new Hashtable();
      data["id"] = id;
      WebsocketService.current.Send("chat/join", data);
    }
    
    public static void SendChat (Chatroom chatroom, string message) {
      Hashtable data = new Hashtable();
      data["id"] = chatroom.ID;
      data["text"] = message;
      data["type"] = chatroom.RoomType;
      WebsocketService.current.Send("chat/message", data);
    }
    
    // End Service Calls
    
    // Callbacks
    
    public static void OnCreate (Hashtable data) {
      JoinChatroom(int.Parse(data["id"].ToString()));
    }
    
    public static void OnJoin (Hashtable data) {
      if (!Convert.ToBoolean(data["success"])) {
        Debug.LogError(data["message"]);
        return;
      }
      Chatroom chatroom = new Chatroom(int.Parse(data["id"].ToString()), data["type"].ToString());
      chatroom.Name = data["name"].ToString();
      foreach (JObject chatHash in data["history"] as IEnumerable) {
        Chat chat = new Chat(chatHash.ToObject<Hashtable>());
        chatroom.ChatLog.Add(chat);
      }
      foreach (var user in data["users"] as IEnumerable) chatroom.Users.Add(user.ToString());
      ChatController.OnChatroomJoined(chatroom);
    }
    
    public static void OnMessage (Hashtable data) {
      if (!Convert.ToBoolean(data["success"])) {
        Debug.LogError(data["message"]);
        return;
      }
      Chat chat = new Chat((data["chat"] as JObject).ToObject<Hashtable>());
      ChatController.OnMessageReceived(int.Parse(data["id"].ToString()), chat);
    }
    
    // End Callbacks
  
  }

}