using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using System;

[CreateAssetMenu(fileName = "Healing Skill", menuName = "Skill/Heal", order = 1)]
public class Heal : Skill
{
    public override void Execute(Combatant actor, Combatant target) {
        target.ChangeHealth(10f);
    }
}
