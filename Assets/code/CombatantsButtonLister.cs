using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using LIMB;
using System;

/// <summary>
/// Populates a UI Panel with UI Buttons representing Combatants.
/// </summary>
/// <notes>TODO: DEFINITE duplication of SkillButtonLister.cs</notes>
public class CombatantsButtonLister : MonoBehaviour
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

    List<Combatant> possibleTargets;
    Stack<CombatantsButton> buttonObjectPool;
    List<CombatantsButton> activeButtons;

    void Awake()
    {
        buttonObjectPool = new Stack<CombatantsButton>();
        activeButtons = new List<CombatantsButton>();
        for(int i = 0; i < MAX_BUTTON_NUM; i++){
            GameObject button = GameObject.Instantiate<GameObject>(buttonPrefab);
            button.transform.SetParent(this.panelTransform);

            CombatantsButton objButton = button.GetComponent<CombatantsButton>();
            if(!objButton){
                Debug.LogError("No CombatantsButton component found in buttonPrefab.");
                return;
            }
            objButton.gameObject.SetActive(false);
            buttonObjectPool.Push(objButton);
        }
    }

    public void ListPossibleTargets(Skill skill, Combatant user, Combatant[] alliedParty, Combatant[] otherParty){
        //TODO: Check skill to populate possibleTargets
        Debug.Log(string.Format("{0} targetable: {1}", skill.name, Enum.GetName(typeof(Skill.TARGETABLE), skill.targetable)));
        if(skill.targetable == Skill.TARGETABLE.ALLIES){
            possibleTargets = new List<Combatant>(alliedParty);
        }else if (skill.targetable == Skill.TARGETABLE.ENEMIES){
            possibleTargets = new List<Combatant>(otherParty);
        }else{
            possibleTargets = new List<Combatant>(alliedParty.Concat<Combatant>(otherParty));
        }

        int targetNum = Mathf.Min(MAX_BUTTON_NUM, possibleTargets.Count);
        
        if(skill.targetType == Skill.TARGET_TYPE.SINGLE){
            for(int i = 0; i < targetNum; i++){
                CombatantsButton combatantsButton = PopButton();
                if(combatantsButton){
                    CreateButton(combatantsButton, possibleTargets[i]);
                }else{
                    Debug.LogError("Out of CombatantsButtons from pool!");
                }
            }
        }else{
            CombatantsButton combatantsButton = PopButton();
            if(combatantsButton){
                CreateButton(combatantsButton, possibleTargets.ToArray());
            }else{
                Debug.LogError("Out of CombatantsButtons from pool!");
            }
        }
    }

    void CreateButton(CombatantsButton combatantsButton, params Combatant[] combatants){
        combatantsButton.SetCombatants(combatants);
        Button buttonComponent = combatantsButton.gameObject.GetComponent<Button>();
        buttonComponent.onClick.AddListener(combatantsButton.SetButtonTarget);
        combatantsButton.gameObject.SetActive(true);
        buttonComponent.Select();
    }

    public void Clear(){
        if(activeButtons != null){
            foreach(CombatantsButton button in activeButtons){
                button.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
                button.gameObject.SetActive(false);
                buttonObjectPool.Push(button);
            }
            activeButtons.Clear();
        }else{
            Debug.LogWarning("activeButtons is null!");
        }
    }

    CombatantsButton PopButton(){
        CombatantsButton button = buttonObjectPool.Pop();
        activeButtons.Add(button);
        return button;   
    }
}
