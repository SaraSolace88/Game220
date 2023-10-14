using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class LerpPractical : MonoBehaviour
{

    [SerializeField]
    private bool bLive;

    [SerializeField]
    private Transform posA, posB;

    [SerializeField]
    private List<GameObject> targets = new List<GameObject>();

    [SerializeField]
    private GameObject pFab;

    private int count;

    private void Update()
    {
        if (bLive)
        {
            DoThingsPlease();
        }
    }

    public void DoThingsPlease()
    {
        count = targets.Count;
        foreach(GameObject gameObject in targets)
        {
            DestroyImmediate(gameObject);
        }
        targets.Clear();
        for (int i = 0; i < count; i++)
        {
            if(i == 0)
            {
                GameObject g = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
                g.transform.position = posA.position;
                targets.Add(g);
            }
            else
            {
                GameObject g = PrefabUtility.InstantiatePrefab(pFab) as GameObject;
                g.transform.position = posA.position;
                //multiple position based of angle - couldn't find right angle? or math wrong idfk
                g.transform.position = Vector3.Slerp(posA.position, posB.position, i / (float)count * Mathf.Cos(i/(float)count) * 2);

                targets.Add(g);
            }
        }
    }
}

[CustomEditor(typeof(LerpPractical))]
public class LerpPracticalCI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LerpPractical lerpPractical = (LerpPractical)target;

        if (GUILayout.Button("Generate"))
        {
            lerpPractical.DoThingsPlease();
        }
    }
}