using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerListViewModel : MonoBehaviour {

  public static PlayerListViewModel current;
  GameObject playerPrefab;
  public Transform playerList;

  // Use this for initialization
  void Start () {
    current = this;
    playerPrefab = Resources.Load("Menu/Button-Friend") as GameObject;
    ClearPlayers();
  }
  
  void OnDestroy () {
    current = null;
  }
  
  public void ClearPlayers () {
    for (int i=0; i<playerList.childCount; i++) {
      GarbageCollector.Collect(playerList.GetChild(i).gameObject);
    }
  }

  public void AddPlayer (PlayerStub player) {
    GameObject playerObject = Instantiate(playerPrefab);
    playerObject.name = "Button-Friend:" + player.id;
    playerObject.GetComponentInChildren<Text>().text = player.username;
    playerObject.transform.SetParent(playerList, false);
    
  }
  
  public void RemovePlayer (PlayerStub player) {
    GameObject playerObject = GameObject.Find("Button-Friend:" + player.id);
    GarbageCollector.Collect(playerObject);
  }
}
