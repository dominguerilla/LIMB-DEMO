using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using System;

public class Heal : Skill
{
    public override IEnumerator Execute(Combatant actor, Combatant target, onFinishCallback callback) {
        target.ChangeHealth(10f);
        yield return null;
    }
}
