using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using LIMB;

public class DamageInflictTest {

    CombatantData combData;
    Combatant comb;
    Damage dmg;
    List<StatValue> stats;
    List<Limb> anatomy;

    [SetUp]
    public void setUp() {
        this.stats = new List<StatValue>();
        this.anatomy = new List<Limb>();
    }

    [Test]
    public void DamageInflict1FlatDamage() {
        StatValue def = new StatValue(Stats.STAT.PHYS_DEF, 3.0f);
        StatValue health = new StatValue(Stats.STAT.HP, 5.0f);
        Limb core = new Limb("Core");
        Limb body = new Limb("Body");

        combData = ScriptableObject.CreateInstance<CombatantData>();
        combData.SetName("Slime");
        combData.SetBaseStats(def, health);
        combData.SetAnatomy(core, body);
        comb = new Combatant(combData);
        dmg = new Damage(Damage.TIMING.INSTANT, Damage.TYPE.BLUNT, Damage.MAGNITUDE.FLAT, 1.0f );
        float originalHealth = comb.GetCurrentHealth();

        comb.InflictDamage(dmg);

        // Use the Assert class to test conditions.
        Assert.Less(comb.GetCurrentHealth(), originalHealth);
    }

    [Test]
    // simulates an attack to a bandit's head and body.
    public void DifferentHeadBodyDamage(){
        // create Head stats
        StatValue headDef = new StatValue(Stats.STAT.PHYS_DEF, 1.0f);
        Limb head = new Limb("Head", headDef);
        anatomy.Add(head);

        // create Body stats
        StatValue bodyDef = new StatValue(Stats.STAT.PHYS_DEF, 3.0f);
        Limb body = new Limb("Body", bodyDef);
        anatomy.Add(body);

        // create Base stats
        StatValue baseDef = new StatValue(Stats.STAT.PHYS_DEF, 3.0f);
        StatValue health = new StatValue(Stats.STAT.HP, 100.0f);
        stats.Add(health);
        stats.Add(baseDef);

        combData = ScriptableObject.CreateInstance<CombatantData>();
        combData.InitializeData("Bandit", stats, anatomy);
        comb = new Combatant(combData);
        dmg = new Damage(Damage.TIMING.INSTANT, Damage.TYPE.BLUNT, Damage.MAGNITUDE.FLAT, 10.0f);

        float coreDamage = comb.InflictDamage(dmg, "Head");
        float bodyDamage = comb.InflictDamage(dmg, "Body");

        Assert.Less(bodyDamage, coreDamage);

    }

}
