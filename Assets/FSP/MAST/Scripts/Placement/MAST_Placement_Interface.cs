using UnityEditor;
using UnityEngine;

public static class MAST_Placement_Interface
{
#if (UNITY_EDITOR)
    
    // Draw Tools
    public enum PlacementMode
    {
        None, DrawSingle, DrawContinuous, PaintArea, Randomize, Erase
    }
    [SerializeField] public static PlacementMode placementMode = PlacementMode.None;
    
// ---------------------------------------------------------------------------
#region Change Placement Mode
// ---------------------------------------------------------------------------
    public static void ChangePlacementMode(PlacementMode newPlacementMode)
    {
        // Get new selected Draw Tool
        placementMode = newPlacementMode;
        
        // Remove any previous visualizer
        MAST_Placement_Visualizer.RemoveVisualizer();
        
        // --------------------------------
        // Create Visualizer
        // --------------------------------
        
        // If changed tool to Nothing or Eraser
        if (placementMode == PlacementMode.None || placementMode == PlacementMode.Erase)
        {
            // Deselect any item in the palette
            MAST_Palette.selectedItemIndex = -1;
            
            // If changed tool to Eraser
            if (placementMode == PlacementMode.Erase)
            {
                // Create eraser visualizer
                ChangePrefabToEraser();
            }
            
            // If changed tool to Nothing, remove the visualizer
            else
            {
                MAST_Placement_Visualizer.RemoveVisualizer();
            }
        }
        
        // If changed tool to Draw Single, Draw Continuous, Paint Area, or Randomizer
        else
        {
            // If a palette item is selected
            if (MAST_Palette.selectedItemIndex != -1)
            {
                // Create visualizer from selected item in the palette
                ChangeSelectedPrefab();
                
                // If changed tool to Randomizer
                if (placementMode == PlacementMode.Randomize)
                {
                    // Make a new random seed
                    MAST_Placement_Randomizer.GenerateNewRandomSeed();
                }
            }
        }
        
        // If Draw Continuous nor Paint Area tools are selected
        if (placementMode != PlacementMode.DrawContinuous &&
            placementMode != PlacementMode.PaintArea)
        {
            // Delete last saved position
            MAST_Placement_Place.lastPosition = Vector3.positiveInfinity;
            
            // Remove any paint area visualization
            MAST_Placement_PaintArea.DeletePaintArea();
        }
        
    }
#endregion
    
    // Change visualizer prefab when a new item is selected in the palette menu
    public static void ChangeSelectedPrefab()
    {
        // Get reference to the MAST script attached to the GameObject
        MAST_Placement_Helper.mastScript = 
            MAST_Palette.GetSelectedPrefab().GetComponent<MAST_Prefab_Component>();
        
        // Remove any existing visualizer
        MAST_Placement_Visualizer.RemoveVisualizer();
        
        // Create a new visualizer
        MAST_Placement_Visualizer.CreateVisualizer(MAST_Palette.GetSelectedPrefab());
    }
    
    // Change visualizer to eraser
    public static void ChangePrefabToEraser()
    {
        // Clear reference to the MAST script attached to the last selected GameObject
        // ????????????????
        
        // Remove any existing visualizer
        MAST_Placement_Visualizer.RemoveVisualizer();
        
        // Create a new visualizer with eraser
        MAST_Placement_Visualizer.CreateVisualizer(MAST_Asset_Loader.GetEraserPrefab());
    }
    
    public static void ErasePrefab()
    {
        // Get array containing all Colliders within eraser
        Collider[] colliders =
            Physics.OverlapBox(
                MAST_Placement_Visualizer.GetGameObject().transform.position +
                new Vector3(0f, 0.35f, 0f), new Vector3(0.4f, 0.4f, 0.4f));
        
        // Loop through each GameObject inside or colliding with this OverlapBox
        foreach (Collider collider in colliders)
    {
            // Use try/catch, incase this collider's GameObject is already destroyed
            try
            {
                // If the nearby GameObject has a parent
                if (collider.gameObject.transform.parent != null)
                {

                    // Get Parent GameObject for the GameObject containing this Collider
                    GameObject testObject = collider.gameObject.transform.parent.gameObject;
                    
                    // If near GameObject is not the visualizer itself
                    if (testObject.name != "MAST_Visualizer" &&
                        testObject.name != MAST_Const.grid.defaultName &&
                        testObject.name != MAST_Const.grid.defaultParentName)
                    {
                        // Erase it, but allow an undo
                        Undo.DestroyObjectImmediate(testObject);
                    }
                }
            }
            catch
            {
                // Do nothing since this collider's GameObject was already destroyed
            }
        }
    }
    
#endif
}