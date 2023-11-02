using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[ExecuteInEditMode]
public class MazeGenerator : MonoBehaviour
{
    //if no open direction - move to new block and check directions again
    [SerializeField]
    private int count;
    [SerializeField]
    private bool lWalls;
    [SerializeField]
    private GameObject floor, EastEnd, EastWall, HorizontalHall, northEastCorner, northEnd, northWall, northWestCorner, southEastCorner, southEnd, southWall, southWestCorner, verticalHall, wall, westEnd, westWall;
    [SerializeField]
    private List<Vector3> directions;
    private GameObject instantiatedPrefab;
    private Vector3 cachePosition, desiredDirection;
    private List<GameObject> flooring = new List<GameObject>(); //initialize the list
    private List<GameObject> walls = new List<GameObject>();
    private bool north, east, west, south;

    public void GeneratePrefabs()
    {
        foreach (GameObject go in flooring)  //delete all elements in the list
        {
            DestroyImmediate(go);
        }
        flooring.Clear(); //remove all elements in the list

        foreach (GameObject go in walls)  //delete all elements in the list
        {
            DestroyImmediate(go);
        }
        walls.Clear(); //remove all elements in the list

        cachePosition = transform.position;
        for (int i = 0; i < count; i++)
        {
            //randomize a direction
            desiredDirection = Direction(cachePosition, Random.Range(0, directions.Count));
            instantiatedPrefab = PrefabUtility.InstantiatePrefab(floor) as GameObject;
            instantiatedPrefab.transform.position = cachePosition + desiredDirection;
            flooring.Add(instantiatedPrefab); //adding new items to the list
            cachePosition = instantiatedPrefab.transform.position; //save the previous instantiated position
        }

        if (lWalls)
        {
            GenerateWalls();
        } 
    }

    public void GenerateWalls()
    {
        foreach(GameObject go in flooring)
        {
            north = true;
            east = true;
            south = true;
            west = true;
            
            //check for floor in each direction
            north = checkPosition(go.transform.position + directions[0]);
            east = checkPosition(go.transform.position + directions[1]);
            south = checkPosition(go.transform.position + directions[2]);
            west = checkPosition(go.transform.position + directions[3]);

            instantiatedPrefab = null;
            //spawn wall based on open directions
            if(north && !east && !south && !west)
            {//north
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(northWall) as GameObject;
            }
            else if (north && east && !south && !west)
            {//northEast
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(northEastCorner) as GameObject;
            }
            else if (north && !east && south && !west)
            {//northSouth
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(HorizontalHall) as GameObject;
            }
            else if (north && !east && !south && west)
            {//northWest
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(northWestCorner) as GameObject;
            }
            else if (north && !east && south && west)
            {//northSouthWest
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(westEnd) as GameObject;
            }
            else if (north && east && !south && west)
            {//northEastWest
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(northEnd) as GameObject;
            }
            else if (north && east && south && !west)
            {//northEastSouth
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(EastEnd) as GameObject;
            }
            else if(!north && east && !south && !west)
            {//east
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(EastWall) as GameObject;
            }
            else if (!north && east && south && !west)
            {//eastSouth
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(southEastCorner) as GameObject;
            }
            else if (!north && east && !south && west)
            {//eastWest
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(verticalHall) as GameObject;
            }
            else if (!north && east && south && west)
            {//eastSouthWest
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(southEnd) as GameObject;
            }
            else if (!north && !east && south && !west)
            {//south
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(southWall) as GameObject;
            }
            else if (!north && !east && south && west)
            {//southWest
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(southWestCorner) as GameObject;
            }
            else if (!north && !east && !south && west)
            {//west
                instantiatedPrefab = PrefabUtility.InstantiatePrefab(westWall) as GameObject;
            }
            if (north || east || south || west)
            {
                instantiatedPrefab.transform.position = go.transform.position;
                Debug.Log(instantiatedPrefab.transform.position);
                walls.Add(instantiatedPrefab);
            }
        }
    }

    private bool checkPosition(Vector3 pos)
    {
        foreach(GameObject go in flooring)
        {
            if (go.transform.position == pos)
            {
                return false;
            }
        }
        return true;
    }

    private Vector3 Direction(Vector3 start, int direction)
    {
        foreach(GameObject go in flooring)
        {
            if(go.transform.position == start + directions[direction])
            {
                start = go.transform.position;
                cachePosition = go.transform.position;
                return Direction(start, direction);
            }
        }
        return directions[direction];
    }

}

[CustomEditor(typeof(MazeGenerator))]
public class MazeGeneratorCI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MazeGenerator generator = (MazeGenerator)target;

        if (GUILayout.Button("Generate"))
        {
            generator.GeneratePrefabs();
        }
    }
}