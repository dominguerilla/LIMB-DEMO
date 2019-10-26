using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    public class Battle
    {
        List<Action> currentRound;
        int roundCount;
        NPCParty partyA, partyB;

        public Battle() {
            currentRound = new List<Action>();
        }

        public void ExecuteRound() {
            this.currentRound.Clear();
            this.roundCount++;
        }

        public void AddActions(params Action[] actions) {
            this.currentRound.AddRange(actions);
        }

        /// <summary>
        /// Return the number of rounds that have gone by.
        /// </summary>
        /// <returns></returns>
        public int GetRoundCount() {
            return roundCount;
        }

        /// <summary>
        /// Return the number of Actions in the current round.
        /// </summary>
        /// <returns></returns>
        public int GetRoundLength() {
            return currentRound.Count;
        }
    }

}
