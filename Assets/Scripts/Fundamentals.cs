using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class Fundamentals : MonoBehaviour
{
    /*public GameObject[] gameObjects;
    public string[] firstNames;
    public List<string> lastNames;

    public bool bPrint;

    private void Update()
    {
        if (bPrint)
        {
            bPrint = false;
            //array
            for(int x = 0; x < firstNames.Length; x++)
            {
                Debug.Log(firstNames[x]);
            }
            //list
            for (int x = 0; x < lastNames.Count; x++)
            {
                Debug.Log(lastNames[x]);
            }
        }
    }*/
    public float number;
    public string firstName;

    public void PrintName()
    {
        Debug.Log(firstName + " " +  number);
    }

}

[CustomEditor(typeof(Fundamentals))]

public class FundamentalsInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Fundamentals customScript = (Fundamentals)target;

        if (GUILayout.Button("Print"))
        {
            customScript.PrintName();
        }
    }
}
