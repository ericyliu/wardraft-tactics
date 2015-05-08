using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine.UI;
using Wardraft.Service;
using Wardraft.App;

namespace Wardraft.Menu {

  public class LoginController : MonoBehaviour {
  
    //GameObjects to be set by developer in Unity Editor
    public Text StatusText;
    public InputField UsernameField;
    public InputField PasswordField;
    public Button LoginButton;
  
    void Start () {
      if (WebsocketService.current.connected) OnConnect(null); else OnDisconnect(null);
      registerServices();
    }
    
    public void Login () {
      Hashtable data = new Hashtable();
      data.Add("username", UsernameField.text);
      data.Add("password", PasswordField.text);
      LoadingWidgetController.current.Display("Logging In");
      WebsocketService.current.Send("account/login", data);
    }
    
    public void OnConnect (Hashtable data) {
      displayStatus("Please Sign In", Status.Neutral);
      UsernameField.enabled = true;
      PasswordField.enabled = true;
      LoginButton.enabled = true;
    }
    
    public void OnDisconnect (Hashtable data) {
      displayStatus("You have lost connection to the server.", Status.Error);
      UsernameField.enabled = false;
      PasswordField.enabled = false;
      LoginButton.enabled = false;
    }
    
    void registerServices () {
      WebsocketService.current.RegisterService("onWsOpen", OnConnect);
      WebsocketService.current.RegisterService("onWsClose", OnDisconnect);
    }
    
    void displayStatus (string message, Status status) {
      if (status == Status.Success) StatusText.color = Color.green;
      if (status == Status.Neutral) StatusText.color = Color.black;
      if (status == Status.Error) StatusText.color = Color.red;
      StatusText.text = message;
    }
    
  }

}