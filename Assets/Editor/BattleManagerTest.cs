using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using LIMB;

namespace Tests
{
    public class BattleManagerTest
    {
        BattleManager bm;
        List<Combatant> cp1;
        List<Combatant> cp2;

        [SetUp]
        public void SetUp()
        {
            bm = new BattleManager();
            cp1 = CreateCombatantParty("party 1; member ", 3);
            cp2 = CreateCombatantParty("party 2; member ", 3);
        }

        [Test]
        public void StartBattleWithCombatantListsSuccessful()
        {
            bm.StartBattle(cp1, cp2);
            Assert.IsTrue(bm.isInBattle());
        }

        [Test]
        public void GetAlliedTeamSuccessful()
        {
            Combatant foo = new Combatant("FOO");
            cp1.Add(foo);
            bm.StartBattle(cp1, cp2);
            List<Combatant> sameTeam = bm.GetAlliedTeam(foo);
            Assert.IsTrue(sameTeam[0].GetName().StartsWith("party 1"));
        }

        [Test]
        public void GetEnemyTeamSuccessful()
        {
            Combatant foo = new Combatant("FOO");
            cp1.Add(foo);
            bm.StartBattle(cp1, cp2);
            List<Combatant> otherTeam = bm.GetEnemyTeam(foo);
            Assert.IsTrue(otherTeam[0].GetName().StartsWith("party 2"));
        }

        [Test]
        public void GetAlliedTeamFailure()
        {
            Combatant foo = new Combatant("FOO");
            bm.StartBattle(cp1, cp2);
            List<Combatant> team = bm.GetAlliedTeam(foo);
            Assert.IsNull(team);
        }

        List<Combatant> CreateCombatantParty(string namePrefix, int numMembers)
        {
            List<Combatant> party = new List<Combatant>();
            for (int i = 0; i < numMembers; i++)
            {
                party.Add(new Combatant(namePrefix + " " + i));
            }
            return party;
        }


    }
}
