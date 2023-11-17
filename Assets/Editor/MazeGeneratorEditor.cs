using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

[ExecuteInEditMode]
public class MazeGeneratorEditor : EditorWindow
{
    //if no open direction - move to new block and check directions again
    private GameObject instantiatedPrefab, floor, EastEnd, EastWall, HorizontalHall, northEastCorner, northEnd, northWall, 
                       northWestCorner, southEastCorner, southEnd, southWall, southWestCorner, verticalHall, westEnd, westWall;
    
    private int count;
    private List<Vector3> directions = new List<Vector3> { new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(0, 0, -1), new Vector3(-1, 0, 0)};
    private Vector3 cachePosition, desiredDirection;
    private List<GameObject> flooring = new List<GameObject>(); //initialize the list
    private List<GameObject> walls = new List<GameObject>();
    private bool north, east, west, south, lWalls;

    public VisualTreeAsset vAsset;


    [MenuItem("MG/MGEditor")]

    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<MazeGeneratorEditor>();
        window.titleContent = new GUIContent("Maze Generation Menu");
    }

    public void CreateGUI()
    {
        rootVisualElement.Add(vAsset.Instantiate());

        lWalls = rootVisualElement.Q<Toggle>("ShowWalls").value;
        count = rootVisualElement.Q<IntegerField>("Amount").value;

        Button generate = rootVisualElement.Q<Button>("Generate");
        generate.clicked += () => GenerateFloors();

        floor = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Floor.prefab", typeof(GameObject));
        EastEnd = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/EastEnd.prefab", typeof(GameObject));
        EastWall = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/EastWall.prefab", typeof(GameObject));
        HorizontalHall = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/HorizontalHall.prefab", typeof(GameObject));
        northEastCorner = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/NorthEastCorner.prefab", typeof(GameObject));
        northEnd = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/NorthEnd.prefab", typeof(GameObject));
        northWall = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/NorthWall.prefab", typeof(GameObject));
        northWestCorner = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/NorthWestCorner.prefab", typeof(GameObject));
        southEastCorner = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SouthEastCorner.prefab", typeof(GameObject));
        southEnd = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SouthEnd.prefab", typeof(GameObject));
        southWall = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SouthWall.prefab", typeof(GameObject));
        southWestCorner = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/SouthWestCorner.prefab", typeof(GameObject));
        verticalHall = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/VerticalHall.prefab", typeof(GameObject));
        westEnd = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/WestEnd.prefab", typeof(GameObject));
        westWall = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/WestWall.prefab", typeof(GameObject));
    }

    public void GenerateFloors()
    {
        lWalls = rootVisualElement.Q<Toggle>("ShowWalls").value;
        count = rootVisualElement.Q<IntegerField>("Amount").value;

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

        GameObject tmp = (GameObject)rootVisualElement.Q<ObjectField>("Location").value;
        cachePosition = tmp.transform.position;

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