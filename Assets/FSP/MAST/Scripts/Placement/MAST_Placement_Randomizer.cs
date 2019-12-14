using UnityEngine;

public static class MAST_Placement_Randomizer
{
#if (UNITY_EDITOR)
    
    private static Vector3 position;
    private static Vector3 rotation;
    private static Vector3 scale;
    
    // -----------------------------------------------------------------------
    // Generate new random values for placement
    // -----------------------------------------------------------------------
    public static void GenerateNewRandomSeed()
    {
        // ---------------------
        // Randomize Position
        // ---------------------
        
        // Create X position
        position.x =
            Random.Range(MAST_Placement_Helper.Randomizer.Position.GetMin().x,
            MAST_Placement_Helper.Randomizer.Position.GetMax().x);
        
        // Create Y position
        position.y =
            Random.Range(MAST_Placement_Helper.Randomizer.Position.GetMin().y,
            MAST_Placement_Helper.Randomizer.Position.GetMax().y);
        
        // Create Z position
        position.z =
            Random.Range(MAST_Placement_Helper.Randomizer.Position.GetMin().z,
            MAST_Placement_Helper.Randomizer.Position.GetMax().z);
        
        // ---------------------
        // Randomize Rotation
        // ---------------------
        
        // Rotate X
        if (MAST_Placement_Helper.Randomizer.Rotation.GetX())
            rotation.x = Random.Range(0f, 360f);
        else
            rotation.x = 0;
        
        // Rotate Y
        if (MAST_Placement_Helper.Randomizer.Rotation.GetY())
            rotation.y = Random.Range(0f, 360f);
        else
            rotation.y = 0;
        
        // Rotate Z
        if (MAST_Placement_Helper.Randomizer.Rotation.GetZ())
            rotation.z = Random.Range(0f, 360f);
        else
            rotation.z = 0;
        
        // ---------------------
        // Randomize Scale
        // ---------------------
        
        // Create X scale
        scale.x =
            Random.Range(MAST_Placement_Helper.Randomizer.Scale.GetMin().x,
            MAST_Placement_Helper.Randomizer.Scale.GetMax().x);
        
        // XYZ lock on, set Y scale to match X scale
        if (MAST_Placement_Helper.Randomizer.Scale.GetLock() == MAST_Placement_ScriptableObject.AxisLock.XYZ)
            scale.y = scale.x;
        
        // XYZ lock off, create Y scale
        else
            scale.y =
                Random.Range(MAST_Placement_Helper.Randomizer.Scale.GetMin().y,
                MAST_Placement_Helper.Randomizer.Scale.GetMax().y);
        
        // XYZ or XZ lock on, set Z scale to match X scale
        if (MAST_Placement_Helper.Randomizer.Scale.GetLock() != MAST_Placement_ScriptableObject.AxisLock.NONE)
            scale.z = scale.x;
        
        // XYZ and XZ locks off, create Z scale
        else
            scale.z =
                Random.Range(MAST_Placement_Helper.Randomizer.Scale.GetMin().z,
                MAST_Placement_Helper.Randomizer.Scale.GetMax().z);
    }
    
    // ---------------------------------------------------------------------------
    // Apply Randomizer values to GameObject Transform
    // ---------------------------------------------------------------------------
    public static GameObject ApplyRandomizerToTransform(GameObject gameObject, Quaternion defaultRotation)
    {
        // Move ghost based on Randomizer values
        gameObject.transform.position += position;
        
        // Rotate gameobject based on Randomizer values
        gameObject.transform.rotation = defaultRotation;
        gameObject.transform.Rotate(rotation.x, rotation.y, rotation.z);
        
        // Scale ghost based on Randomizer values
        gameObject.transform.localScale = new Vector3(
            scale.x, scale.y, scale.z);
        
        return gameObject;
    }
    
#endif
}
