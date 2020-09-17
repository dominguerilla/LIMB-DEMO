using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;
using LIMB;

public class RandomCombatantAI
{
    BattleManager bm;

    public RandomCombatantAI(BattleManager bm)
    {
        this.bm = bm;
    }

    Skill GetRandomCombatantSkill(Combatant combatant)
    {
        List<Skill> skills = combatant.GetSkills();
        System.Random rand = new System.Random();
        return skills[rand.Next(skills.Count)];
    }

    public List<Combatant> GetRandomTargets(Combatant combatant, Skill skill)
    {
        //TODO: The combatant passed in might not be in combat.
        if (!InCombat(combatant)) throw new System.InvalidOperationException("Combatant is not in combat!");
        IEnumerable<Combatant> targetTeam;

        switch (skill.targetable)
        {
            case Skill.TARGETABLE.ALLIES:
                targetTeam = bm.GetAlliedTeam(combatant);
                break;
            case Skill.TARGETABLE.ENEMIES:
                targetTeam = bm.GetEnemyTeam(combatant);
                break;
            case Skill.TARGETABLE.ALL:
                IEnumerable<Combatant> combined = bm.GetCombatantTeam1().Concat(bm.GetCombatantTeam2());
                targetTeam = combined.ToList<Combatant>();
                break;
            default:
                throw new Exception("Invalid Targetable!");
        }

        Combatant[] alliedTeam = bm.GetAlliedTeam(combatant).ToArray();
        Combatant[] enemyTeam = bm.GetEnemyTeam(combatant).ToArray();

        if (skill.targetType == Skill.TARGET_TYPE.GROUP)
        {
            foreach (Combatant c in targetTeam)
            {
                if (skill.CanTarget(combatant, c, actorParty:alliedTeam, enemyParty:enemyTeam))
                {
                    return targetTeam.ToList();
                }
            }
            return null;
        }
        else
        {
            targetTeam = targetTeam.Where(c => skill.CanTarget(combatant, c, actorParty: alliedTeam, enemyParty: enemyTeam));
            if (targetTeam.Count<Combatant>() == 0) return null;
            System.Random rand = new System.Random();
            Combatant comb = targetTeam.ElementAt<Combatant>(rand.Next(0, targetTeam.Count()));
            return new List<Combatant>() { comb };
        };
    }


    public LIMB.Action CreateRandomAction(Combatant combatant)
    {
        Skill skill = GetRandomCombatantSkill(combatant);
        List<Combatant> targets = GetRandomTargets(combatant, skill);
        return new LIMB.Action(combatant, skill, targets.ToArray());
    }

    bool InCombat(Combatant combatant)
    {
        return bm.GetCombatantTeam1().Contains(combatant) || bm.GetCombatantTeam2().Contains(combatant);
    }
}
