using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class Enum : MonoBehaviour
{
    public ComponentType componentType;

    public bool bPrint;

    private void Update()
    {
        /*
        if (bPrint)
        {
            bPrint = false;
            if (componentType == ComponentType.Rigidbody)
            {
                Debug.Log("Rigidbody type selected");
            }
            if (componentType == ComponentType.MeshRenderer)
            {
                Debug.Log("MeshRenderer type selected");
            }
            if (componentType == ComponentType.Collider)
            {
                Debug.Log("Collider type selected");
            }
        }
        */
    }
    public void SelectType()
    {
        if(componentType == ComponentType.Rigidbody)
            {
            Rigidbody[] rBodies = FindObjectsOfType<Rigidbody>(); //finds all the rigidbodies
            List<GameObject> list = new List<GameObject>(); //list of gameobjects

            foreach (Rigidbody r in rBodies) //create a list of gameobjects using the rigidbody list
            {
                list.Add(r.gameObject);
            }
            Selection.objects = list.ToArray();
            Debug.Log("Rigidbody type selected");
        }
        if (componentType == ComponentType.MeshRenderer)
        {
            MeshRenderer[] mRenderers = FindObjectsOfType<MeshRenderer>(); //finds all the rigidbodies
            List<GameObject> list = new List<GameObject>(); //list of gameobjects

            foreach (MeshRenderer r in mRenderers) //create a list of gameobjects using the rigidbody list
            {
                list.Add(r.gameObject);
            }
            Selection.objects = list.ToArray();
            Debug.Log("MeshRenderer type selected");
        }
        if (componentType == ComponentType.Collider)
        {
            Collider[] coll = FindObjectsOfType<Collider>(); //finds all the rigidbodies
            List<GameObject> list = new List<GameObject>(); //list of gameobjects

            foreach (Collider r in coll) //create a list of gameobjects using the rigidbody list
            {
                list.Add(r.gameObject);
            }
            Selection.objects = list.ToArray();
            Debug.Log("Collider type selected");
        }
    }
}


[CustomEditor(typeof(Enum))]

public class EnumInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Enum customScript = (Enum)target;
        base.OnInspectorGUI();
        if(GUILayout.Button("Select Component Type"))
        {
            customScript.SelectType();
            /*
            Rigidbody[] rBodies = FindObjectsOfType<Rigidbody>(); //finds all the rigidbodies
            List<GameObject> list = new List<GameObject>(); //list of gameobjects

            foreach(Rigidbody r in rBodies) //create a list of gameobjects using the rigidbody list
            {
                list.Add(r.gameObject);
            }
            Selection.objects = list.ToArray(); //convert list to array is then selected
            */
        }
    }
}
public enum ComponentType { Rigidbody, MeshRenderer, Collider }