using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LIMB;
public class BattleTester : MonoBehaviour {

    public bool DebugMode;

    [SerializeField]
    NPCParty leftParty;
    [SerializeField]
    NPCParty rightParty;

    BattleManager bManager;

    private void Awake() {
        bManager = GameObject.FindObjectOfType<BattleManager>();    
    }

    public void StartBattle(){
        bManager.StartBattle(leftParty, rightParty);
    }

    public void EndBattle(){
        bManager.EndBattle();
    }

}
