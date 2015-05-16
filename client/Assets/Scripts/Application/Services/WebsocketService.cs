using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using WebSocketSharp;
using Wardraft.App;
using System.Threading;
using Newtonsoft.Json;

namespace Wardraft.Service {

public class WebsocketService : MonoBehaviour {

  public static WebsocketService current;
  public bool connected;

  WebSocket ws;
  
  char delimiter = '|';

  Dictionary<string, List<System.Action<Hashtable>>> serviceMap = new Dictionary<string, List<System.Action<Hashtable>>>();
  List<System.Action> todo = new List<System.Action>();

  void Awake () {
    DontDestroyOnLoad(gameObject);
    current = this;
    createWebsocket();
    InvokeRepeating("connect", 0f, 5f);
  }
  
  void Update () {
    while (todo.Count > 0) {
      todo[0].Invoke();
      todo.RemoveAt(0);
    }
  }
  
  public void RegisterService (string route, System.Action<Hashtable> action) {
    if (!serviceMap.ContainsKey(route)) serviceMap.Add(route, new List<System.Action<Hashtable>>());
    serviceMap[route].Add(action);
  }
  
  public void RemoveService (string route, System.Action<Hashtable> action) {
    if (!serviceMap.ContainsKey(route)) return;
    if (!serviceMap[route].Contains(action)) return;
    serviceMap[route].Remove(action);
  }

  public void Send (string route, Hashtable data) {
    if (connected && data != null) {
      string message = createMessage(route, data);
      Debug.Log("Sending: " + message);
      ws.Send(message);
    }
  }
  
  public void Send (string route) {
    if (connected) {
      string message = createMessage(route, new Hashtable());
      ws.Send(message);
      Debug.Log("Sent: " + message);
    }
  }

  void createWebsocket () {
    ws = new WebSocket(string.Format("ws://{0}:{1}", Config.SERVER_ADDR, Config.SERVER_PORT));
    ws.OnMessage += (object sender, MessageEventArgs e) => handleMessage(e.Data);
    ws.OnClose += (object sender, CloseEventArgs e) => handleConnectionClose();
    ws.OnOpen += (object sender, System.EventArgs e) => handleConnectionOpen();
  }
  
  void connect () {
    if (!connected) {
      LoadingWidgetController.current.Display("Estbalishing connection to server...");
      if (ws == null) createWebsocket();
      ws.Connect();
    }
  }
  
  void handleMessage (string message) {
    Debug.Log("Recieved: " + message);
    string route = message.Split(delimiter)[0];
    Hashtable data = JsonConvert.DeserializeObject<Hashtable>(message.Split(delimiter)[1]);
    if (serviceMap.ContainsKey(route)) {
      foreach (System.Action<Hashtable> action in serviceMap[route]) {
        todo.Add(() => action.Invoke(data));
      }
    }
  }
  
  void handleConnectionClose () {
    if (connected) Debug.Log("You have lost connection to the server.");
    else Debug.Log("Could not connect to server.");
    connected = false;
    handleMessage(createMessage("onWsClose", null));
  }
  
  void handleConnectionOpen () {
    Debug.Log("You have connected to the server.");
    LoadingWidgetController.current.Hide();
    connected = true;
      handleMessage(createMessage("onWsOpen", null));
  }
  
  string createMessage (string route, Hashtable data) {
    string jsonData = JsonConvert.SerializeObject(data);
    return route + delimiter + jsonData;
  }
  
  void OnApplicationQuit () {
    CancelInvoke("connect");
    ws.Close();
  }

}

}