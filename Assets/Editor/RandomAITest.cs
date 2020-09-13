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
        Combatant sampleCombatant;
        List<Combatant> party1;
        List<Combatant> party2;
        MockSkill mockSkill;

        [SetUp]
        public void SetUp()
        {
            bm = new BattleManager();
            ai = new RandomCombatantAI(bm);
            party1 = CreateCombatantParty("party 1; member ", 3);
            party2 = CreateCombatantParty("party 2; member ", 3);
            sampleCombatant = new Combatant("FOO");
            mockSkill = new MockSkill();
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
            party1.Add(sampleCombatant);
            bm.StartBattle(party1, party2);
            mockSkill.targetable = Skill.TARGETABLE.ALLIES;
            mockSkill.targetType = Skill.TARGET_TYPE.SINGLE;
            List <Combatant> targetList = ai.GetRandomTargets(sampleCombatant, mockSkill);
            Assert.IsTrue(targetList.Count == 1);
            Assert.IsTrue(bm.GetAlliedTeam(sampleCombatant).Contains(targetList[0]));
        }

        [Test]
        public void GetRandomTargetAllAllies()
        {
            party1.Add(sampleCombatant);
            bm.StartBattle(party1, party2);
            mockSkill.targetable = Skill.TARGETABLE.ALLIES;
            mockSkill.targetType = Skill.TARGET_TYPE.GROUP;
            List<Combatant> targetList = ai.GetRandomTargets(sampleCombatant, mockSkill);
            Assert.IsTrue(targetList.Count == 4);
            Assert.IsTrue(bm.GetAlliedTeam(sampleCombatant).Contains(targetList[0]));
        }

        [Test]
        public void GetRandomTargetAll()
        {
            party1.Add(sampleCombatant);
            bm.StartBattle(party1, party2);
            mockSkill.targetable = Skill.TARGETABLE.ALL;
            mockSkill.targetType = Skill.TARGET_TYPE.SINGLE;
            List<Combatant> targetList = ai.GetRandomTargets(sampleCombatant, mockSkill);
            Assert.IsTrue(targetList.Count == 1);
            Assert.IsTrue(bm.GetCombatantTeam1().Contains(targetList[0]) || bm.GetCombatantTeam2().Contains(targetList[0]));
        }

        [Test]
        public void GetRandomEnemyTarget()
        {
            party1.Add(sampleCombatant);
            bm.StartBattle(party1, party2);
            mockSkill.targetable = Skill.TARGETABLE.ENEMIES;
            mockSkill.targetType = Skill.TARGET_TYPE.SINGLE;
            List<Combatant> targetList = ai.GetRandomTargets(sampleCombatant, mockSkill);
            Assert.IsTrue(targetList.Count == 1);
            Assert.IsTrue(bm.GetEnemyTeam(sampleCombatant).Contains(targetList[0]));
        }

        [Test]
        public void GetRandomEnemyTeam()
        {
            party1.Add(sampleCombatant);
            bm.StartBattle(party1, party2);
            mockSkill.targetable = Skill.TARGETABLE.ENEMIES;
            mockSkill.targetType = Skill.TARGET_TYPE.GROUP;
            List<Combatant> targetList = ai.GetRandomTargets(sampleCombatant, mockSkill);
            Assert.IsTrue(targetList.Count == 3);
            Assert.IsTrue(bm.GetEnemyTeam(sampleCombatant).Contains(targetList[0]));
        }

        [Test]
        public void GetRandomAllCombatants()
        {
            party1.Add(sampleCombatant);
            bm.StartBattle(party1, party2);
            mockSkill.targetable = Skill.TARGETABLE.ALL;
            mockSkill.targetType = Skill.TARGET_TYPE.GROUP;
            List<Combatant> targetList = ai.GetRandomTargets(sampleCombatant, mockSkill);
            Assert.IsTrue(targetList.Count == 7);
        }

        [Test]
        public void GetRandomCombatantFailure()
        {
            try
            {
                bm.StartBattle(party1, party2);
                mockSkill.targetable = Skill.TARGETABLE.ALL;
                mockSkill.targetType = Skill.TARGET_TYPE.SINGLE;
                List<Combatant> targetList = ai.GetRandomTargets(sampleCombatant, mockSkill);
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
                bm.StartBattle(party1, party2);
                mockSkill.targetable = Skill.TARGETABLE.ALLIES;
                mockSkill.targetType = Skill.TARGET_TYPE.SINGLE;
                List<Combatant> targetList = ai.GetRandomTargets(sampleCombatant, mockSkill);
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
                bm.StartBattle(party1, party2);
                mockSkill.targetable = Skill.TARGETABLE.ENEMIES;
                mockSkill.targetType = Skill.TARGET_TYPE.SINGLE;
                List<Combatant> targetList = ai.GetRandomTargets(sampleCombatant, mockSkill);
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
        public void GetRandomTargetFailureCannotTarget()
        {
            MockSkill mockSkill = new MockSkill();
            mockSkill.canTarget = (actor, target, actorParty, enemyParty ) => { return false; };
            sampleCombatant.AddSkill(mockSkill);
            party1.Add(sampleCombatant);
            bm.StartBattle(party1, party2);

            List<Combatant> targets = ai.GetRandomTargets(sampleCombatant, mockSkill);
            Assert.IsNull(targets);
        }
    }
}
