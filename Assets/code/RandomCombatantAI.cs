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
        IEnumerable<Combatant> team, otherTeam;

        switch (skill.targetable)
        {
            case Skill.TARGETABLE.ALLIES:
            case Skill.TARGETABLE.ENEMIES:
                team = bm.GetAlliedTeam(combatant);
                otherTeam = bm.GetEnemyTeam(combatant);
                if (team == null) throw new System.InvalidOperationException("Combatant is not in battle!");
                break;
            case Skill.TARGETABLE.ALL:
                // TODO team 1 or 2 might be null here
                IEnumerable<Combatant> combined = bm.GetCombatantTeam1().Concat(bm.GetCombatantTeam2());
                team = combined.ToList<Combatant>();
                otherTeam = null;
                if (!team.Contains(combatant)) throw new System.InvalidOperationException("Combatant is not in battle!");
                break;
            default:
                team = null;
                otherTeam = null;
                throw new Exception("Invalid Targetable!");
        }
        Combatant[] alliedTeam = team.ToArray();
        Combatant[] enemyTeam = null;
        if (otherTeam != null) enemyTeam = otherTeam.ToArray();

        if (skill.targetType == Skill.TARGET_TYPE.GROUP)
        {
            foreach (Combatant c in team)
            {
                if (skill.CanTarget(combatant, c, actorParty:alliedTeam, enemyParty:enemyTeam))
                {
                    return team.ToList();
                }
            }
            return null;
        }
        else
        {
            team = team.Where(c => skill.CanTarget(combatant, c, actorParty: alliedTeam, enemyParty: enemyTeam));
            if (team == null) return null;
            System.Random rand = new System.Random();
            Combatant comb = team.ElementAt<Combatant>(rand.Next(0, team.Count()));
            return new List<Combatant>() { comb };
        };
    }


    public LIMB.Action CreateRandomAction(Combatant combatant)
    {
        Skill skill = GetRandomCombatantSkill(combatant);
        List<Combatant> targets = GetRandomTargets(combatant, skill);
        return new LIMB.Action(combatant, skill, targets.ToArray());
    }
}
