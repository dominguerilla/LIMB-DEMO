using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using UnityEngine.UI;

/// <summary>
/// Represents one or more combatants in the UI.
/// </summary>
/// <notes>TODO: DEFINITE duplication of SkillButton.cs</notes>
public class CombatantsButton : MonoBehaviour
{
    [SerializeField]
    Combatant[] combatants;

    public void SetCombatants(params Combatant[] combatants){
        this.combatants = combatants;
        string combatantsName = "";
        if (this.combatants.Length == 1){
            combatantsName = this.combatants[0].ToString();
        }else{
            combatantsName += this.combatants[0];
            for(int i = 1; i < this.combatants.Length; i++){
                combatantsName += ", " + this.combatants[i]; 
            }
        }
        Text text = GetComponentInChildren<Text>();
        text.text = combatantsName;
    }

    public Combatant[] GetCombatants(){
        return this.combatants;
    }

    public void SetButtonTarget(){

    }

}
