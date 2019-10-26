using System.Collections;
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
            button.transform.SetParent(this.transform);

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
    public void ListSkills(Combatant combatant){
        currentSkills = combatant.GetSkills();
        int skillNum = Mathf.Min(MAX_BUTTON_NUM, currentSkills.Count);
        
        for(int i = 0; i < skillNum; i++){
            SkillButton skillButton = PopSkillButton();
            if(skillButton){
                skillButton.SetSkill(currentSkills[i]);
                Button buttonComponent = skillButton.gameObject.GetComponent<Button>();
                buttonComponent.onClick.AddListener(skillButton.SetActionBuilderUISkill);
                skillButton.gameObject.SetActive(true);
            }else{
                Debug.LogError("Out of SkillButtons from pool!");
            }
        }
    }

    public void Clear(){
        foreach(SkillButton button in activeButtons){
            button.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
            buttonObjectPool.Push(button);
        }
        activeButtons.Clear();
    }

    SkillButton PopSkillButton(){
        SkillButton button = buttonObjectPool.Pop();
        activeButtons.Add(button);
        return button;   
    }
}
