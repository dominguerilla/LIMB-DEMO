using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;

/// <summary>
/// Triggers BOLT CustomEvents based on UnityEvents called from ActionBuilderUI.
/// </summary>
public class ActionBuilderUINotifier : MonoBehaviour
{
    public void OnSkillSelected() {
        CustomEvent.Trigger(this.gameObject, "On Skill Selected");
    }
}
