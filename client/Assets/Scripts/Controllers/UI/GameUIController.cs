using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Wardraft.Game;

namespace Wardraft.UI {

  public class GameUIController : MonoBehaviour {
  
    GameObject SelectedActorInfo;
    GameObject SelectedTileInfo;
    GameObject PlayerInfo;
    
    void Start () {
      SelectedActorInfo = GameObject.Find("SelectedActorInfo");
      SelectedTileInfo = GameObject.Find("SelectedTileInfo");
      PlayerInfo = GameObject.Find("PlayerInfo");
      HideSelectedInfo();
    }
    
    public void ShowSelectedInfo (System.Object toShow) {
      if (toShow is ActiveActorController) {
        ActiveActor aa = (toShow as ActiveActorController).AA;
        HideSelectedInfo();
        SelectedActorInfo.SetActive(true);
        string owned = "";
        if (aa.ownerID == GameData.PlayerID) owned = " (Owned)";
        changeLabel(SelectedActorInfo, "LabelName", Actors.codes[aa.code] + owned);
        changeLabel(SelectedActorInfo, "LabelHealth", "Health: " + aa.attributes.health.current.ToInt() + "/" + aa.attributes.health.max.ToInt());
        changeLabel(SelectedActorInfo, "LabelDamage", "Damage: " + aa.attributes.damage.current.ToInt());
        changeLabel(SelectedActorInfo, "LabelArmor", "Armor: " + aa.attributes.armor.current.ToInt());
        changeLabel(SelectedActorInfo, "LabelSpeed", "Speed: " + aa.attributes.speed.current.ToInt());
        changeLabel(SelectedActorInfo, "LabelMana", "Mana: " + aa.attributes.mana.current.ToInt() + "/" + aa.attributes.mana.max.ToInt());
        changeLabel(SelectedActorInfo, "LabelAttackRange", "A. Range: " + aa.attributes.attackRange.current);
      }
      if (toShow is TileController) {
        Tile tile = (toShow as TileController).tile;
        HideSelectedInfo();
        SelectedTileInfo.SetActive(true);
        int layer = (int) Enums.Layers.Ground;
        int code = tile.terrains[layer].code;
        changeLabel(SelectedTileInfo, "LabelName", Terrains.codes[code]);
      }
    }
    
    public void HideSelectedInfo () {
      SelectedActorInfo.SetActive(false);
      SelectedTileInfo.SetActive(false);
    }
    
    public void UpdateCurrentPlayerInfo (Player player) {
      changeLabel(PlayerInfo, "LabelGold", "Gold: " + player.gold);
      changeLabel(PlayerInfo, "LabelPopulation", "Pop: " + player.population.current + "/" + player.population.max);
    }
    
    void changeLabel (GameObject panel, string name, string text) {
      GameObject textObject = panel.transform.FindChild(name).gameObject;
      changeText(textObject, text);
    }
    
    void changeText (GameObject textObject, string text) {
      textObject.GetComponent<Text>().text = text;
    }
  
  }

}