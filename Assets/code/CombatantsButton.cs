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
        Text text = GetComponentInChildren<Text>();
        text.text = this.combatants[0].ToString();
    }

    public Combatant[] GetCombatants(){
        return this.combatants;
    }


}
