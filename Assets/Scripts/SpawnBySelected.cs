using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnBySelected : MonoBehaviour
{
    [SerializeField]
    private GameObject pFab;

    [SerializeField]
    private GameObject[] gObjects = new GameObject[0];

    public void Spawn()
    {
        gObjects = Selection.gameObjects;

        foreach (GameObject obj in gObjects)
        {
            PrefabUtility.InstantiatePrefab(pFab, obj.transform);
            obj.transform.DetachChildren();
            DestroyImmediate(obj);
            gObjects = null;
        }
    }
}

[CustomEditor(typeof(SpawnBySelected))]

public class SpawnBySelectedInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpawnBySelected customScript = (SpawnBySelected)target;
        if (GUILayout.Button("Replace"))
        {
            customScript.Spawn();
        }


    }
}

