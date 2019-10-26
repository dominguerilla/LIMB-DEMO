using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using UnityEngine.UI;
public class SelectionButton : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
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
