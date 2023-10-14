using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Spiral : MonoBehaviour
{
    [SerializeField]
    private bool bGenerate;
    [SerializeField]
    private GameObject pFab;
    void Update()
    {
        if (bGenerate)
        {
            Generate();
            bGenerate = false;
        }
    }

    private void Generate() 
    {
        GameObject go;
        go = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
        go.transform.position = transform.position;
        go = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
        go.transform.position = transform.position + transform.forward;
        go = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
        go.transform.position = transform.position + transform.forward + transform.right;
        go = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
        go.transform.position = transform.position - transform.forward + transform.right;
        go = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
        go.transform.position = transform.position - transform.forward - transform.right;
        go = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
        go.transform.position = transform.position + transform.forward - transform.right;

    }
}
