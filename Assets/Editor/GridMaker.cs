using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class GridMaker : EditorWindow
{
    public VisualTreeAsset vAsset;
    private GameObject target;
    private List<Transform> transforms = new List<Transform>();
    private RaycastHit hit;
    private bool track;

    private void OnEnable()
    {
        SceneView.duringSceneGui += SceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= SceneGUI;
    }
    //shows window
    [MenuItem("Tools/GridMaker")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<GridMaker>();
        window.titleContent = new GUIContent("Custom window");
    }

    public void CreateGUI()
    {
        rootVisualElement.Add(vAsset.Instantiate());

        Button lockTarget = rootVisualElement.Q<Button>("LockTarget");
        lockTarget.clicked += () => LockTarget();
        Button lockSelection = rootVisualElement.Q<Button>("LockSelection");
        lockSelection.clicked += () => LockSelection();
        Button gridandTrack = rootVisualElement.Q<Button>("GridandTrack");
        gridandTrack.clicked += () => GridandTrack();
    }

    public void LockTarget()
    {
        target = Selection.activeGameObject;
    }

    public void LockSelection()
    {
        transforms.Clear();
        foreach (GameObject go in Selection.gameObjects)
        {
            transforms.Add(go.transform);
        }
    }

    public void GridandTrack()
    {
        track = true;
    }

    private void SceneGUI(SceneView sceneView)
    {
        if (target && track)
        {
            Debug.Log("track");
            int x = 0, z = 0;
            for (int i = 0; i < transforms.Count; i++)
            {
                if (x == 5)
                {
                    x = 0;
                    z++;
                }
                transforms[i].transform.position = target.transform.position + new Vector3(x, 0, z);
                x++;
                if (Physics.Raycast(transforms[i].transform.position + Vector3.up * 20, Vector3.down, out hit))
                {
                    transforms[i].position = hit.point;
                }
            }
        }
    }
}