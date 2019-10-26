using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace LIMB {
    [CustomEditor(typeof(BattleTester))]
    public class BattleTesterEditor : Editor {
        public override void OnInspectorGUI() {
            BattleTester bt = (BattleTester)target;
            if (Application.isPlaying && bt.DebugMode){
                if(GUILayout.Button("Start Battle")){
                    bt.StartBattle();
                }
                if(GUILayout.Button("End Battle")){
                    bt.EndBattle();
                }
            }

            DrawDefaultInspector();
        }

    }
}
