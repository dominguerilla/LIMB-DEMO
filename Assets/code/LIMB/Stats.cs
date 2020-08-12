using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    public static class Stats{
        
        public enum STAT{
            LVL,
            HP,
            MP,
            ACCURACY,
            SPEED,
            CRIT,
            PHYS_ATK,
            PHYS_DEF,
            MAG_ATK,
            MAG_DEF,
            FIRE_ATK,
            FIRE_DEF,
            WATER_ATK,
            WATER_DEF,
            ICE_ATK,
            ICE_DEF,
            WIND_ATK,
            WIND_DEF,
            EARTH_ATK,
            EARTH_DEF,
            ELECTRIC_ATK,
            ELECTRIC_DEF,
            LIGHT_ATK,
            LIGHT_DEF,
            DARK_ATK,
            DARK_DEF,
        }

        /// <summary>
        /// Returns the resistance for the specified damage type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static STAT GetResistance(Damage.TYPE type) {
            switch(type) {
                case Damage.TYPE.BLUNT:
                case Damage.TYPE.SLASH:
                case Damage.TYPE.STAB:
                case Damage.TYPE.NONE:
                    return STAT.PHYS_DEF;
                default:
                    throw new System.MissingFieldException("Damage type " + type.ToString() + " has no resistance.");
            }
        }

        /// <summary>
        /// Calculates the magnitude of the damage given a Damage object.
        /// If inflictor is set, will use its stats in its calculation.
        /// </summary>
        /// <param name="dmg"></param>
        /// <returns></returns>
        public static float CalculateDamageMagnitude(Damage dmg, Combatant inflictor) {
            float damageMagnitude = 0.0f;
            switch (dmg.magnitude) {
                case Damage.MAGNITUDE.FLAT:
                    damageMagnitude = dmg.flatDamage;
                    break;
                case Damage.MAGNITUDE.MASSIVE:
                    damageMagnitude = inflictor.GetRawStat(STAT.PHYS_ATK) * 10f + dmg.flatDamage;
                    break;
                default:
                    Debug.LogError("Damage type " + dmg.magnitude.ToString() + " unsupported.");
                    damageMagnitude = 0.0f;
                    break;
            }

            return damageMagnitude;
        }

        /// <summary>
        /// Calculates the net damage to be inflicted on a target given a damage magnitude and its resistance value.
        /// Minimum damage inflicted is 1.
        /// </summary>
        public static float CalculateNetDamage(float damageMagnitude, float resistance) {
            return damageMagnitude - resistance > 1.0f ? damageMagnitude - resistance : 1.0f;
        }

        

    }
}
