using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class UIDemo : EditorWindow
{
    public VisualTreeAsset vAsset;
    public GameObject preFab;
    private Toggle bMakeLive;
    private IntegerField age;
    private ObjectField target;

    //shows window
    [MenuItem("UIDemo/Monkey")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<UIDemo>();
        window.titleContent = new GUIContent("My custom window");
    }

    public void CreateGUI()
    {
        rootVisualElement.Add(vAsset.Instantiate());

        bMakeLive = rootVisualElement.Q<Toggle>("MakeLive");
        age = rootVisualElement.Q<IntegerField>("Age");
        target = rootVisualElement.Q<ObjectField>("Target");

        Button printStuff = rootVisualElement.Q<Button>("Print");
        printStuff.clicked += () => PrintStuff();
    }

    private void PrintStuff()
    {
        Debug.Log("Make live is " + bMakeLive.value + " I am " + age.value + " years old.");
        if (preFab)
        {
            Transform targetTransform = target.value as Transform;
            GameObject go = PrefabUtility.InstantiatePrefab(preFab) as GameObject;
            go.transform.position = targetTransform.position;
        }
    }
}

