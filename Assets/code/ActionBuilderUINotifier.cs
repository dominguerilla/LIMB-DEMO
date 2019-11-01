using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;

/// <summary>
/// Triggers BOLT CustomEvents based on UnityEvents called from ActionBuilderUI.
/// </summary>
public class ActionBuilderUINotifier : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSkillSelected() {
        CustomEvent.Trigger(this.gameObject, "On Skill Selected");
    }
}
