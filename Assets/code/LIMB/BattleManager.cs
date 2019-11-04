using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using LIMB;
public class BattleManager : MonoBehaviour {

    [SerializeField]
    UnityEvent onBattleStart;

    [SerializeField]
    UnityEvent onActionExecuted;

    bool inBattle;
    List<Combatant> lCombatants, rCombatants;

    // Sorted by descending SPEED.
    Queue<Combatant> allCombatants;
    Combatant currentCombatant;

    public void StartBattle(NPCParty leftParty, NPCParty rightParty){
        if(!inBattle){
            inBattle = true;
            lCombatants = GenerateCombatants(leftParty);
            rCombatants = GenerateCombatants(rightParty);

            IEnumerable<Combatant> sortedCombatants = lCombatants.Concat<Combatant>(rCombatants);
            sortedCombatants = sortedCombatants.OrderByDescending(c => c.GetRawStat(Stats.STAT.SPEED));
            allCombatants = new Queue<Combatant>(sortedCombatants);
            
            /*foreach(Combatant c in allCombatants){
                Debug.Log(c + "; SPEED " + c.GetRawStat(Stats.STAT.SPEED));
            }*/
            onBattleStart.Invoke();
            Debug.Log("Battle started!");
        }
    }

    public void EndBattle(){
        if(inBattle){
            inBattle = false;
            Debug.Log("Battle ended!");
            lCombatants = null;
            rCombatants = null;
            allCombatants = null;
            currentCombatant = null;
        }
    }
    
    public void ExecuteAction(Action action) {
        action.Execute();
        Debug.Log("Action executed: " + action.ToString());
        onActionExecuted.Invoke();
    }

    List<Combatant> GenerateCombatants(NPCParty party){
        CombatantData[] data = party.GetData();
        List<Combatant> combatants = new List<Combatant>();
        foreach(CombatantData c in data){
            combatants.Add(new Combatant(c));
        }
            
        return combatants;
    }

    public Combatant GetNextCombatant(){
        if(!inBattle){
            Debug.LogError("Not in battle!");
            return null;
        }
        if(currentCombatant != null){
            allCombatants.Enqueue(currentCombatant);
        }
        currentCombatant = allCombatants.Dequeue();
        return currentCombatant;
    }

    public List<Combatant> GetLeftCombatants() {
        if(!inBattle) Debug.LogError("Cannot get Left Combatants; not in battle!");
        return this.lCombatants;
    }

    public List<Combatant> GetRightCombatants() {
        if(!inBattle) Debug.LogError("Cannot get Right Combatants; not in battle!");
        return this.rCombatants;
    }
    
    /// <summary>
    /// Checks the following in this order:
    /// 1. Is the BattleManager in battle?
    /// 2. Are the lCombatants alive?
    /// 3. Are the rCombatants alive?
    /// If any are false, return false.
    /// </summary>
    public bool CanContinueBattle(){
        if(!inBattle) return false;

        bool canPartyContinue = false;
        foreach(Combatant combatant in this.lCombatants){
            if(combatant.IsAlive()){
                canPartyContinue = true;
            }
        }
        if(!canPartyContinue){
            return false;
        }

        canPartyContinue = false;
        foreach(Combatant combatant in this.rCombatants){
            if(combatant.IsAlive()){
                canPartyContinue = true;
            }
        }

        return canPartyContinue;
    }

}
