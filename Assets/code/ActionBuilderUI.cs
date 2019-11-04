using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEngine.Events;
using LIMB;
/// <summary>
/// Creates and enqueues an Action to perform for the currently controlled Combatant through UI.
/// Used by the player to issue orders to their party.
/// </summary>
public class ActionBuilderUI : MonoBehaviour
{
    /// <summary>
    /// The BattleManager will decide the current Combatant to build an Action for.
    /// </summary>
    Combatant currentCombatant;

    /// <summary>
    /// The UI will decide what Skill to build an Action for.
    /// This should be a Skill that is available to the currentCombatant.
    /// </summary>
    public Skill currentSkill;

    /// <summary>
    /// The UI will decide which Combatant the Action will target.
    /// This should be a Combatant that the currentSkill is able to target.
    /// </summary>
    public Combatant[] selectedTargets;

    [SerializeField]
    SkillButtonLister skillLister;
    
    [SerializeField]
    CombatantsButtonLister targetLister;

    [SerializeField]
    UnityEvent onSkillSelected;

    [SerializeField]
    UnityEvent onTargetSelected;

    public Action CreateAction(){
        return new Action(currentCombatant, currentSkill, selectedTargets);
    }

    public void DisplaySkills() {
        if(skillLister.gameObject.activeInHierarchy){
            Debug.Log("Displaying skills for " + currentCombatant.ToString());
            skillLister.ListSkills(currentCombatant);
        }else{
            Debug.LogWarning("SkillLister object not active. Enable the Skill Panel first.");
        }
    }

    public void ClearSkills(){
        skillLister.Clear();
    }

    public void SelectSkill(Skill skill){
        this.currentSkill = skill;
        this.onSkillSelected.Invoke();
    }

    public void EnqueueAction() {
        Action action = new Action(currentCombatant, currentSkill, selectedTargets);
        Debug.Log("Enqueued Action: " + action.ToString());
    }

    public void EnableSkillPanel(){
        this.skillLister.gameObject.SetActive(true);
    }

    public void DisableSkillPanel(){
        this.skillLister.gameObject.SetActive(false);
    }

    public void EnablePossibleTargetsPanel(){
        this.targetLister.gameObject.SetActive(true);
    }

    public void DisablePossibleTargetsPanel(){
        this.targetLister.gameObject.SetActive(false);
    }

    public void DisplayPossibleTargets(Combatant[] lParty = null, Combatant[] rParty = null){
        if(this.currentSkill == null){
            Debug.LogError("No current Skill set!");
            return;
        }
        if(lParty == null || rParty == null)
            (lParty, rParty) = Locator.GetCombatants();
        if(lParty == null || rParty == null){
            Debug.LogError("No BattleManager set in Locator!");
            return;
        }
        if(lParty.Contains(this.currentCombatant)){
            this.targetLister.ListPossibleTargets(this.currentSkill, this.currentCombatant, lParty, rParty);
        }else if (rParty.Contains(this.currentCombatant)){
            this.targetLister.ListPossibleTargets(this.currentSkill, this.currentCombatant, rParty, lParty);
        }else{
            Debug.LogError(string.Format("Combatant {0} is not in either party!",this.currentCombatant));
        }
    }

    public void ClearPossibleTargets(){
        this.targetLister.Clear();
    }

    public void SetTargets(params Combatant[] combatants) {
        this.selectedTargets = combatants;
        this.onTargetSelected.Invoke();
        Debug.Log("Selected targets: " + string.Join<Combatant>(", ", this.selectedTargets));
    }
    
    public void SetCurrentCombatant(Combatant combatant){
        this.currentCombatant = combatant;
        Debug.Log("Current combatant: " + this.currentCombatant);
    }

    public Combatant GetCurrentCombatant(){
        return this.currentCombatant;
    }
}
