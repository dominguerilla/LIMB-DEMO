using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using System;

public class Locator
{
    private static BattleManager battleManager;
    private static CameraController camController;
    private static BattleSceneManager battleSceneManager;

    public static void Provide(BattleManager bm){
        if (battleManager != null) throw new InvalidOperationException("Battle Manager already set!");
        battleManager = bm;
    }

    public static void Provide(CameraController camCon)
    {
        if (camController != null) throw new InvalidOperationException("Camera Controller already set!");
        camController = camCon;
    }

    public static void Provide(BattleSceneManager bsm)
    {
        if (battleSceneManager != null) throw new InvalidOperationException("Battle Scene Manager already set!");
        battleSceneManager = bsm;
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

    public static CameraController GetCameraController()
    {
        return camController;
    }

    public static BattleSceneManager GetBattleSceneManager()
    {
        return battleSceneManager;
    }
}
