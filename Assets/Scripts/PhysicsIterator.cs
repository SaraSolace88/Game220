using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PhysicsIterator : MonoBehaviour
{
    [SerializeField]
    private int iterations = 1;

    protected GameObject[] gameObjects;
    protected List<Vector3> positions;
    protected List<Quaternion> rotations;

    //capture all object locations in scene
    public void SaveState()
    {
        gameObjects = FindObjectsOfType<GameObject>();
        positions.Clear();
        rotations.Clear();
        foreach(GameObject obj in gameObjects)
        {
            positions.Add(obj.transform.position);
            rotations.Add(obj.transform.rotation);
        }
    }

    //return objects to previously captured positions in scene
    public void LoadState()
    {
        int i = 0;
        foreach(GameObject obj in gameObjects)
        {
            obj.transform.position = positions[i];
            obj.transform.rotation = rotations[i];
            i++;
        }
    }

    //simulate physics for certain number of frames
    public void Simulate()
    {
        Physics.simulationMode = SimulationMode.Script;
        for (int i = 0; i < iterations; i++)
        {
            Physics.Simulate(Time.fixedDeltaTime);
        }
    }
}

//custom editor with 3 buttons
[CustomEditor(typeof(PhysicsIterator))]

public class PhysicsIteratorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PhysicsIterator customScript = (PhysicsIterator)target; //allows pulling functions from main class

        if (GUILayout.Button("Save State"))
        {
            customScript.SaveState();
        }
        if (GUILayout.Button("Load State"))
        {
            customScript.LoadState();
        }
        if (GUILayout.Button("Simulate"))
        {
            customScript.Simulate();
        }
    }
}
