using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Generator : MonoBehaviour
{
    [SerializeField]
    private int count;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<Vector3> directions;
    private GameObject instantiatedPrefab;
    private Vector3 cachePosition, desiredDirection;

    private List<GameObject> prefabs = new List<GameObject>(); //initialize the list
   
    public void GeneratePrefabs()
    {
        cachePosition = transform.position;
        foreach (GameObject go in prefabs)  //delete all elements in the list
        {
            DestroyImmediate(go);
        }
        prefabs.Clear(); //remove all elements in the list

       for(int i = 0; i < count; i++) 
        {
            desiredDirection = Direction(cachePosition, directions[Random.Range(0,directions.Count)]);
            if(desiredDirection == Vector3.zero)
            {
                break;
            }
            else
            {
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                instantiatedPrefab.transform.position = cachePosition + desiredDirection;
                prefabs.Add(instantiatedPrefab); //adding new items to the list
                cachePosition = instantiatedPrefab.transform.position; //save the previous instantiated position
            }
        }
    }

    private Vector3 Direction(Vector3 start,Vector3 direction)
    {
        if (Physics.Raycast(start, direction))
            return Direction(start, directions[Random.Range(0, directions.Count)]);        
        else
            return direction;
    }

}
[CustomEditor(typeof(Generator))]
public class GeneratorCI :Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Generator generator = (Generator)target;

        if(GUILayout.Button("Generate"))
            generator.GeneratePrefabs();

    }
}