using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using LIMB;

/// <summary>
/// Used to control the flow of battle.
/// </summary>
public class BattleManager : MonoBehaviour {

    public UnityEvent onBattleStart = new UnityEvent();

    public UnityEvent onBattleEnd = new UnityEvent();

    public UnityEvent onActionExecuted = new UnityEvent();

    bool inBattle;
    List<Combatant> combatantTeam1, combatantTeam2;

    // Sorted by descending SPEED.
    Queue<Combatant> allCombatants;
    Combatant currentCombatant;

    /// <summary>
    /// Given two NPCParty objects, create two Lists of Combatants representing two 'teams' in battle against each other.
    /// Sorts the list of all combatants by their SPEED stat.
    /// </summary>
    /// <param name="team1"></param>
    /// <param name="team2"></param>
    public void StartBattle(NPCParty team1, NPCParty team2){
        if(!inBattle){
            inBattle = true;
            combatantTeam1 = GenerateCombatants(team1);
            combatantTeam2 = GenerateCombatants(team2);

            IEnumerable<Combatant> sortedCombatants = combatantTeam1.Concat<Combatant>(combatantTeam2);
            sortedCombatants = sortedCombatants.OrderByDescending(c => c.GetRawStat(Stats.STAT.SPEED));
            allCombatants = new Queue<Combatant>(sortedCombatants);
            
            /*foreach(Combatant c in allCombatants){
                Debug.Log(c + "; SPEED " + c.GetRawStat(Stats.STAT.SPEED));
            }*/
            onBattleStart.Invoke();
            Debug.Log("Battle started!");
        }
    }

    public void StartBattle(List<Combatant> team1, List<Combatant> team2)
    {
        if (!inBattle)
        {
            inBattle = true;
            combatantTeam1 = team1;
            combatantTeam2 = team2;

            IEnumerable<Combatant> sortedCombatants = combatantTeam1.Concat<Combatant>(combatantTeam2);
            sortedCombatants = sortedCombatants.OrderByDescending(c => c.GetRawStat(Stats.STAT.SPEED));
            allCombatants = new Queue<Combatant>(sortedCombatants);

            onBattleStart.Invoke();
            Debug.Log("Battle started!");
        }
    }

    /// <summary>
    /// Resets the state of the BattleManager.
    /// </summary>
    public void EndBattle(){
        if(inBattle){
            inBattle = false;
            combatantTeam1 = null;
            combatantTeam2 = null;
            allCombatants = null;
            currentCombatant = null;
            onBattleEnd.Invoke();
            Debug.Log("Battle ended!");
        }
    }
    
    public void ExecuteAction(Action action) {
        StartCoroutine(action.Execute(onActionExecuted.Invoke));
        Debug.Log("Action executing: " + action.ToString());
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

    public List<Combatant> GetCombatantTeam1() {
        if(!inBattle) Debug.LogError("Cannot get Combatant Team 1; not in battle!");
        return this.combatantTeam1;
    }

    public List<Combatant> GetCombatantTeam2() {
        if(!inBattle) Debug.LogError("Cannot get Combatant Team 2; not in battle!");
        return this.combatantTeam2;
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
        foreach(Combatant combatant in this.combatantTeam1){
            if(combatant.IsAlive()){
                canPartyContinue = true;
            }
        }
        if(!canPartyContinue){
            return false;
        }

        canPartyContinue = false;
        foreach(Combatant combatant in this.combatantTeam2){
            if(combatant.IsAlive()){
                canPartyContinue = true;
            }
        }

        return canPartyContinue;
    }

    public bool isInBattle()
    {
        return inBattle;
    }

    public List<Combatant> GetAlliedTeam(Combatant combatant)
    {
        if (combatantTeam1.Contains(combatant))
        {
            return combatantTeam1;
        }
        else if (combatantTeam2.Contains(combatant))
        {
            return combatantTeam2;
        }
        else
        {
            return null;
        }
    }

    public List<Combatant> GetEnemyTeam(Combatant combatant)
    {
        if (combatantTeam1.Contains(combatant))
        {
            return combatantTeam2;
        }
        else if (combatantTeam2.Contains(combatant))
        {
            return combatantTeam1;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Returns 0 if combatant is on team 1, 1 if on team 2.
    /// If on neither team or not in battle, will throw InvalidOperationException.
    /// </summary>
    /// <param name="combatant"></param>
    /// <returns></returns>
    public int GetTeamIndex(Combatant combatant)
    {
        if (!inBattle) throw new System.InvalidOperationException("Not in battle!");
        if (combatantTeam1.Contains(combatant))
        {
            return 0;
        }
        else if (combatantTeam2.Contains(combatant))
        {
            return 1;
        }
        else
        {
            throw new System.InvalidOperationException(string.Format("Combatant {0} is not in battle!", combatant.GetName()));
        }
    }
}
