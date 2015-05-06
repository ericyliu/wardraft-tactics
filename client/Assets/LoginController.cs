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
    public InputField UsernamePassword;
  
    void Start () {
      if (WebsocketService.current.connected) OnConnect(null); else OnDisconnect(null);
      WebsocketService.current.RegisterService("onWsOpen", OnConnect);
      WebsocketService.current.RegisterService("onWsClose", OnDisconnect);
    }
    
    public void OnConnect (Hashtable data) {
      displayStatus("Please Sign In", Status.Neutral);
    }
    
    public void OnDisconnect (Hashtable data) {
      displayStatus("You have lost connection to the server.", Status.Error);
    }
    
    void testJSON () {
      Hashtable toConvert = new Hashtable();
      toConvert["entry1"] = "value1";
      toConvert["entry2"] = "value2";
      string jsonObject = JsonConvert.SerializeObject(toConvert);
      Debug.Log (jsonObject);
    }
    
    void displayStatus (string message, Status status) {
      if (status == Status.Success) StatusText.color = Color.green;
      if (status == Status.Neutral) StatusText.color = Color.black;
      if (status == Status.Error) StatusText.color = Color.red;
      StatusText.text = message;
    }
    
  }

}