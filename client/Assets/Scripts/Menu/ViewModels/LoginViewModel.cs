using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine.UI;
using Wardraft.Service;
using Wardraft.App;

namespace Wardraft.Menu {

  public class LoginViewModel : MonoBehaviour {
  
    //GameObjects to be set by developer in Unity Editor
    public Text StatusText;
    public InputField UsernameField;
    public InputField PasswordField;
    public Button LoginButton;
    
    public static LoginViewModel Current;
  
    void Start () {
      if (WebsocketService.current.connected) OnConnect(null); else OnDisconnect(null);
      registerServices();
      Current = this;
    }
    
    void OnDestroy () {
      removeServices();
      Current = null;
    }
    
    public void Login () {
      LoginController.Login(UsernameField.text, PasswordField.text);
    }

    public void OnConnect (Hashtable data) {
      DisplayStatus("Please Sign In", Status.Neutral);
      UsernameField.enabled = true;
      PasswordField.enabled = true;
      LoginButton.enabled = true;
    }
    
    public void OnDisconnect (Hashtable data) {
      DisplayStatus("You have lost connection to the server.", Status.Error);
      UsernameField.enabled = false;
      PasswordField.enabled = false;
      LoginButton.enabled = false;
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