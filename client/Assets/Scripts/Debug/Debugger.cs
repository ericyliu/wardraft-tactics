using UnityEngine;
using System;

public class Debugger : MonoBehaviour {

  public bool testsOn;
  public bool allOn;
  public bool testMap;
  public bool testGame;
  public bool testDraft;
  public bool testTurns;
  public bool testBuffs;
  public bool testPathing;
  public bool testAbilities;
  public bool testAttack;

  // Use this for initialization
  void Start () {
    if (testsOn) {
      Debug.Log("Debugging..." + DateTime.Now + "\n");
      if (allOn || testMap) Tests.Map_Tests();
      if (allOn || testGame) Tests.Game_Tests();
      if (allOn || testDraft) Tests.Draft_Tests();
      if (allOn || testTurns) Tests.Turn_Tests();
      if (allOn || testBuffs) Tests.Buff_Tests();
      if (allOn || testPathing) Tests.Pathing_Tests();
      if (allOn || testAbilities) Tests.Ability_Tests();
      if (allOn || testAttack) Tests.Attack_Tests();
    }
  }

}
