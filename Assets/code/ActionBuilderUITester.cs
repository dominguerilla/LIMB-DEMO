using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using System;
/// <summary>
/// Used to debug the ActionBuilderUI functionality.
/// </summary>
public class ActionBuilderUITester : MonoBehaviour
{
    [SerializeField]
    CombatantData currentCombatant;

    [SerializeField]
    Skill currentSkill;

    [SerializeField]
    CombatantData currentTarget;

    [SerializeField]
    NPCParty alliedParty;

    [SerializeField]
    NPCParty otherParty;

    ActionBuilderUI ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindObjectOfType<ActionBuilderUI>();
        if(!ui){
            Debug.LogError("No ActionBuilderUI found in scene.");
            Destroy(this);
        }
        Debug.Log("ActionBuilderUI found in " + ui.gameObject);
    }
    
    public void ShowSkills(){
        ui.SetCurrentCombatant(new Combatant(currentCombatant));
        ui.EnableSkillPanel();
        ui.DisplaySkills();
    }

    public void HideSkills(){
        ui.ClearSkills();
        ui.DisableSkillPanel();
    }

    public void ShowPossibleTargets(){
        ui.EnablePossibleTargetsPanel();

        int partyLength = alliedParty.GetData().Length;
        Combatant[] fullAlliedParty = new Combatant[partyLength + 1];
        Array.Copy(CreateCombatants(alliedParty), fullAlliedParty, partyLength);
        fullAlliedParty[partyLength] = ui.GetCurrentCombatant();

        ui.DisplayPossibleTargets(
            lParty: fullAlliedParty,
            rParty: CreateCombatants(otherParty)
            );
    }

    Combatant[] CreateCombatants(NPCParty party){
        CombatantData[] data = party.GetData();
        Combatant[] combatants = new Combatant[data.Length];
        for(int i = 0; i < combatants.Length; i++){
            combatants[i] = new Combatant(data[i]);
        }
        return combatants;
    }

    public void HidePossibleTargets(){
        ui.ClearPossibleTargets();
        ui.DisablePossibleTargetsPanel();
    }
}
