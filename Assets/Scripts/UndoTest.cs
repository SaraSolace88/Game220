using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

public class UndoTest : MonoBehaviour
{
    [SerializeField]
    private bool bMove;

    [SerializeField]
    private Transform[] gameObjects;
    void Update()
    {
        if (bMove)
        {
            bMove = false;
            gameObjects = FindObjectsOfType<Transform>();
            //record change
            Undo.RecordObjects(gameObjects, "Move object up");
            //make change
            foreach(Transform go in gameObjects)
            {
                go.transform.position += Vector3.up;
            }
            
        }
    }
}
