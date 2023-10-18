using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class MediumAlignment : MonoBehaviour
{
    [SerializeField]
    private bool bLive;
    [SerializeField]
    [Range(1, 100)]
    private float curveIntensity = 1;
    [SerializeField]
    private Transform positionA,positionB;
    [SerializeField]
    private AnimationCurve curve;

    [SerializeField]
    private List<Transform> targets = new List<Transform>();

    // Update is called once per frame
    void Update()
    {
        if (bLive)
        {
            Align(); 
        }

    }
    public void Align()
    {
        positionA.rotation = Quaternion.LookRotation(positionB.position - positionA.position, positionA.up);

        if (positionA && positionB)
        {
            float spacing = 1 / (float)targets.Count;
            float rate = spacing;
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].position = Vector3.Lerp(positionA.position, positionB.position, rate) 
                    + curve.Evaluate(rate) * positionA.up * curveIntensity;
                rate += spacing;
            }
        }
    }
}

[CustomEditor(typeof(MediumAlignment))]
public class MediumAlignmentCI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MediumAlignment basicAlignment = (MediumAlignment)target;

        if (GUILayout.Button("Align"))
            basicAlignment.Align();
    }
}