using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MAST_Palette
{
    private static GameObject[] prefabs;
    private static Texture2D[] texture2D;
    private static string[] tooltip;
    private static GUIContent[] guiContent;
    
    public static int selectedItemIndex = -1;
    
    // ---------------------------------------------------------------------------
    // Prefab Palette
    // ---------------------------------------------------------------------------
    
    // Load all Prefabs from the selected folder and save the palette locally
    public static void ProcessPrefabsInSelectedFolder()
    {
        // Load Prefabs in the selected folder
        prefabs = MAST_Asset_Loader.GetPrefabsInSelectedFolder(false);
        
        // Generate images of all prefabs and save to a Texture2D array
        texture2D = MAST_Asset_Loader.GetThumbnailCamera()
            .GetComponent<MAST_Preview_Camera_Component>()
            .GetThumbnails(prefabs);
        
        // Initialize guiContent and tooltip arrays
        guiContent = new GUIContent[prefabs.Length];
        tooltip = new string[prefabs.Length];
        
        // Create GUI Content from Images and Prefab names
        for (int i = 0; i < prefabs.Length; i++)
        {
            // Create tooltip from object name
            tooltip[i] = prefabs[i].name.Replace("_", "\n").Replace(" ", "\n");
            
            // Make sure texture has a transparent background
            texture2D[i].alphaIsTransparency = true;
            
            // Create GUIContent from texture and tooltip
            guiContent[i] = new GUIContent(texture2D[i], tooltip[i]);
        }
    }
    
    // Set the Palette GUIContent array directly
    public static void RestorePaletteData(
        GameObject[] newPrefabs, Texture2D[] newTexture2D, string[] newTooltip)
    {
        prefabs = newPrefabs;
        texture2D = newTexture2D;
        tooltip = newTooltip;
        
        // Initialize guiContent array
        guiContent = new GUIContent[prefabs.Length];
        
        // Create GUI Content from Images and Prefab names
        for (int i = 0; i < prefabs.Length; i++)
        {
            // Make sure texture has a transparent background
            texture2D[i].alphaIsTransparency = true;
            
            // Create GUIContent from texture and tooltip
            guiContent[i] = new GUIContent(texture2D[i], tooltip[i]);
        }
    }
    
    // Does the Palette contain prefabs?
    public static bool IsReady()
    {
        return (guiContent != null && guiContent.Length > 0);
    }
    
    // Were palette images lost
    public static bool ArePaletteImagesLost()
    {
        // If array is empty, return true
        if (texture2D == null)
            return true;
        
        // If image in array is empty, return true
        return (texture2D[0] == null);
    }
    
    // Get the Palette Prefab (GameObject) array
    public static GameObject[] GetPrefabArray()
    {
        return prefabs;
    }
    
    // Get the Palette Texture2D array
    public static Texture2D[] GetTexture2DArray()
    {
        return texture2D;
    }
    
    // Get the Palette GUIContent array for display
    public static GUIContent[] GetGUIContentArray()
    {
        return guiContent;
    }
    
    // Return currently selected prefab in the palette
    public static GameObject GetSelectedPrefab()
    {
        return prefabs[selectedItemIndex];
    }
    
}