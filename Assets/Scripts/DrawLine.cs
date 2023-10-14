using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrawLine : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    // Update is called once per frasme
    void Update()
    {
        if (target)
        {
            Debug.DrawLine(transform.position, target.position, Color.magenta);
        }
    }
}
