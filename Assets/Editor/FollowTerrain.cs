using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class FollowTerrain : EditorWindow
{
    public VisualTreeAsset vAsset;
    private ObjectField posA, posB;
    private Toggle draw, conform;
    private List<Transform> transforms = new List<Transform>();
    private RaycastHit hit;
    private GameObject ground;
    private LayerMask mask;
     
    private void OnEnable()
    {
        SceneView.duringSceneGui += SceneGUI;
        ground = FindAnyObjectByType<MeshCollider>().gameObject;
        mask = LayerMask.GetMask("Default");
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= SceneGUI;
    }
    //shows window
    [MenuItem("Raycast/Conform")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<FollowTerrain>();
        window.titleContent = new GUIContent("Custom window");
    }

    public void CreateGUI()
    {
        rootVisualElement.Add(vAsset.Instantiate());
        posA = rootVisualElement.Q<ObjectField>("PosA");
        posB = rootVisualElement.Q<ObjectField>("PosB");
        draw = rootVisualElement.Q<Toggle>("DrawLine");
        conform = rootVisualElement.Q<Toggle>("Conform");

        Button lockObjects = rootVisualElement.Q<Button>("LockObjects");
        lockObjects.clicked += () => AddToList();
        Button clearCache = rootVisualElement.Q<Button>("ClearCache");
        clearCache.clicked += () => ClearList();
    }

    public void AddToList()
    {
        ClearList();

        foreach (GameObject go in Selection.gameObjects)
        {
            transforms.Add(go.transform);
        }
    } 

    public void ClearList()
    {
        transforms.Clear();
    }
    private void SceneGUI(SceneView sceneView)
    {
        if (posA.value && posB.value)
        {
            Transform posALoc = posA.value as Transform;
            Transform posBLoc = posB.value as Transform;
            if (draw.value)
            {
                Debug.DrawLine(posALoc.position, posBLoc.position, Color.red);
            }
            //evenly spacing all objects in the list using the posA & posB
            
            if(conform.value)
            {
                for (int x = 0; x < transforms.Count; x++)
                {
                    Vector3 tmp = Vector3.Lerp(posALoc.position, posBLoc.position, x / (float)transforms.Count);
                    Vector3 tmp2 = transforms[x].position;
                    tmp.y = tmp2.y;
                    if (Physics.Raycast(tmp + Vector3.up * 20, Vector3.down, out hit, 30f, mask))
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        tmp.y = hit.point.y;
                    }
                    transforms[x].position = tmp;
                }
            }
            else
            {
                for (int x = 0; x < transforms.Count; x++)
                {
                    transforms[x].position = Vector3.Lerp(posALoc.position, posBLoc.position, x / (float)transforms.Count);
                }
            }
        }
    }
}
