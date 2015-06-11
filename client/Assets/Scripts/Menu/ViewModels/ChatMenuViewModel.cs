using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Wardraft.Menu {

  public class ChatMenuViewModel : MonoBehaviour {
  
    public Transform ChatTabPanel;
    public GameObject ChatMessagesText;
    public GameObject ChatInput;
    
    public static ChatMenuViewModel current;
    
    readonly Color activeChatColor = Color.white;
    readonly Color inactiveChatColor = Color.gray; 
    
    GameObject chatTabPrefab;
    
    void Start () {
      current = this;
      chatTabPrefab = Resources.Load("Menu/Button-Chattab") as GameObject;
    }
    
    void Update () {
      if (EventSystem.current.currentSelectedGameObject == ChatInput) {
        if (Input.GetKeyDown(KeyCode.Return)) SendChat();
      }
    }
    
    void OnDestroy () {
      current = null;
    }
    
    public void SendChat () {
      ChatController.SendChat(GetInputText());
      ClearInputText();
      ChatInput.GetComponent<InputField>().ActivateInputField();
    }
    
    public string GetInputText () {
      return ChatInput.GetComponent<InputField>().text;
    }
    
    public void ClearInputText () {
      ChatInput.GetComponent<InputField>().text = "";
    }
    
    public void SetChatText (string text) {
      ChatMessagesText.GetComponent<Text>().text = text;
    }
    
    public void AddChatText (string text) {
      ChatMessagesText.GetComponent<Text>().text += text + "\n";
    }
    
    public void AddChatroom (Chatroom chatroom) {
      GameObject chatTab = Instantiate(chatTabPrefab);
      chatTab.transform.SetParent(ChatTabPanel, false);
      chatTab.name = "Button-ChatTab:" + chatroom.ID;
    }
    
    public void SwitchChatroom (Chatroom chatroom) {
      for (int i=0; i<ChatTabPanel.childCount; i++) {
        Button button = ChatTabPanel.GetChild(i).GetComponent<Button>();
        ColorBlock colors = button.colors;
        colors.normalColor = inactiveChatColor;
        button.colors = colors;
      }
      Utility.SetButtonNormalColor(ChatTabPanel.Find("Button-ChatTab:" + chatroom.ID), activeChatColor);
      SetChatText(chatroom.ChatLogToString());
    }
    
  }
 
}
