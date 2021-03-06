﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using LIMB;


/// <summary>
/// Populates a UI Panel with UI Buttons representing an object.
/// </summary>
public class SkillButtonLister : MonoBehaviour
{
    ///
    /// I'm thinking of showing only 4 buttons at a time.
    /// To see more buttons than that, you will need to scroll 4 buttons at a time.
    /// This might get annoying when you have 20+ objects though...
    /// 
    private static int MAX_BUTTON_NUM = 4;
    
    [SerializeField]
    Transform panelTransform;

    /// <summary>
    /// The UI Button prefab that represents a Skill.
    /// </summary>
    [SerializeField]
    GameObject buttonPrefab;

    List<Skill> currentSkills;
    Stack<SkillButton> buttonObjectPool;
    List<SkillButton> activeButtons;

    void Awake()
    {
        buttonObjectPool = new Stack<SkillButton>();
        activeButtons = new List<SkillButton>();
        for(int i = 0; i < MAX_BUTTON_NUM; i++){
            GameObject button = GameObject.Instantiate<GameObject>(buttonPrefab);
            button.transform.SetParent(this.panelTransform);

            SkillButton objButton = button.GetComponent<SkillButton>();
            if(!objButton){
                Debug.LogError("No SelectionButton component found in skillButtonPrefab.");
                return;
            }
            objButton.gameObject.SetActive(false);
            buttonObjectPool.Push(objButton);
        }
    }

    /// <summary>
    /// Creates Buttons for the first four skills that the Combatant has.
    /// </summary>
    /// <param name="combatant"></param>
    public void ListSkills(Combatant combatant, Skill.MENU_CATEGORY category = Skill.MENU_CATEGORY.NONE){
        currentSkills = combatant.GetSkills();
        if (currentSkills == null)
        {
            Debug.LogError(string.Format("Combatant {0} has no set skills!", combatant.GetName()));
            return;
        }
        if (category != Skill.MENU_CATEGORY.NONE)
        {
            IEnumerable<Skill> filtered_skills = currentSkills.Where( (x) => 
            {
                // I could replace this with a single boolean expression,
                // but this looks easier to read.
                if (x != null) {
                    return x.category == category;
                }
                return false;
            });
            if (filtered_skills.Count<Skill>() == 0)
            {
                Debug.LogWarning(string.Format("Combatant {0} has no {1} skills!", combatant.GetName(), category));
                return;
            }
            else
            {
                currentSkills = filtered_skills.ToList<Skill>();
            }
        }
        int skillNum = Mathf.Min(MAX_BUTTON_NUM, currentSkills.Count);
        
        for(int i = 0; i < skillNum; i++){
            SkillButton skillButton = PopSkillButton();
            if(skillButton){
                skillButton.SetSkill(currentSkills[i]);
                Button buttonComponent = skillButton.gameObject.GetComponent<Button>();
                buttonComponent.onClick.AddListener(skillButton.SetActionBuilderUISkill);
                skillButton.gameObject.SetActive(true);
                buttonComponent.Select();
            }else{
                Debug.LogError("Out of SkillButtons from pool!");
            }
        }
    }

    public void Clear(){
        if(activeButtons != null){
            foreach(SkillButton button in activeButtons){
                button.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                button.gameObject.SetActive(false);
                buttonObjectPool.Push(button);
            }
            activeButtons.Clear();
        }else{
            Debug.LogWarning("activeButtons is null!");
        }
    }

    SkillButton PopSkillButton(){
        SkillButton button = buttonObjectPool.Pop();
        activeButtons.Add(button);
        return button;   
    }
}
