using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
public class Locator
{
    private static BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Provide(BattleManager bm){
        battleManager = bm;
    }

    public static (Combatant[], Combatant[]) GetCombatants(){
        if(battleManager != null){
            return (battleManager.GetCombatantTeam1().ToArray(),
                battleManager.GetCombatantTeam2().ToArray());
        }
        return (null, null);
    }
}
