using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Wardraft.Service;

namespace Wardraft.Menu {

  public static class ChatController {
    
    public static List<Chatroom> chatrooms = new List<Chatroom>();
    public static Chatroom currentChatroom;
    
    public static void SwitchChatroom (int id) {
      currentChatroom = chatrooms.Find(x => x.ID == id);
      if (currentChatroom == null) return;
      if (ChatMenuViewModel.current != null) ChatMenuViewModel.current.SwitchChatroom(currentChatroom);
    }
    
    public static void Start () {
      ChatService.RegisterServices();
    }
  
    public static void CreateChatroom () {
      ChatService.CreateChatroom();
    }
  
    public static void SendChat (string message) {
      if (currentChatroom == null) return;
      if (message.Trim() == "") return;
      ChatService.SendChat(currentChatroom, message.Trim());
    }
    
    public static void OnChatroomJoined (Chatroom chatroom) {
      chatrooms.Add(chatroom);
      if (ChatMenuViewModel.current != null) ChatMenuViewModel.current.AddChatroom(chatroom);
      SwitchChatroom(chatroom.ID);
    }
    
    public static void OnMessageReceived (int id, Chat chat) {
      Chatroom chatroom = chatrooms.Find(room => room.ID == id);
      chatroom.ChatLog.Add(chat);
      if (currentChatroom.ID == id) {
        if (ChatMenuViewModel.current != null) ChatMenuViewModel.current.AddChatText(chat.ToChatString());
      }
    }
    
  }

}
