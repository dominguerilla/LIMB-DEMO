using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    [System.Serializable]
    public class Damage {
        public enum TYPE{
            NONE,
            BLUNT,
            SLASH,
            STAB,
            FIRE,
            WATER,
            ICE,
            WIND,
            EARTH,
            ELECTRIC,
            PSYCHIC,
            LIGHT,
            DARK,
        }
        
        public enum MAGNITUDE{
            FLAT,
            MINIMAL,
            LIGHT,
            SMALL,
            MEDIUM,
            LARGE,
            HEAVY,
            MASSIVE
        }

        public enum TIMING{
            INSTANT,
            TURN_START,
            TURN_END,
        }

        public TIMING timing;
        public TYPE type;
        public MAGNITUDE magnitude;

        [Tooltip("Used only if MAGNITUDE is set to FLAT.")]
        public float flatDamage;

        public Damage() {

        }

        public Damage(float flatDamage) {
            this.timing = TIMING.INSTANT;
            this.type = TYPE.NONE;
            this.magnitude = MAGNITUDE.FLAT;
            this.flatDamage = flatDamage;
        }

        public Damage(TIMING timing, TYPE type, MAGNITUDE mag, float flatDamage = 0.0f) {
            this.timing = timing;
            this.type = type;
            this.magnitude = mag;
            this.flatDamage = flatDamage;
        }

        public override string ToString() {
            string damageTotal;
            if(magnitude == MAGNITUDE.FLAT) {
                damageTotal = "flat (" + flatDamage + ")";
            }else {
                damageTotal = System.Enum.GetName(typeof(MAGNITUDE), magnitude);
            }

            return damageTotal + " " + System.Enum.GetName(typeof(TYPE), type);
        }
    }
}
