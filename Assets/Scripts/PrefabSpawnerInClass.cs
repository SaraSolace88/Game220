using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PrefabSpawnerInClass : MonoBehaviour
{
    [SerializeField]
    private float distance;
    [SerializeField]
    private GameObject prefab;
    
    private bool bSpawn;

    void Update()
    {
        if (bSpawn)
        {
            bSpawn = false;
            Undo.IncrementCurrentGroup();
            if (!Physics.Raycast(transform.position, Vector3.forward, distance))
            {
                
                GameObject newObject = new GameObject("cube");
                Undo.RegisterCreatedObjectUndo(newObject, "created");
                Undo.RegisterCompleteObjectUndo(newObject, "create prefab and move");
                newObject.transform.position = transform.position + Vector3.forward * distance;
                PrefabUtility.InstantiatePrefab(prefab, newObject.transform);
            }
            if (!Physics.Raycast(transform.position, Vector3.back, distance))
            {
                GameObject newObject = new GameObject("cube");
                Undo.RegisterCreatedObjectUndo(newObject, "created");
                Undo.RegisterCompleteObjectUndo(newObject, "create prefab and move");
                newObject.transform.position = transform.position + Vector3.back * distance;
                PrefabUtility.InstantiatePrefab(prefab, newObject.transform);
            }
            if (!Physics.Raycast(transform.position, Vector3.right, distance))
            {
                GameObject newObject = new GameObject("cube");
                Undo.RegisterCreatedObjectUndo(newObject, "created");
                Undo.RegisterCompleteObjectUndo(newObject, "create prefab and move");
                newObject.transform.position = transform.position + Vector3.right * distance;
                PrefabUtility.InstantiatePrefab(prefab, newObject.transform);
            }
            if (!Physics.Raycast(transform.position, Vector3.left, distance))
            {
                GameObject newObject = new GameObject("cube");
                Undo.RegisterCreatedObjectUndo(newObject, "created");
                Undo.RegisterCompleteObjectUndo(newObject, "create prefab and move");
                newObject.transform.position = transform.position + Vector3.left * distance;
                PrefabUtility.InstantiatePrefab(prefab, newObject.transform);
            }
            
            Undo.SetCurrentGroupName("Create Prefabs");
        }
    }
    public void SetbSpawn(bool value)
    {
        bSpawn = value;
    }
}


[CustomEditor(typeof(PrefabSpawnerInClass))]
 
public class PrefabSpawnerInClassInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PrefabSpawnerInClass customScript = (PrefabSpawnerInClass)target;
        if (GUILayout.Button("Spawn"))
        {
            customScript.SetbSpawn(true);
        }


    }
}
