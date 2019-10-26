using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    /// <summary>
    /// The class the implementer will inherit from to specify and calculate stats.
    /// All combatants will have these stats, and it will be up to the implementer
    /// to decide what stats to use and how to calculate them.
    /// </summary>
    public abstract class StatCalculator {

        public string[] STATS;
        public abstract float CalculateStat(Combatant combatant, Stats.STAT stat);
        /*    
            switch(stat) {
                case STATS.HP:
                    return (.25f * this.data.LVL) + (.5f * this.data.END);
                case STATS.MP:
                    return 0;
                case STATS.ACCURACY:
                    return 0;
                case STATS.SPEED:
                    return 0;
                case STATS.CRIT:
                    return 0;
                case STATS.PHYS_ATK:
                    return 0;
                case STATS.PHYS_DEF:
                    return 0;
                case STATS.MAG_ATK:
                    return 0;
                case STATS.MAG_DEF:
                    return 0;
                case STATS.FIRE_ATK:
                    return 0;
                case STATS.FIRE_DEF:
                    return 0;
                case STATS.WATER_ATK:
                    return 0;
                case STATS.WATER_DEF:
                    return 0;
                case STATS.ICE_ATK:
                    return 0;
                case STATS.ICE_DEF:
                    return 0;
                case STATS.WIND_ATK:
                    return 0;
                case STATS.WIND_DEF:
                    return 0;
                case STATS.EARTH_ATK:
                    return 0;
                case STATS.EARTH_DEF:
                    return 0;
                case STATS.ELECTRIC_ATK:
                    return 0;
                case STATS.ELECTRIC_DEF:
                    return 0;
                case STATS.LIGHT_ATK:
                    return 0;
                case STATS.LIGHT_DEF:
                    return 0;
                default:
                    return 0;
            }
            */
    }
}
