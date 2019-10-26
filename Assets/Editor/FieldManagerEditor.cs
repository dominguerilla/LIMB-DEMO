using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LIMB {
    [CustomEditor(typeof(FieldManager))]
    public class FieldManagerEditor : Editor {

        public override void OnInspectorGUI() {
            FieldManager manager = (FieldManager)target;
            if (Application.isPlaying && manager.DebugMode){
                if(GUILayout.Button("Start Battle")){
                    manager.StartBattle();
                }
                if(GUILayout.Button("End Battle")){
                    manager.EndBattle();
                }
            }

            DrawDefaultInspector();
        }
    }
}
