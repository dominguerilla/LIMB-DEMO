using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Combatant currentCombatant;

    /// <summary>
    /// The UI will decide what Skill to build an Action for.
    /// This should be a Skill that is available to the currentCombatant.
    /// </summary>
    public Skill currentSkill;

    /// <summary>
    /// The UI will decide which Combatant the Action will target.
    /// This should be a Combatant that the currentSkill is able to target.
    /// </summary>
    public Combatant possibleTargets;

    [SerializeField]
    SkillButtonLister skillLister;
    /*
    [SerializeField]
    TargetListerUI targetLister;
    */
    // Start is called before the first frame update
    void Start()
    {
    }

    public void DisplaySkills() {
        if(skillLister.gameObject.activeInHierarchy){
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
    }

    /*
    public void DisplayTargets() {
        targetLister.ListTargets(currentSkill);
    }
    */
    public void EnqueueAction() {
        Action action = new Action(currentCombatant, currentSkill, possibleTargets);
        Debug.Log("Enqueued Action: " + action.ToString());
    }

    public void EnableSkillPanel(){
        this.skillLister.gameObject.SetActive(true);
    }

    public void DisableSkillPanel(){
        this.skillLister.gameObject.SetActive(false);
    }
}
