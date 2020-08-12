using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LIMB;

public class DamageTester : MonoBehaviour {

    public CombatantData targetData;
    public Damage[] damages;
    public string targetLimb;

    Combatant combatant;

	void Start () {
	    combatant = new Combatant(targetData);	
	}

    public void InflictDamage() {
        foreach(Damage dmg in damages) {
            float totalDamage = combatant.InflictDamage(dmg, combatant, targetLimb);
            Debug.Log("Inflicted " + totalDamage + " " + dmg.ToString() + " damage to combatant.");
        }
    }
}
