using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.VisualScripting;

/// <summary>
/// Triggers BOLT CustomEvents based on UnityEvents called from ActionBuilderUI.
/// </summary>
public class ActionBuilderUINotifier : MonoBehaviour
{
    public void OnSkillSelected() {
        CustomEvent.Trigger(this.gameObject, "On Skill Selected");
    }

    public void OnTargetSelected(){
        CustomEvent.Trigger(this.gameObject, "On Target Selected");
    }
}
