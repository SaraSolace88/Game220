using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class FundamentalPractical : MonoBehaviour
{
    [SerializeField] [Range(1,10)] private float scale;
    [SerializeField] private GameObject prefab, target;

    private List<GameObject> targets = new List<GameObject>();

    private void Update()
    {
        foreach (GameObject target in targets)
        {
            target.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
    public void SelectRigidbodies()
    {
        targets.Clear();
        Rigidbody[] rBodies = FindObjectsOfType<Rigidbody>();

        foreach (Rigidbody r in rBodies)
        {
            targets.Add(r.gameObject);
        }
        Selection.objects = targets.ToArray();
    }

    public void SpawnPrefab()
    {
        GameObject pfab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        pfab.transform.position = target.transform.position;
    }
}

[CustomEditor(typeof(FundamentalPractical))]
public class FundamentalPracticalCI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        FundamentalPractical fPrac = (FundamentalPractical)target;

        if (GUILayout.Button("Select Rigidbodies"))
        {
            fPrac.SelectRigidbodies();
        }

        if (GUILayout.Button("Spawn Prefab"))
        {
            fPrac.SpawnPrefab();
        }

    }
}
