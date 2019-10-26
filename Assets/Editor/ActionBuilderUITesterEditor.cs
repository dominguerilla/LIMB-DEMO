using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LIMB;
using UnityEditor;

[CustomEditor(typeof(ActionBuilderUITester))]
public class ActionBuilderUITesterEditor : Editor
{

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        ActionBuilderUITester tester = (ActionBuilderUITester)target;

        if(GUILayout.Button("Show Skills")){
            tester.ShowSkills();
        }

        if(GUILayout.Button("Hide Skills")){
            tester.HideSkills();
        }
    }
}
