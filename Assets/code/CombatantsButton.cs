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
    Skill skill;

    // Don't think this is the best way to do it...
    // maybe inject this into SkillButton instead?
    ActionBuilderUI ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindObjectOfType<ActionBuilderUI>();    
    }

    public void SetSkill(Skill skill){
        this.skill = skill;
        Text text = GetComponentInChildren<Text>();
        text.text = skill.actionName;
    }

    public Skill GetSkill(){
        return this.skill;
    }

    public void SetActionBuilderUISkill(){
        ui.SelectSkill(skill);
    }

}
