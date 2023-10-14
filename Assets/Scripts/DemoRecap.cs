using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DemoRecap : MonoBehaviour
{
    private void Update()
    {
        /*Physics.Simulate(Time.deltaTime); //
        Physics.Simulate(Time.fixedDeltaTime); //project setting time: called a set number of times per 
        Physics.simulationMode = SimulationMode.*/


    }

    public void ClassFunction()
    {
        Debug.Log("");
    }
}
[CustomEditor(typeof(DemoRecap))]

public class DemoRecapInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Click me please!"))
        {
            Debug.Log("Thank you for clicking");
        }
    }
}
