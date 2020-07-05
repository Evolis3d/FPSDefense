using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GeneratePlayableLevel : MonoBehaviour
{

    #region Crear el core minimo jugable para una escena
    
    [MenuItem("FPSDefense/2. Create Minimal Playable Core...", priority = 2)]
    public static void GenerateMinimalCore()
    {
        var juegoObj = GameObject.Find("GAME");
        if (juegoObj)
        {
            Destroy(juegoObj);
            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(juegoObj, "Delete " + juegoObj.name);
        }
        
        juegoObj = new GameObject(
            "GAME",
            typeof(SelectionChecker),
            typeof(ListaSlots),
            typeof(ExtraControls),
            typeof(WaveManager),
            typeof(WaveController),
            typeof(EnemyController)
            );
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(juegoObj, "Create " + juegoObj.name);

        if (EditorUtility.DisplayDialog(
            "IMPORTANT: Last step is manually required...",
            "You need to to feed some data to Components in order for CORE to work properly: \n \n" +
            "1) SELECTION CHECKER: Assign the mouse pointer image to the Cursor field. \n" +
            "2) EXTRA CONTROLS: Assign at least one prefab to the PrefabTrampa list. \n" +
            "3) WAVE MANAGER: Assign at least one wave asset to the list.",
            "OK",
            "Cancel"
            ))
        {
            Debug.Log("Minimal Playable Core created. Remember:");
            Debug.Log("1) SELECTION CHECKER: Assign the mouse pointer image to the Cursor field.");
            Debug.Log("2) EXTRA CONTROLS: Assign at least one prefab to the PrefabTrampa list.");
            Debug.Log("3) WAVE MANAGER: Assign at least one wave asset to the list.");
        }
        
    }
    
    #endregion

    #region Crear estructura de escena

    [MenuItem("FPSDefense/1. Create Scene Tree...", priority = 1)]
    public static void GenerateSceneTree()
    {
        var gofolder = new GameObject("DynamicLevel");
        var goitem = new GameObject("_--PUT HERE YOUR SLOTS AND SPAWNPOINTS--_");
        goitem.transform.SetParent(gofolder.transform);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(gofolder, "Create " + gofolder.name);
        
        gofolder = new GameObject("Level");
        goitem = new GameObject("_--PUT HERE YOUR LEVEL GEOMETRY AND THE GOAL ASSET--_");
        goitem.transform.SetParent(gofolder.transform);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(gofolder, "Create " + gofolder.name);
        
        gofolder = new GameObject("Enemies");
        goitem = new GameObject("_--ALL ENEMIES WILL SPAWN HERE INSIDE--_");
        goitem.transform.SetParent(gofolder.transform);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(gofolder, "Create " + gofolder.name);
        
        if (EditorUtility.DisplayDialog(
            "IMPORTANT: Last step is manually required...",
            "Remember to assign some Tags and Layers where to: \n \n" +
            "1) DYNAMIC LEVEL: All SLOTS need the Slot tag, SPAWNPOINTS need the Spawnpoint tag. \n" +
            "2) LEVEL: The GOAL needs the Goal tag. \n" ,
            "OK",
            "Cancel"
        ))
        {
            Debug.Log("Scene Tree created. Remember:");
            Debug.Log("1) Inside DYNAMIC LEVEL: Assign tags to all Slots and Spawnpoints accordingly.");
            Debug.Log("2) Inside LEVEL: Assign the goal tag to the Goal.");
        }
    }

    #endregion
}
