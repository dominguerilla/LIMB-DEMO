using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class MAST_Interface_State_Save_ScriptableObject : ScriptableObject
{
    [SerializeField] public bool gridExists = false;
    [SerializeField] public int selectedDrawToolIndex = -1;
    [SerializeField] public int selectedItemIndex = -1;
    
    [SerializeField] public GameObject[] prefabs;
    [SerializeField] public string[] paletteItemTooltip;
}