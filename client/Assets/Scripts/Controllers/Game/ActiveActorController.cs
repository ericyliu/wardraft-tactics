using UnityEngine;
using System.Collections.Generic;
using System.Threading;

namespace Wardraft.Game {

  public class ActiveActorController : MonoBehaviour {
  
    public ActiveActorVM AAVM;
    public ActiveActor AA;
    
    List<Tile> currentPath;
    Vector3 currentDestination;
    Enums.AnimationState state;
    
    void Start () {
      currentPath = new List<Tile>();
      currentDestination = Vector3.zero;
      state = Enums.AnimationState.Standing;
    }
    
    void Update () {
      navigate();
      AAVM.PlayAnimation(state);
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
    
    public void Select () {
      if (AA.ownerID != GameData.PlayerID) AAVM.SelectEnemy();
      else AAVM.SelectOwn();
      AA.Select();
      PlayerController.yourself.Select(this);
      if (AA is Unit) MapController.current.DisplayOptions(AA);
    }
    
    public void Deselect () {
      AAVM.Normal();
      AA.Deselect();
    }
    
    public void MoveTo (Tile tile) {
      if (AA.canMove) {
        if (Map.current.TilesInUnitMoveRange(AA as Unit).Contains(tile)) {
          List<Tile> path = new List<Tile>();
          Map.current.BuildPath(AA.position, tile, ref path);
          AA.Move(path);
          MapController.current.MoveActor(AA, tile);
          path.AddRange(currentPath);
          currentPath = path;
        }
        else {
          Debug.Log("Unit cannot move to that tile");
        }
      }
    }
    
    void navigate () {
      if (currentPath.Count > 0) {
        state = Enums.AnimationState.Moving;
        float threshold = ((float)AA.attributes.speed.current.ToDouble() * Time.deltaTime) / 5f;
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
        }
      }
      else state = Enums.AnimationState.Standing;
    }
  
  }

}