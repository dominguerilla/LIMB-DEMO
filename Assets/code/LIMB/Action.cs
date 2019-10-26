using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    /// <summary>
    /// Uses a Combatant, Skill, and expected Combatant targets to build an Action that may or may not succeed.
    /// </summary>
    /// When Actions are performed, all buffs should have already been applied to all characters.
    public class Action {
        
        Skill skill;
        Combatant actor;
        Combatant[] registeredTargets;

        public Action() {

        }

        public Action(Combatant actor, Skill skill, params Combatant[] registeredTargets) {
            this.skill = skill;
            this.actor = actor;
            this.registeredTargets = registeredTargets;
        }

        public void Execute() {
            foreach(Combatant target in registeredTargets) {
                if(skill.CanTarget(actor, target)) {
                    skill.Execute(actor, target);
                }
            }
        }

        public override string ToString() {
            string targets = "";
            if(registeredTargets.Length == 0){
                targets = "NONE";
            }else if(registeredTargets.Length == 1){
                targets = registeredTargets[0].ToString();
            }
            else{
                for (int i = 0; i < registeredTargets.Length - 1; i++){
                    targets += registeredTargets[i] + ", ";
                }
                targets += "and " + registeredTargets[registeredTargets.Length - 1].ToString();
            }

            return actor.ToString() + " uses " + skill.actionName + " on " + targets;
        }
    }
}
