using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class CustomBox : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 10f)]
    private float Width;
    [SerializeField]
    [Range(1f, 10f)]
    private float Depth;
    [SerializeField]
    [Range(1f, 10f)]
    private float Height;
    [SerializeField]
    private bool bShowWalls;
    [SerializeField]
    private bool bLive;
    [SerializeField]
    private Transform frontWall;
    [SerializeField]
    private Transform backWall;
    [SerializeField]
    private Transform leftWall;
    [SerializeField]
    private Transform rightWall;
    [SerializeField]
    private Transform floor;

    private void Update()
    {
        if (!bShowWalls)
        {
            frontWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
            backWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
            leftWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
            rightWall.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            frontWall.gameObject.GetComponent<MeshRenderer>().enabled = true;
            backWall.gameObject.GetComponent<MeshRenderer>().enabled = true;
            leftWall.gameObject.GetComponent<MeshRenderer>().enabled = true;
            rightWall.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        if (bLive)
        {
            UpdateRoom();
        }
    }

    public void UpdateRoom()
    {
        //based on height, width, and depth change the transform of the objects

        floor.transform.localScale = new Vector3(Width, 1, Depth);

        rightWall.transform.localScale = new Vector3(1, Height, Depth);
        rightWall.transform.position = transform.position + new Vector3(Width/2 + .5f, Height/2 - .5f, 0);

        leftWall.transform.localScale = new Vector3(1, Height, Depth);
        leftWall.transform.position = transform.position + new Vector3(-Width/2 - .5f, Height / 2 - .5f, 0);

        backWall.transform.localScale = new Vector3(Width + 2, Height, 1);
        backWall.transform.position = transform.position + new Vector3(0, Height / 2 - .5f, Depth/2 + .5f);

        frontWall.transform.localScale = new Vector3(Width + 2, Height, 1);
        frontWall.transform.position = transform.position + new Vector3(0, Height / 2 - .5f, -Depth / 2 - .5f);

    }


}

[CustomEditor(typeof(CustomBox))]

public class CustomBoxInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CustomBox customScript = (CustomBox)target;
        if (GUILayout.Button("Update Room"))
        {
            customScript.UpdateRoom();
        }
    }
}