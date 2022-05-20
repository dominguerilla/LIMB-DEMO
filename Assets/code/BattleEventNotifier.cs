using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.VisualScripting;

/// <summary>
/// Triggers BOLT CustomEvents based on UnityEvents called from BattleManager.
/// </summary>
public class BattleEventNotifier : MonoBehaviour
{
    public void BattleStart() {
        CustomEvent.Trigger(this.gameObject, "Battle Start");
    }

    public void OnActionExecuted(){
        CustomEvent.Trigger(this.gameObject, "On Action Executed");
    }
}
