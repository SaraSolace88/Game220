using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class LerpDemo : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    private float range;

    [SerializeField]
    private bool bPrint;

    [SerializeField]
    private Transform posA, posB;

    private void Update()
    {
        if (posA && posB)
        {
            transform.position = Vector3.Lerp(posA.position, posB.position, range);
        }
        /*if (bPrint)
        {
            bPrint = false;
            Debug.Log(1 / (float)2);
        }*/
    }
}
