using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bolt;

/// <summary>
/// Triggers BOLT CustomEvents based on UnityEvents called from BattleManager.
/// </summary>
[RequireComponent(typeof(BattleManager))]
public class BattleEventNotifier : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BattleStart() {
        CustomEvent.Trigger(this.gameObject, "Battle Start");
    }
}
