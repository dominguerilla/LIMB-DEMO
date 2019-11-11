using UnityEngine;
using System;

// Use [SelectionBase] to make sure parent is selected when clicking a child the first time
[SelectionBase]
[ExecuteInEditMode]
public class MAST_Prefab_Component : MonoBehaviour
{
#if (UNITY_EDITOR)
    // ---------------------------------------------------------------------------
    [Tooltip("Group ID to link this prefab with other prefabs of the same type")]
    [SerializeField] public int categoryID = 0;
    // ---------------------------------------------------------------------------
    [Tooltip("Allow this Prefab to be placed inside other Prefabs?")]
    [SerializeField] public bool placeInsideOthers = true;
    // ---------------------------------------------------------------------------
    [Tooltip("Offset position")]
    [SerializeField] public Vector3 offsetPos = new Vector3(0.0f, 0.0f, 0.0f);
    // ---------------------------------------------------------------------------
    [Tooltip("Set to 0 if no rotation allowed around the specified axis")]
    [SerializeField] public Vector3 rotationFactor = new Vector3(90f, 90f, 90f);
    // ---------------------------------------------------------------------------
    [Tooltip("Stretch prefabs when painting an area?")]
    [SerializeField] public bool scalable = false;
    // ---------------------------------------------------------------------------
    public enum AxisLock { NONE = 0, XZ = 1, XYZ = 2 }
    [Tooltip("Randomizer Options")]
    [SerializeField] public Randomizer randomizer;
    [Serializable] public class Randomizer
    {
        [Tooltip("Is prefab randomizable?")]
        [SerializeField] public bool randomizable = false;
        
        [Tooltip("Allow rotation on X axis?")]
        [SerializeField] public bool rotateX = false;
        [Tooltip("Allow rotation on Y axis?")]
        [SerializeField] public bool rotateY = true;
        [Tooltip("Allow rotation on Z axis?")]
        [SerializeField] public bool rotateZ = false;
        
        [Tooltip("Minimum scale during randomization")]
        [SerializeField] public Vector3 scaleMin = new Vector3(0.75f, 0.75f, 0.75f);
        [Tooltip("Maximum scale during randomization")]
        [SerializeField] public Vector3 scaleMax = new Vector3(1.25f, 1.25f, 1.25f);
        [Tooltip("Lock axis together during randomization?")]
        [SerializeField] public AxisLock scaleLock = AxisLock.XZ;
        
        [Tooltip("Minimum position offset during randomization")]
        [SerializeField] public Vector3 posMin = new Vector3(-0.5f, -0.1f, -0.5f);
        [Tooltip("Maximum position offset during randomization")]
        [SerializeField] public Vector3 posMax = new Vector3(0.5f, 0.1f, 0.5f);
    }
    // ---------------------------------------------------------------------------
    [Tooltip("Include prefab when merging models?")]
    [SerializeField] public bool includeInMerge = true;
    
#endif
}