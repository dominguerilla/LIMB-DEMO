using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MAST_Interface_State_Save
{
#if (UNITY_EDITOR)
    
    [SerializeField] public MAST_Interface_State_Save_ScriptableObject state;
    
    // ---------------------------------------------------------------------------
    // Get or create the state scriptable object
    // ---------------------------------------------------------------------------
    private void Get_Reference_To_Scriptable_Object()
    {
        // Get MAST Core path
        string statePath = MAST_Asset_Loader.GetMASTRootFolder() + "/Etc/MAST_Interface_State.asset";
        
        // Load the MAST Core scriptable object
        state = AssetDatabase.LoadAssetAtPath<MAST_Interface_State_Save_ScriptableObject>(statePath);
        
        // If the Core scriptable object isn't found, create a new default one
        if (state == null)
        {
            state = ScriptableObject.CreateInstance<MAST_Interface_State_Save_ScriptableObject>();
            AssetDatabase.CreateAsset(state, statePath);
        }
        
    }
    
    // ---------------------------------------------------------------------------
    // Save preferences to state scriptable object
    // ---------------------------------------------------------------------------
    public void Save_Palette_State(bool forceSave = false)
    {
        // Get or create a scriptable object to store the interface state data
        Get_Reference_To_Scriptable_Object();
        
        // If palette data has changed or if forcing a save "from loading new prefabs with button"
        if (MAST_Palette.GetPrefabArray() != state.prefabs || forceSave)
        {
            // Delete any previous palette items same in the "MAST/Etc/Temp" folder
            string paletteImagePath = MAST_Asset_Loader.GetMASTRootFolder() + "/Etc/Temp";
            if (Directory.Exists(paletteImagePath)) { Directory.Delete(paletteImagePath, true); }
            Directory.CreateDirectory(paletteImagePath);
            
            // Save prefabs
            state.prefabs = MAST_Palette.GetPrefabArray();
            
            // Define palette item tooltip array
            string[] paletteItemTooltip = new string[MAST_Palette.GetGUIContentArray().Length];
            
            // Get texture path to save palette images
            string texturePath = MAST_Asset_Loader.GetMASTRootFolder() + "/Etc/Temp/temp_palette_image_";
            
            // Loop through each item in the palette
            for (int i = 0; i < MAST_Palette.GetGUIContentArray().Length; i++)
            {
                // Get the tooltip from the palette GUIContent
                paletteItemTooltip[i] = MAST_Palette.GetGUIContentArray()[i].tooltip;
                
                // Encode this palette item image to PNG then save to disk
                byte[] bytes = MAST_Palette.GetTexture2DArray()[i].EncodeToPNG();
                File.WriteAllBytes(texturePath + i.ToString("000") + ".png", bytes);
            }
            
            // Save palette item tooltips and images (converted to byte arrays)
            state.paletteItemTooltip = paletteItemTooltip;
        }
        
        // Save scriptable object changes
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(state);
    }
    
    // ---------------------------------------------------------------------------
    // Load preferences from state scriptable object
    // ---------------------------------------------------------------------------
    public void Load_Palette_State()
    {
        // Get or scriptable object to store the interface state data
        Get_Reference_To_Scriptable_Object();
        
        // -----------------------------------------------
        // If there is no saved scriptable object
        // -----------------------------------------------
        if (state == null)
        {
            // Set selected draw tool and palette to -1 so nothing is selected
            MAST_Settings.gui.toolbar.selectedDrawToolIndex = -1;
            MAST_Palette.selectedItemIndex = -1;
            
            // There's no need to do anything to the prefab or GUIContent arrays
            // since they will already be blank
        }
        
        // -----------------------------------------------
        // If there is a scriptable object
        // -----------------------------------------------
        else
        {
            // Get palette item count
            int paletteItemCount = state.prefabs.Length;
            
            // Load palette item tooltips
            string[] paletteItemTooltip = state.paletteItemTooltip;
            
            // Create empty Texture2D array and empty GUIContent array
            Texture2D[] paletteTexture2D = new Texture2D[paletteItemCount];
            GUIContent[] paletteGuiContent = new GUIContent[paletteItemCount];
            
            // Get texture path to save palette images
            string texturePath = MAST_Asset_Loader.GetMASTRootFolder() + "/Etc/Temp/temp_palette_image_";
            
            // Loop through each palette item
            for (int i = 0; i < paletteItemCount; i++)
            {
                // Load Texture2D saved as PNG on disk back to palette item
                byte[] bytes = File.ReadAllBytes(texturePath + i.ToString("000") + ".png");
                paletteTexture2D[i] = new Texture2D(2, 2);
                paletteTexture2D[i].LoadImage(bytes);
            }
            
            // Copy palette GUI content to Palette class
            MAST_Palette.RestorePaletteData(state.prefabs, paletteTexture2D, paletteItemTooltip);
        }
    }
    
    // ---------------------------------------------------------------------------
    // Save grid state preferences to state scriptable object
    // ---------------------------------------------------------------------------
    public void Save_Interface_State()
    {
        // Get or create a scriptable object to store the interface state data
        Get_Reference_To_Scriptable_Object();
        
        // Save grid exists state
        state.gridExists = MAST_Grid_Manager.gridExists;
        
        // Save selected draw tool and palette
        state.selectedDrawToolIndex = MAST_Settings.gui.toolbar.selectedDrawToolIndex;
        state.selectedItemIndex = MAST_Palette.selectedItemIndex;
        
        // Save scriptable object changes
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.SetDirty(state);
    }
    
    // ---------------------------------------------------------------------------
    // Load grid state preferences from state scriptable object
    // ---------------------------------------------------------------------------
    public void Load_Interface_State()
    {
        // Get or scriptable object to store the interface state data
        Get_Reference_To_Scriptable_Object();
        
        // -----------------------------------------------
        // If there is no saved scriptable object
        // -----------------------------------------------
        if (state == null)
        {
            // Set grid exists to false
            MAST_Grid_Manager.gridExists = false;
            
            // Load selected draw tool and palette
            MAST_Settings.gui.toolbar.selectedDrawToolIndex = -1;
            MAST_Palette.selectedItemIndex = -1;
        }
        
        // -----------------------------------------------
        // If there is a scriptable object
        // -----------------------------------------------
        else
        {
            // Load grid exists state
            MAST_Grid_Manager.gridExists = state.gridExists;
            
            // Load selected draw tool and palette
            MAST_Settings.gui.toolbar.selectedDrawToolIndex = state.selectedDrawToolIndex;
            MAST_Palette.selectedItemIndex = state.selectedItemIndex;
        }
    }
    
    
    
#endif
}
