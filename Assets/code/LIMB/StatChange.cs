using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    [System.Serializable]
    public class StatChange {

        // for ease of design
        public enum DIRECTION{
            UP = 1,
            DOWN = -1
        }

        public enum MAGNITUDE{
            FLAT,
            MINIMAL,
            LIGHT,
            SMALL,
            MEDIUM,
            LARGE,
            HEAVY,
            MASSIVE,
        }

        public MAGNITUDE Magnitude;
        public DIRECTION Direction;
        public Stats.STAT Stat;
        public float flatBuff = 1.0f;

        public StatChange(StatChange.DIRECTION dir, StatChange.MAGNITUDE mag, Stats.STAT stat){
            this.Direction = dir;
            this.Magnitude = mag;
            this.Stat = stat;
        }

        public float PercentValue() {
            float mag = 0f;
            switch(Magnitude) {
                case MAGNITUDE.MINIMAL:
                    mag = 5f;
                    break;
                case MAGNITUDE.LIGHT:
                    mag = 10f;
                    break;
                case MAGNITUDE.SMALL:
                    mag = 15f;
                    break;
                case MAGNITUDE.MEDIUM:
                    mag = 25f;
                    break;
                case MAGNITUDE.LARGE:
                    mag = 45f;
                    break;
                case MAGNITUDE.HEAVY:
                    mag = 65f;
                    break;
                case MAGNITUDE.MASSIVE:
                    mag = 80f;
                    break;
                default:
                    mag = flatBuff;
                    break;
            }
            return mag * (float)Direction;

        }
    }
}
