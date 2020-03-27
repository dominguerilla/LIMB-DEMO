using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
public class Locator
{
    private static BattleManager battleManager;

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

    public static BattleManager GetBattleManager()
    {
        return battleManager;
    }
}
