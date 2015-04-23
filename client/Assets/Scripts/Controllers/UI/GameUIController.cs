using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Wardraft.Game;

namespace Wardraft.UI {

  public class GameUIController : MonoBehaviour {
  
    GameObject  SelectedActorInfo;
    GameObject  SelectedTileInfo;
    GameObject  AbilityPanel;
    GameObject  PlayerInfo;
    GameObject  BuildListPanel;
    
    public static GameUIController current;
    
    void Start () {
      SelectedActorInfo = GameObject.Find("SelectedActorInfo");
      SelectedTileInfo = GameObject.Find("SelectedTileInfo");
      PlayerInfo = GameObject.Find("PlayerInfo");
      AbilityPanel = GameObject.Find("AbilityPanel");
      BuildListPanel = GameObject.Find("BuildListPanel");
      BuildListPanel.SetActive(false);
      AbilityPanel.SetActive(false);
      HideSelectedInfo();
      current = this;
    }
    
    public void ShowSelectedInfo (System.Object toShow) {
      if (toShow is ActiveActorController) {
        ActiveActor aa = (toShow as ActiveActorController).AA;
        HideSelectedInfo();
        SelectedActorInfo.SetActive(true);
        string owned = "";
        string state = "";
        if (aa.ownerID == GameData.PlayerID) owned = " (Owned)";
        if (aa.state == Enums.ActiveActorState.Dead) state = " (Dead)";
        clearLabels();
        changeLabel(SelectedActorInfo, "LabelName", ActorList.codes[aa.code] + owned + state);
        if (aa.state == Enums.ActiveActorState.Alive) {
          if (aa.attributes.health.max.ToInt() != 0) {
            changeLabel(SelectedActorInfo, "LabelHealth", string.Format("Health: {0}/{1}", 
                                                                        aa.attributes.health.current.ToInt(),
                                                                        aa.attributes.health.max.ToInt())); }
          if (aa.attributes.damage.max.ToInt() != 0) {
            changeLabel(SelectedActorInfo, "LabelDamage", string.Format("Damage: {0}",
                                                                        aa.attributes.damage.current.ToInt())); }
          changeLabel(SelectedActorInfo, "LabelArmor", string.Format("Armor: {0}", 
                                                                     aa.attributes.armor.current.ToInt()));
          if (aa.attributes.speed.max.ToInt() != 0) {
            changeLabel(SelectedActorInfo, "LabelSpeed", string.Format("Movement: {0}/{1}",
                                                                       aa.attributes.speed.current.ToInt(),
                                                                       aa.attributes.speed.max.ToInt())); }
          if (aa.attributes.mana.max.ToInt() != 0) {
            changeLabel(SelectedActorInfo, "LabelMana", string.Format("Mana: {0}/{1}", 
                                                                      aa.attributes.mana.current.ToInt(),
                                                                      aa.attributes.mana.max.ToInt())); }
          if (aa.attributes.damage.max.ToInt() != 0) {
            changeLabel(SelectedActorInfo, "LabelAttackRange", string.Format("A. Range: {0}", 
                                                                             aa.attributes.attackRange.current)); }
        
          if (aa.ownerID == GameData.PlayerID) {
            showAbilities(aa);
            if (aa is Building) showBuildList(toShow as ActiveActorController);
          }
        }
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
      hideAbilities();
      hideBuildList();
    }
    
    public void UpdateCurrentPlayerInfo (Player player) {
      Game.Game game = Game.Game.current;
      changeLabel(PlayerInfo, "LabelGold", "Gold: " + player.gold);
      changeLabel(PlayerInfo, "LabelPopulation", "Pop: " + player.population.current + "/" + player.population.max);
      changeLabel(PlayerInfo, "LabelTurn", string.Format("Turn {0}: {1}", 
                                                         game.turn.number,
                                                         game.GetPlayer(game.turn.playerId).name));
    }
    
    void clearLabels () {
      changeLabel(SelectedActorInfo, "LabelHealth", "");
      changeLabel(SelectedActorInfo, "LabelDamage", "");
      changeLabel(SelectedActorInfo, "LabelSpeed", "");
      changeLabel(SelectedActorInfo, "LabelArmor", "");
      changeLabel(SelectedActorInfo, "LabelMana", "");
      changeLabel(SelectedActorInfo, "LabelAttackRange", "");
    }
    
    void changeLabel (GameObject panel, string name, string text) {
      GameObject textObject = panel.transform.FindChild(name).gameObject;
      changeText(textObject, text);
    }
    
    void changeText (GameObject textObject, string text) {
      textObject.GetComponent<Text>().text = text;
    }
    
    void showAbilities (ActiveActor aa) {
      AbilityPanel.SetActive(true);
      GameObject garbage = GameObject.Find("Garbage");
      for (int i=0; i<AbilityPanel.transform.childCount; i++) {
        AbilityPanel.transform.GetChild(i).SetParent(garbage.transform);
      }
      for (int i=0; i<aa.abilities.Count; i++) {
        Ability ability = aa.abilities[i];
        float position = (60 * i) + (10 * (i + 1));
        GameObject buttonObject = Instantiate(ResourceLoader.current.ui["AbilityButton"]) as GameObject;
        buttonObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position, 10f);
        changeLabel(buttonObject, "Label", AbilityList.abilities[ability.code]);
        buttonObject.transform.SetParent(AbilityPanel.transform, false);
        buttonObject.name = "AbilityButton:" + ability.code;
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(() => {
          PlayerController.yourself.PrimeAbility(ability, aa);
        });
        
      }
    }
    
    void hideAbilities () {
      AbilityPanel.SetActive(false);
    }
    
    void showBuildList (ActiveActorController buildingController) {
      Building building = buildingController.AA as Building;
      BuildListPanel.SetActive(true);
      GameObject garbage = GameObject.Find("Garbage");
      for (int i=0; i<BuildListPanel.transform.childCount; i++) {
        BuildListPanel.transform.GetChild(i).SetParent(garbage.transform);
      }
      for (int i=0; i<building.buildList.Length; i++) {
        int code = building.buildList[i];
        string name = ActorList.codes[code].name;
        float positionY = -(10 * ((i%3) + 1)) - ((i%3) * 60);
        float positionX = (10 * ((i/3) + 1)) + ((i/3) * 60);
        GameObject buttonObject = Instantiate(ResourceLoader.current.ui["BuildListButton"]) as GameObject;
        buttonObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(positionX, positionY);
        changeLabel(buttonObject, "Label", name);
        buttonObject.transform.SetParent(BuildListPanel.transform, false);
        buttonObject.name = string.Format("BuildListButton:{0}", code);
        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(() => {
          buildingController.Build(code);
        });
      }
    }
    
    void hideBuildList () {
      BuildListPanel.SetActive(false);
    }
  
  }

}