using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Wardraft.Service;

public static class ChatController {
  
  public static List<Chatroom> chatrooms = new List<Chatroom>();
  public static Chatroom currentChatroom;
  
  public static void SwitchChatroom (int id) {
    currentChatroom = chatrooms.Find(x => x.ID == id);
    if (currentChatroom == null) return;
    if (ChatMenuViewModel.Current != null) ChatMenuViewModel.Current.SwitchChatroom(currentChatroom);
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
    if (ChatMenuViewModel.Current != null) ChatMenuViewModel.Current.AddChatroom(chatroom);
    SwitchChatroom(chatroom.ID);
  }
  
  public static void OnMessageReceived (int id, Chat chat) {
    Chatroom chatroom = chatrooms.Find(room => room.ID == id);
    chatroom.ChatLog.Add(chat);
    if (currentChatroom.ID == id) {
      if (ChatMenuViewModel.Current != null) ChatMenuViewModel.Current.AddChatText(chat.ToChatString());
    }
  }
  
  
  

}
