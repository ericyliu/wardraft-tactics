using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingWidgetController : MonoBehaviour {

  public static LoadingWidgetController current;
  
  void Awake () {
    current = this;
  }
  
  void Start () {
    current = this;
    Hide();
  }
  
  public void Display (string text) {
    gameObject.SetActive(true);
    gameObject.transform.Find("LoadingPanel/Text").GetComponent<Text>().text = text;
  }
  
  public void Hide () {
    gameObject.SetActive(false);
  }

}
