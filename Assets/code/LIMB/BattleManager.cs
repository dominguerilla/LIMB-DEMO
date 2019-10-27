using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
public class BattleManager : MonoBehaviour {

    List<Action> currentRound;
    int roundCount;
    bool inBattle;
    List<Combatant> lCombatants, rCombatants;

    // Sorted by descending SPEED.
    Queue<Combatant> allCombatants;
    Combatant currentCombatant;

    private void Awake() {
        currentRound = new List<Action>();
    }

    public void StartBattle(NPCParty leftParty, NPCParty rightParty){
        if(!inBattle){
            inBattle = true;
            lCombatants = GenerateCombatants(leftParty);
            rCombatants = GenerateCombatants(rightParty);

            IEnumerable<Combatant> sortedCombatants = lCombatants.Concat<Combatant>(rCombatants);
            sortedCombatants = sortedCombatants.OrderByDescending(c => c.GetRawStat(Stats.STAT.SPEED));
            allCombatants = new Queue<Combatant>(sortedCombatants);
            
            foreach(Combatant c in allCombatants){
                Debug.Log(c + "; SPEED " + c.GetRawStat(Stats.STAT.SPEED));
            }
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
    
    public void ExecuteRound() {
        this.currentRound.Clear();
        this.roundCount++;
    }

    public void AddActions(params Action[] actions) {
        this.currentRound.AddRange(actions);
    }

    /// <summary>
    /// Return the number of rounds that have gone by.
    /// </summary>
    /// <returns></returns>
    public int GetRoundCount() {
        return roundCount;
    }

    /// <summary>
    /// Return the number of Actions in the current round.
    /// </summary>
    /// <returns></returns>
    public int GetRoundLength() {
        return currentRound.Count;
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
    /// Returns true if there is at least one living Combatant on each party.
    /// </summary>
    public bool CanContinueBattle(){
        return inBattle && true;
    }
}
