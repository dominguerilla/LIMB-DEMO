using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using LIMB;
namespace Tests
{
    public class BattleTest
    {
        [Test]
        public void RoundTestSimplePasses()
        {
            Battle battle = new Battle();
            Assert.AreEqual(0, battle.GetRoundCount());
            battle.ExecuteRound();
            Assert.AreEqual(1, battle.GetRoundCount());
        }

        [Test]
        public void AddRoundsAndExecute() {
            Battle battle = new Battle();
            Action action1 = new Action(), action2 = new Action(), action3 = new Action();
            Assert.AreEqual(0, battle.GetRoundLength());
            battle.AddActions(action1, action2, action3);
            Assert.AreEqual(3, battle.GetRoundLength());
            battle.ExecuteRound();
            Assert.AreEqual(0, battle.GetRoundLength());
        }


    }
}
