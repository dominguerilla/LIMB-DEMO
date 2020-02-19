using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using System;

public class Heal : Skill
{
    public override void Execute(Combatant actor, Combatant target) {
        target.ChangeHealth(10f);
    }
}
