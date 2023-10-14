using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent (typeof(BoxCollider))]
public class CustomGizmo : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Uses box Collider for drawing gizmo")]
    private bool bBoxCollider;
    [SerializeField]
    private Color color = Color.green;
    private BoxCollider boxCollider;

    private void OnDrawGizmos()
    {
        //Gizmos don't render
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = color;

        boxCollider = GetComponent<BoxCollider>();
        Gizmos.DrawCube(boxCollider.center, boxCollider.size);
    }
}
