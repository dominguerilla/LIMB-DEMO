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
        return GetRandomTargets(combatant, skill.targetable, skill.targetType);
    }

    public List<Combatant> GetRandomTargets(Combatant combatant, Skill.TARGETABLE targetable, Skill.TARGET_TYPE targetType)
    {

        List<Combatant> team;

        switch (targetable)
        {
            case Skill.TARGETABLE.ALLIES:
                team = bm.GetAlliedTeam(combatant);
                if (team == null) throw new System.InvalidOperationException("Combatant is not in battle!");
                break;
            case Skill.TARGETABLE.ENEMIES:
                team = bm.GetEnemyTeam(combatant);
                if (team == null) throw new System.InvalidOperationException("Combatant is not in battle!");
                break;
            case Skill.TARGETABLE.ALL:
                IEnumerable<Combatant> combined = bm.GetCombatantTeam1().Concat(bm.GetCombatantTeam2());
                team = combined.ToList<Combatant>();
                if (!team.Contains(combatant)) throw new System.InvalidOperationException("Combatant is not in battle!");
                break;
            default:
                team = null;
                break;
        }

        if (targetType == Skill.TARGET_TYPE.GROUP)
        {
            return team;
        }
        else
        {
            System.Random rand = new System.Random();
            Combatant comb = team[rand.Next(team.Count)];
            return new List<Combatant>() { comb };
        }

    }

    public LIMB.Action CreateRandomAction(Combatant combatant)
    {
        Skill skill = GetRandomCombatantSkill(combatant);
        List<Combatant> targets = GetRandomTargets(combatant, skill);
        return new LIMB.Action(combatant, skill, targets.ToArray());
    }
}
