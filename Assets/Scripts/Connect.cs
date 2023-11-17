using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Connect : MonoBehaviour
{
    [SerializeField][ContextMenuItem("Set Target", "SetTarget")] private Transform target;
    [SerializeField] private Color custColor;
    [SerializeField] private Vector3 lineOffSet;

#if UNITY_EDITOR
    public void SetTarget()
    {
        Undo.RecordObject(this, "Set Target");
        if(Selection.gameObjects.Length > 0)
        {
            target = Selection.gameObjects[0].transform;
        }
    }

    public void SetMultiTarget()
    {
        Undo.RecordObject(this, "Set Multiple Targets");
        if (Selection.gameObjects.Length > 0)
        {
            foreach(GameObject go in Selection.gameObjects)
            {
                if (go != Selection.gameObjects[0].transform)
                {
                    go.GetComponent<Connect>().target = Selection.gameObjects[0].transform;
                }
            }
            Selection.gameObjects[0].GetComponent<Connect>().target = null;
        }
    }
#endif

    private void OnDrawGizmos()
    {
        //correctly transforms the gizmo to match the local transform
        Gizmos.matrix = transform.localToWorldMatrix;
        if (target)
        {
            Gizmos.color = Color.green;
            Debug.DrawLine(transform.position + lineOffSet, target.position, custColor);
        }
        else
        {
            Gizmos.color = Color.red;
        }

        //uses the box collider for drawing gizmo
        Gizmos.DrawCube(Vector3.zero, transform.localScale);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Connect))]

public class ConnectCI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Connect c = (Connect)target;

        if (GUILayout.Button("Set Target"))
        {
            c.SetTarget();
        }
        if (GUILayout.Button("Set MultiTarget"))
        {
            c.SetTarget();
        }
    }
}
#endif