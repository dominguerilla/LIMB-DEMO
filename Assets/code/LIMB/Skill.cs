using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    /// <summary>
    /// Defines a Skill that a combatant performs.
    /// </summary>
    public abstract class Skill : ScriptableObject{
        
        public string actionName;

        /// <summary>
        /// Specifies the UI menu that this action should belong to
        /// </summary>
        public enum MENU_CATEGORY{
            ATTACK,
            DEFEND,
            MAGIC,
            ITEM,
            ESCAPE
        }

        public enum TARGETABLE {
            ALL,
            ALLIES,
            ENEMIES
        }

        public enum TARGET_TYPE {
            SINGLE,
            GROUP
        }
        
        public MENU_CATEGORY category;
        public TARGETABLE targetable;
        public TARGET_TYPE targetType;

        /// <summary>
        /// Returns true if this Action can be performed.
        /// For example, if the actor combatant has no mana, it shouldn't be able to use
        /// mana-based skills.
        /// Called at the beginning of the 'Order' phase.
        /// </summary>
        public virtual bool CanBeCast(Combatant actor, Combatant[] actorParty = null, Combatant[] enemyParty = null){
            if(actor.IsAlive()) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns true if this Action can be executed by the actor on the target.
        /// For example, perhaps a 'Sleep Kill' skill could only be performed on a sleeping target.
        /// Called at the start of the 'Select Target' phase of Action queuing.
        /// </summary>
        public virtual bool CanTarget(Combatant actor, Combatant target, Combatant[] actorParty = null, Combatant[] enemyParty = null){
            if(target.IsAlive()) {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Change the state of the actor/target combatants. 
        /// Do the damage calculation here.
        /// </summary>
        public abstract void Execute(Combatant actor, Combatant target);
        
    }
}
