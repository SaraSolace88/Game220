using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class GridAllignment : EditorWindow
{
    public VisualTreeAsset vAsset;

    [MenuItem("Window/Grid")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow<GridAllignment>();
        window.titleContent = new GUIContent("My custom window");
    }

    public void CreateGUI()
    {
        rootVisualElement.Add(vAsset.Instantiate());

        Button printStuff = rootVisualElement.Q<Button>("Allign");
        printStuff.clicked += () => Allign();
    }

    private void Allign()
    {
        int x = 0, z = 0;
        foreach (GameObject obj in Selection.gameObjects) 
        {
            if(x == 5) 
            {
                x = 0;
                z++;
            }
            obj.transform.position = new Vector3( x, 0, z);
            x++;
        }
    }
}


