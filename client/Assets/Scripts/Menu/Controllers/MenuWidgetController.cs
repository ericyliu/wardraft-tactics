using UnityEngine;
using System.Collections.Generic;
using Wardraft.Service;
using System.Collections;

public class MenuWidgetController : MonoBehaviour {

  public static MenuWidgetController current;
  public enum MenuButton {Return, Logout, Quit}
  
  Dictionary<MenuButton, GameObject> buttonMap;
      
  void Awake () {    
    current = this;
    loadButtonMap();
    SetButtons(new List<MenuButton>());
  }
  
  void Start () {
    Toggle();
  }
  
  public void SetButtons (List<MenuButton> buttons) {
    Transform menu = gameObject.transform.FindChild("Menu");
    int index = 0;
    foreach (KeyValuePair<MenuButton, GameObject> entry in buttonMap) {
      if (buttons.Contains(entry.Key)) {
        entry.Value.SetActive(true);
        Utility.PositionGameObject(entry.Value, 0f, -25f - (index * 40) - ((index - 1) * 10));
        index++;
      }
      else entry.Value.SetActive(false);
    }
    menu.GetComponent<RectTransform>().sizeDelta =
      new Vector2(200f, (buttons.Count * 40) + ((buttons.Count - 1) * 10) + 30);
  }
  
  public void Toggle () {
    gameObject.SetActive(!gameObject.activeSelf);
  }
  
  public void Logout () {
    WebsocketService.current.Send("account/logout");
  }
  
  public void Quit () {
    Application.Quit();
  }
  
  void loadButtonMap () {
    buttonMap = new Dictionary<MenuButton, GameObject>();
    foreach (MenuButton button in MenuButton.GetValues(typeof(MenuButton))) {
      buttonMap.Add(button, gameObject.transform.Find("Menu/Button-" + button.ToString()).gameObject);
    }
  }

}
