using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MAST_Prefab_Component))]
public class MAST_Prefab_Inspector : Editor
{
    public override void OnInspectorGUI ()
    {
	    serializedObject.Update();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.LabelField("Grouping", EditorStyles.boldLabel);
		EditorGUI.indentLevel += 1;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("categoryID"));
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.LabelField("Placement (Decoration or Structural?)", EditorStyles.boldLabel);
		EditorGUI.indentLevel += 1;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("placeInsideOthers"));
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.LabelField("Position", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 1;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("offsetPos"));
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.LabelField("Rotate Tool (Degrees for Rotation per Axis)", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 1;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("rotationFactor"));
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.LabelField("Paint Area Tool (Scale or Repeat Prefab?)", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 1;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("scalable"));
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.LabelField("Randomizer Tool (Not for Modular Prefabs)", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 1;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("randomizer"), true);
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.LabelField("Merge Models Tool", EditorStyles.boldLabel);
        EditorGUI.indentLevel += 1;
		EditorGUILayout.PropertyField(serializedObject.FindProperty("includeInMerge"));
        EditorGUI.indentLevel -= 1;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
		serializedObject.ApplyModifiedProperties();
	}
}
