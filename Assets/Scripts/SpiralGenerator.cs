using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SpiralGenerator : MonoBehaviour
{
    [SerializeField]
    private bool bLive;
    [SerializeField]
    private bool bRelative;
    [SerializeField]
    [Range(0, 1000)]
    private int amount;
    [SerializeField]
    private GameObject pFab;

    private Vector3 cachePosition;
    private int maxDirection, direction, count;
    private List<GameObject> prefabs = new List<GameObject>(); //initialize the list

    private void Update()
    {
        if (bLive)
        {
            GeneratePrefabs();
        }
        if (bRelative)
        {
            foreach (GameObject go in prefabs)
            {
                go.transform.position = transform.localToWorldMatrix * go.transform.position;
            }
        }
    }
    public void GeneratePrefabs()
    {
        count = 0;
        direction = 0;
        maxDirection = 2;
        cachePosition = transform.localPosition;

        foreach (GameObject go in prefabs)  //delete all elements in the list
        {
            DestroyImmediate(go);
        }
        prefabs.Clear(); //remove all elements in the list


        for (int i = 0; i < amount; i++)
        {
            if (i == 0)
            {
                GameObject g;
                g = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
                prefabs.Add(g);
            }
            else
            {
                if (count == maxDirection)
                {
                    if (direction == 3)
                    {
                        direction = 0;
                    }
                    else
                    {
                        direction++;
                    }
                    maxDirection += 1;
                    count = 0;
                }

                GameObject go;
                go = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
                go.transform.rotation = Quaternion.Euler(0, 0, 0);
                if (direction == 0)
                {
                    go.transform.localPosition = cachePosition + new Vector3(0, 0, 1);
                }
                else if (direction == 1)
                {
                    go.transform.localPosition = cachePosition + new Vector3(1, 0, 0);
                }
                else if (direction == 2)
                {
                    go.transform.localPosition = cachePosition + new Vector3(0, 0, -1);
                }
                else
                {
                    go.transform.localPosition = cachePosition + new Vector3(-1, 0, 0);
                }
                count++;
                cachePosition = go.transform.position;
                prefabs.Add(go);
            }
        }
    }
}

[CustomEditor(typeof(SpiralGenerator))]
public class SpiralGeneratorCI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SpiralGenerator spiralGenerator = (SpiralGenerator)target;

        if (GUILayout.Button("Generate"))
        {
            spiralGenerator.GeneratePrefabs();
        }
    }
}