using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    [CreateAssetMenu(fileName = "New Party", menuName = "Preset Party", order = 52)]
    public class NPCParty : ScriptableObject {
        [SerializeField]
        CombatantData[] members;

        public CombatantData[] GetData(){
            return members;
        }
    }
}
