using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using LIMB;


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
            button.transform.SetParent(this.transform);

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
        throw new System.Exception("Not implemented!");
        int skillNum = Mathf.Min(MAX_BUTTON_NUM, possibleTargets.Count);
        
        for(int i = 0; i < skillNum; i++){
            CombatantsButton combatantsButton = PopSkillButton();
            if(combatantsButton){
                Debug.Log("CombatantsButton found!");
                /*
                combatantsButton.SetCombatants(possibleTargets[i]);
                Button buttonComponent = combatantsButton.gameObject.GetComponent<Button>();
                buttonComponent.onClick.AddListener(combatantsButton.SetActionBuilderUISkill);
                combatantsButton.gameObject.SetActive(true);
                */
            }else{
                Debug.LogError("Out of SkillButtons from pool!");
            }
        }
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

    CombatantsButton PopSkillButton(){
        CombatantsButton button = buttonObjectPool.Pop();
        activeButtons.Add(button);
        return button;   
    }
}
