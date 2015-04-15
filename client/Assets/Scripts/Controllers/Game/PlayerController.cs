using UnityEngine;
using System.Collections;
using Wardraft.UI;

namespace Wardraft.Game {

  public class PlayerController : MonoBehaviour {
  
    public Player                   player;
    public System.Object            selected;
    public GameUIController         GUIC;
    
    public Ability                  primedAbility;
    public ActiveActor              abilitySource;
    public bool                     isAbilityPrimed;
    
    public static PlayerController  yourself;
    
    void Start () {
      GUIC = GameObject.Find("UI").GetComponent<GameUIController>();
    }
    
    public void Select (System.Object toSelect) {
      if (toSelect != selected && selected != null) {
        if (selected is ActiveActorController) (selected as ActiveActorController).Deselect();
        if (selected is TileController) (selected as TileController).Deselect();
      }
      selected = toSelect;
      GUIC.ShowSelectedInfo(toSelect);
    }
    
    public void PrimeAbility (Ability ability, ActiveActor source) {
      if (ability.target != Enums.SpellTarget.Self) {
        Debug.Log("Priming ability: " + AbilityList.abilities[ability.code]);
        primedAbility = ability;
        abilitySource = source;
        isAbilityPrimed = true;
      }
      else {
        Debug.Log("Using ability: " + AbilityList.abilities[ability.code]);
        ability.Invoke(null, null);
      }
    }
    
    public void UnprimeAbility () {
      isAbilityPrimed = false;
    }
  
    public void UseAbility (ActiveActor aa_target = null, Tile tile_target = null) {
      if (primedAbility.target == Enums.SpellTarget.Target && aa_target == null) {
        Debug.Log("Must cast target ability on target");
        UnprimeAbility();
        return;
      }
      Debug.Log("Using ability: " + AbilityList.abilities[primedAbility.code]);
      primedAbility.Invoke(aa_target, tile_target);
      UnprimeAbility();
    }
  
  }

}
