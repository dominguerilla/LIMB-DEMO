using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using LIMB;

namespace Tests
{
    public class RandomAITest
    {
        BattleManager bm;
        RandomCombatantAI ai;
        Combatant foo;
        List<Combatant> cp1;
        List<Combatant> cp2;

        [SetUp]
        public void SetUp()
        {
            bm = new BattleManager();
            ai = new RandomCombatantAI(bm);
            cp1 = CreateCombatantParty("party 1; member ", 3);
            cp2 = CreateCombatantParty("party 2; member ", 3);
            foo = new Combatant("FOO");
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

        [Test]
        public void GetRandomAlliedTarget()
        {
            cp1.Add(foo);
            bm.StartBattle(cp1, cp2);
            List<Combatant> targetList = ai.GetRandomTargets(foo, Skill.TARGETABLE.ALLIES, Skill.TARGET_TYPE.SINGLE);
            Assert.IsTrue(targetList.Count == 1);
            Assert.IsTrue(bm.GetAlliedTeam(foo).Contains(targetList[0]));
        }

        [Test]
        public void GetRandomTargetAllAllies()
        {
            cp1.Add(foo);
            bm.StartBattle(cp1, cp2);
            List<Combatant> targetList = ai.GetRandomTargets(foo, Skill.TARGETABLE.ALLIES, Skill.TARGET_TYPE.GROUP);
            Assert.IsTrue(targetList.Count == 4);
            Assert.IsTrue(bm.GetAlliedTeam(foo).Contains(targetList[0]));
        }

        [Test]
        public void GetRandomTargetAll()
        {
            cp1.Add(foo);
            bm.StartBattle(cp1, cp2);
            List<Combatant> targetList = ai.GetRandomTargets(foo, Skill.TARGETABLE.ALL, Skill.TARGET_TYPE.SINGLE);
            Assert.IsTrue(targetList.Count == 1);
            Assert.IsTrue(bm.GetCombatantTeam1().Contains(targetList[0]) || bm.GetCombatantTeam2().Contains(targetList[0]));
        }

        [Test]
        public void GetRandomEnemyTarget()
        {
            cp1.Add(foo);
            bm.StartBattle(cp1, cp2);
            List<Combatant> targetList = ai.GetRandomTargets(foo, Skill.TARGETABLE.ENEMIES, Skill.TARGET_TYPE.SINGLE);
            Assert.IsTrue(targetList.Count == 1);
            Assert.IsTrue(bm.GetEnemyTeam(foo).Contains(targetList[0]));
        }

        [Test]
        public void GetRandomEnemyTeam()
        {
            cp1.Add(foo);
            bm.StartBattle(cp1, cp2);
            List<Combatant> targetList = ai.GetRandomTargets(foo, Skill.TARGETABLE.ENEMIES, Skill.TARGET_TYPE.GROUP);
            Assert.IsTrue(targetList.Count == 3);
            Assert.IsTrue(bm.GetEnemyTeam(foo).Contains(targetList[0]));
        }

        [Test]
        public void GetRandomAllCombatants()
        {
            cp1.Add(foo);
            bm.StartBattle(cp1, cp2);
            List<Combatant> targetList = ai.GetRandomTargets(foo, Skill.TARGETABLE.ALL, Skill.TARGET_TYPE.GROUP);
            Assert.IsTrue(targetList.Count == 7);
        }

        [Test]
        public void GetRandomCombatantFailure()
        {
            try
            {
                bm.StartBattle(cp1, cp2);
                List<Combatant> targetList = ai.GetRandomTargets(foo, Skill.TARGETABLE.ALL, Skill.TARGET_TYPE.SINGLE);
                Assert.Fail();
            }
            catch (System.InvalidOperationException)
            {
                Assert.Pass();
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetRandomAlliedCombatantFailure()
        {
            try
            {
                bm.StartBattle(cp1, cp2);
                List<Combatant> targetList = ai.GetRandomTargets(foo, Skill.TARGETABLE.ALLIES, Skill.TARGET_TYPE.SINGLE);
                Assert.Fail();
            }
            catch (System.InvalidOperationException)
            {
                Assert.Pass();
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetRandomEnemyCombatantFailure()
        {
            try
            {
                bm.StartBattle(cp1, cp2);
                List<Combatant> targetList = ai.GetRandomTargets(foo, Skill.TARGETABLE.ENEMIES, Skill.TARGET_TYPE.SINGLE);
                Assert.Fail();
            }
            catch (System.InvalidOperationException)
            {
                Assert.Pass();
            }
            catch (System.Exception)
            {
                Assert.Fail();
            }
        }
    }
}
