using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Wardraft.Service;
using Wardraft.App;

namespace Wardraft.Menu {

  public class RegisterViewModel : MonoBehaviour {
  
    public Text StatusText;
    public InputField UsernameField;
    public InputField PasswordField;
    public InputField PasswordConfirmField;
    public Button RegisterButton;
    
    public static RegisterViewModel current;
    
    void Start () {
      if (WebsocketService.current.connected) OnConnect(null); else OnDisconnect(null);
      registerServices();
      current = this;
    }
    
    void OnDestroy () {
      removeServices();
      current = null;
    }
    
    public void Register () {
      if (PasswordField.text == PasswordConfirmField.text) {
        RegisterController.Register(UsernameField.text, PasswordField.text);
      }
      else {
        DisplayStatus("Password and Confirm Password does not match.", Status.Error);
      }
    }
    
    public void Back () {
      ApplicationController.Current.LoadMenu(Enums.Scene.Login);
    }
    
    public void OnConnect (Hashtable data) {
      DisplayStatus("Please Sign In", Status.Neutral);
      UsernameField.enabled = true;
      PasswordField.enabled = true;
      RegisterButton.enabled = true;
    }
    
    public void OnDisconnect (Hashtable data) {
      DisplayStatus("You have lost connection to the server.", Status.Error);
      UsernameField.enabled = false;
      PasswordField.enabled = false;
      RegisterButton.enabled = false;
    }
    
    public void DisplayStatus (string message, Status status) {
      if (status == Status.Success) StatusText.color = Color.green;
      if (status == Status.Neutral) StatusText.color = Color.black;
      if (status == Status.Error) StatusText.color = Color.red;
      StatusText.text = message;
    }
    
    void registerServices () {
      WebsocketService.current.RegisterService("onWsOpen", OnConnect);
      WebsocketService.current.RegisterService("onWsClose", OnDisconnect);
    }
    
    void removeServices () {
      WebsocketService.current.RemoveService("onWsOpen", OnConnect);
      WebsocketService.current.RemoveService("onWsClose", OnDisconnect);
    }
  }

}