using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToSurface : MonoBehaviour
{
    public bool bSnap;
    public float offset = 0f;

    private RaycastHit hitInfo;

    void Update()
    {
        if (bSnap)
        {
            bSnap = false;
            Undo.RecordObject(gameObject.transform, "Snap to surface");
            if (Physics.Raycast(transform.position, Vector3.down, out hitInfo))
            {
                gameObject.transform.position = hitInfo.point + new Vector3(0, offset, 0);
            }
        }
    }
}