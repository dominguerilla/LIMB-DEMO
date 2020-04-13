using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using System;

public class MockSkill : Skill {
    
    public delegate bool canBeCastFunc(Combatant actor, Combatant[] actorParty = null, Combatant[] enemyParty = null);
    public delegate bool canTargetFunc(Combatant actor, Combatant target, Combatant[] actorParty = null, Combatant[] enemyParty = null);
    public delegate void executeFunc(Combatant actor, Combatant target);

    public canBeCastFunc canBeCast;
    public canTargetFunc canTarget;
    public executeFunc execute;

    public override bool CanBeCast(Combatant actor, Combatant[] actorParty = null, Combatant[] enemyParty = null) {
        if(canBeCast != null) {
            return canBeCast.Invoke(actor, actorParty, enemyParty);
        }else {
            return true;
        }
    }

    public override bool CanTarget(Combatant actor, Combatant target, Combatant[] actorParty = null, Combatant[] enemyParty = null) {
        if(canTarget != null) {
            return canTarget.Invoke(actor, target, actorParty, enemyParty);
        }else {
            return true;
        }
    }

    public override IEnumerator Execute(Combatant actor, Combatant target, onFinishCallback callback) {
        if(execute != null) {
            execute.Invoke(actor,target);
        }
        callback.Invoke();
        yield return null;
    }
}
