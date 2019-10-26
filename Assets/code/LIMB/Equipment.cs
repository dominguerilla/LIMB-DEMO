using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    public class Equipment : ScriptableObject {
        public List<StatChange> StatBuffs;

        public void AddBuffs(params StatChange[] buffs) {
            if(StatBuffs == null)
                StatBuffs = new List<StatChange>();

            foreach(StatChange buff in buffs){
                StatBuffs.Add(buff);
            }
        }

        /// <summary>
        /// Returns all the buffs pertaining to a given stat.
        /// </summary>
        public List<StatChange> GetBuffs(Stats.STAT stat) {
            if(this.StatBuffs == null)
                this.StatBuffs = new List<StatChange>();

            List<StatChange> buffs = new List<StatChange>();
            foreach(StatChange buff in this.StatBuffs) {
                if(buff.Stat == stat) {
                    buffs.Add(buff);
                }
            }

            return buffs;
        }

        public void SetName(string name){
            this.name = name;
        }
    }
}
