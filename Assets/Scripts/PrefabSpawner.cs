using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class PrefabSpawner : MonoBehaviour
{
    [SerializeField]
    private bool bSpawn;
    [SerializeField]
    private GameObject prefab;

    // Update is called once per frame
    void Update()
    {
        if (bSpawn)
        {
            bSpawn = false;
            PrefabUtility.InstantiatePrefab(prefab);
        }
    }
}
