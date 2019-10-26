using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    public class FieldManager : MonoBehaviour {
        
        /// <summary>
        /// Only needed if FieldManager.StartBattle() is being called directly
        /// </summary>
        public GameObject battleScenePrefab;

        public bool DebugMode;
        BattleManager bm;

        // Use this for initialization
        void Awake () {
            bm = GetComponent<BattleManager>();	
        }
        
        public void StartBattle(){
            throw new System.NotImplementedException();
        }
        
        public void EndBattle(){
            bm.EndBattle();
        }

        Combatant[] GetLeftParty(){
            int count = 5;
            Combatant[] party = new Combatant[count];
            for (int i = 0; i < count; i++){
                Combatant combatant = new Combatant(null);
                party[i] = combatant;
            }
            Debug.Log("Left party: " + party.Length);
            return party;
        }

        Combatant[] GetRightParty(){
            int count = 4;
            Combatant[] party = new Combatant[count];
            for (int i = 0; i < count; i++){
                Combatant combatant = new Combatant(null);
                party[i] = combatant;
            }
            Debug.Log("Right party: " + party.Length);
            return party;
        }
    }
}
