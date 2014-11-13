using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Debugger : MonoBehaviour {

	public bool testsOn;
  public bool allOn;
	public bool testMap;
  public bool testGame;
  public bool testDraft;
  public bool testTurns;

	// Use this for initialization
	void Start () {
		if (testsOn) {
			Debug.Log("Debugging..." + DateTime.Now);
			if (allOn || testMap) Tests.Map_Tests();
      if (allOn || testGame) Tests.Game_Tests();
      if (allOn || testDraft) Tests.Draft_Tests(); 
      if (allOn || testTurns) Tests.Turn_Tests(); 
		}
	}
  
}
