using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using LIMB;

namespace Tests
{
    public class ActionTest
    {
        MockSkill CreateMockSkill()
        {
            return ScriptableObject.CreateInstance<MockSkill>();
        }

        [Test]
        public void ActionTestConstructionSuccessful()
        {
            Combatant actor = new Combatant("Actor");
            MockSkill mockSkill = CreateMockSkill();
            Action action = new Action(actor, mockSkill);
        }

        [Test]
        public void HeroAttacksGoblin() {
            Combatant hero = new Combatant("Hero");
            Combatant goblin = new Combatant("Goblin");
            MockSkill attack = CreateMockSkill();
            attack.execute = (actor, target) => { target.InflictDamage(new Damage(10f)); };

            Action attackAction = new Action(hero, attack, goblin);
            float startingHealth = goblin.GetCurrentHealth();
            attackAction.Execute(delegate { Assert.Less(goblin.GetCurrentHealth(), startingHealth); });
                        
        }

        [Test]
        public void HeroCantTargetEnemy() {
            Combatant hero = new Combatant("Hero");
            Combatant enemy = new Combatant("Enemy");
            MockSkill finisher_move = CreateMockSkill();
            finisher_move.canTarget = (actor, target, x, y) => { return false; };
            // This shouldn't execute.
            finisher_move.execute = (actor, target) => { target.InflictDamage(new Damage(100f)); };

            Action finisher = new Action(hero, finisher_move, enemy);
            float startingHealth = enemy.GetCurrentHealth();
            finisher.Execute(delegate { Assert.AreEqual(startingHealth, enemy.GetCurrentHealth()); });
            
        }
    }
}
