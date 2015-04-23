using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Wardraft.UI;

namespace Wardraft.Game {

  public class ActiveActorController : MonoBehaviour {
  
    public ActiveActorVM AAVM;
    public ActiveActor AA;
    
    public List<Tile> currentPath;
    Vector3 currentDestination;
    ActiveActorController target;
    Enums.AnimationState state;
    
    void Start () {
      currentPath = new List<Tile>();
      currentDestination = Vector3.zero;
      AAVM.PlayAnimation(Enums.AnimationState.Standing);
    }
    
    void Update () {
      navigate();
    }
    
    public void HoverOn () {
      if (PlayerController.yourself.selected != this) {
        if (AA.ownerID != GameData.PlayerID) AAVM.MouseoverEnemy();
        else AAVM.MouseoverOwn();
      }
    }
    
    public void HoverExit () {
      if (PlayerController.yourself.selected != this) {
        AAVM.Normal();
      }
    }
    
    public void Click (BaseEventData data) {
      if (data is PointerEventData) {
        PointerEventData.InputButton button = (data as PointerEventData).button;
        if (button == PointerEventData.InputButton.Left) {
          if (PlayerController.yourself.isAbilityPrimed) {
            PlayerController.yourself.UseAbility(AA, AA.position);
            return;
          }
          onSelected();
        }
        else if (button == PointerEventData.InputButton.Right) {
          if (PlayerController.yourself.isAbilityPrimed) {
            PlayerController.yourself.UnprimeAbility();
            return;
          }
          if (PlayerController.yourself.selected is ActiveActorController) {
            if ((PlayerController.yourself.selected as ActiveActorController).AA.canAttack) onAttacked();
          }
        }
      }
    }
    
    public void Deselect () {
      AAVM.Normal();
      AA.Deselect();
    }
    
    public void MoveTo (Tile tile) {
      if (!owned()) return;
      if (AA.canMove) {
        if (Map.current.TilesInUnitMoveRange(AA as Unit).Contains(tile)) {
          List<Tile> path = new List<Tile>();
          Map.current.BuildPath(AA.position, tile, ref path);
          AA.Move(path);
          MapController.current.MoveActor(AA, tile);
          path.AddRange(currentPath);
          currentPath = path;
        }
      }
    }
    
    public void Attack (ActiveActorController target) {
      if (AA.canAttack) {
        if (owned() && !target.owned() && target.AA.state == Enums.ActiveActorState.Alive) {
          if (Map.current.IsWithinAttackRange(AA, target.AA)) {
            List<Tile> path = new List<Tile>();
            Map.current.BuildPath(AA.position, target.AA.position, ref path);
            path.RemoveAt(0);
            MapController.current.MoveActor(AA,path[0]);
            AA.Move(path);
            path.AddRange(currentPath);
            currentPath = path;
            this.target = target;
          }
        }
      }
    }
    
    public void DealDamage () {
      AA.Attack(target.AA);
      target.TakeDamage();
    }
    
    public void FinishAttack () {
      target = null;
    }
    
    public void TakeDamage () {
      AAVM.PlayAnimation(Enums.AnimationState.TakeDamage);
      if (AA.state == Enums.ActiveActorState.Dead) {
        Debug.Log(string.Format("{0} has been destroyed.", ActorList.codes[AA.code]));
        AAVM.PlayAnimation(Enums.AnimationState.Dying);
      }
      GameUIController.current.ShowSelectedInfo(PlayerController.yourself.selected);
    }
    
    public void OnDeathFinish () {
      Game.current.RemoveActor(AA);
      gameObject.transform.SetParent(GameObject.Find("Garbage").transform);
    }
    
    public void Build (int code) {
      if (AA is Building) {
        Debug.Log(ActorList.codes[code].cost);
        Debug.Log(PlayerController.yourself.player.gold);
        if (ActorList.codes[code].cost > PlayerController.yourself.player.gold) {
          Tile tile = (AA as Building).rallyPoint;
          if (tile == null) {
            Debug.Log("No rally point set.");
            return;
          }
          Debug.Log ("hi");
          Actor actor = MapController.current.CreateActor(code, tile, AA.ownerID);
          if (actor != null) {
            PlayerController.yourself.player.gold -= ActorList.codes[code].cost;
            Debug.Log(string.Format("Player {0} created a {1} at [{2},{3}].", AA.ownerID, ActorList.codes[code].name, tile.position.X, tile.position.Y));
            GameUIController.current.UpdateCurrentPlayerInfo(PlayerController.yourself.player);
          }
          else {
            Debug.Log("No space to create new unit.");
          }
        }
      }
    }
    
    void onSelected () {
      if (AA.ownerID != GameData.PlayerID) AAVM.SelectEnemy();
      else AAVM.SelectOwn();
      AA.Select();
      PlayerController.yourself.Select(this);
      if (owned()) MapController.current.DisplayOptions(AA);
    }
    
    void onAttacked () {
      ActiveActorController source = PlayerController.yourself.selected as ActiveActorController;
      source.Attack(this);
    }
    
    bool owned () {
      return AA.ownerID == GameData.PlayerID;
    }
    
    void navigate () {
      if (currentPath.Count > 0) {
        AAVM.PlayAnimation(Enums.AnimationState.Moving);
        float threshold = ((float)AA.attributes.speed.max.ToDouble() * Time.deltaTime) / 5f;
        if (currentDestination == Vector3.zero) {
          Tile tile = currentPath[currentPath.Count-1];
          GameObject tileObject = GameObject.Find("Tile:" + tile.position.X + "," + tile.position.Y);
          currentDestination = new Vector3(tileObject.transform.position.x, tileObject.transform.position.y + .5f, tileObject.transform.position.z);
        }
        if (Vector3.Distance(currentDestination, transform.position) > threshold) {
          AAVM.GoTowards(currentDestination, threshold);
        }
        else {
          transform.position = currentDestination;
          currentPath.RemoveAt(currentPath.Count-1);
          currentDestination = Vector3.zero;
          if (currentPath.Count == 0) AAVM.PlayAnimation(Enums.AnimationState.Standing);
        }
      }
      else {
        if (target != null) {
          AAVM.Face(target.transform.position);
          AAVM.PlayAnimation(Enums.AnimationState.Attacking);
        }
      }
    }
  
  }

}