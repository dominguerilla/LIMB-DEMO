using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    /// <summary>
    /// You can consider an Effect a collection of Buffs, Damage, and a duration in which they take effect
    /// </summary>
    [CreateAssetMenu(fileName = "New Effect", menuName = "Effect", order = 53)]
    public class Effect : ScriptableObject {
        public string Name;
        public string Description;
        public int Duration;
        public StatChange[] Buffs;
        public Damage[] ProcDamage;
    }
}
