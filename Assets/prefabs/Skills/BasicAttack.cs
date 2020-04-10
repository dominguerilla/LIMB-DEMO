using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIMB {
    /// <summary>
    /// Base class for attacks made with equipped weapons.
    /// </summary>
    [CreateAssetMenu(fileName = "New BasicAttack", menuName = "Skill/Basic Attack", order = 51)]
    public class BasicAttack : Skill {

        public Damage damage;

        public override bool CanTarget(Combatant actor, Combatant target, Combatant[] actorParty = null, Combatant[] enemyParty = null) {
            if(target.IsAlive() && actor != target){
                return true;
            }
            return false;
        }

        public override IEnumerator Execute(Combatant actor, Combatant target, onFinishCallback callback) {
            CombatantAnimator actorAnim = actor.GetGameObject().GetComponent<CombatantAnimator>();
            CombatantAnimator targetAnim = target.GetGameObject().GetComponent<CombatantAnimator>();

            if (actorAnim) actorAnim.TriggerAnimation("LightAttack");
            if (targetAnim) targetAnim.TriggerAnimation("OnHurt");
            target.InflictDamage(damage);
            Debug.Log("Basic Attack finished!");
            yield return new WaitForSeconds(1f);
            callback.Invoke();
        }
    }
}
