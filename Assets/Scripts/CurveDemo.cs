using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurveDemo : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 1f)]
    private float range;
    [SerializeField]
    private AnimationCurve animCurve;
    [SerializeField]
    private Transform target, posA, posB;
    private void Update()
    {
        if(target && posA && posB)
        {
            target.position = Vector3.Lerp(posA.position, posB.position, range) + Vector3.up * animCurve.Evaluate(range) * 5;
        }
    }
}
