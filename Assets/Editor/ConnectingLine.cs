using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class ConnectingLine : EditorWindow
{
    public VisualTreeAsset vAsset;
    private Toggle bLive;
    private ObjectField targetA, targetB;


    private void OnEnable()
    {
        SceneView.duringSceneGui += SceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= SceneGUI;
    }

    [MenuItem("Connecting/Connect")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<ConnectingLine>();
        window.titleContent = new GUIContent("My custom window");
    }

    public void CreateGUI()
    {
        rootVisualElement.Add(vAsset.Instantiate());

        bLive = rootVisualElement.Q<Toggle>("BLive");
        targetA = rootVisualElement.Q<ObjectField>("TargetA");
        targetB = rootVisualElement.Q<ObjectField>("TargetB");
    }

    private void SceneGUI(SceneView sceneView) 
    { 
        if (bLive.value && targetA.value && targetB.value)
        {
            Debug.DrawLine((targetA.value as Transform).position, (targetB.value as Transform).position, Color.green);
        }
    }
}
