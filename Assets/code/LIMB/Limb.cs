using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    [System.Serializable]
    public class Limb{

        [SerializeField]
        string name;

        [Tooltip("Uses the combatant's base stats by default.")]
        [SerializeField]
        List<StatValue> limbStats;

        [SerializeField]
        List<Equipment> equipment;

        static float DEFAULT_STAT = 1f;

        public Limb() { }

        public Limb(string name, params StatValue[] limbStats) {
            this.name = name;
            this.limbStats = new List<StatValue>(limbStats);
            equipment = new List<Equipment>();
        }

        public void Equip(Equipment equipment) {
            this.equipment.Add(equipment);
        }

        public void DeEquip(Equipment equipment){
            this.equipment.Remove(equipment);
        }

        public bool IsEquipped(Equipment equipment){
            return this.equipment.Contains(equipment);
        }

        public float GetBuffedStatus(Stats.STAT stat, float baseStat) {
            float totalStats = baseStat + GetStat(stat);

            float percentageDelta = 0f;
            foreach(Equipment equip in equipment) {
                List<StatChange> equipBuffs = equip.GetBuffs(stat);
                foreach(StatChange equipBuff in equipBuffs) {
                    // collect percentages total of all buffs
                    // ex. +10% DEF glove +5% DEF ring -3% DEF wound = +12% DEF
                    percentageDelta += equipBuff.PercentValue();
                }
            }

            float delta = .01f * percentageDelta * totalStats;
            return totalStats + delta;
        }
        
        public List<StatValue> GetLimbStats(){
            return this.limbStats;
        }

        public float GetStat(Stats.STAT stat) {
            // TODO duplication in CombatantData.GetStat()
            if (limbStats.Exists(x => x.GetStat() == stat)) {
                return limbStats.Find(x => x.GetStat() == stat).GetValue();
            }else {
                return DEFAULT_STAT;
            }
        }

        public string GetName(){
            return name;
        }
    }
}
