using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class Movement : MonoBehaviour
{
    [SerializeField]    //This exsposes any variable in the Inspector
    [ContextMenuItem("Capture Position", "CaptureCurrentPosition")]
    [ContextMenuItem("Return","Return")]
    private Vector3 currentPosition;

    [SerializeField]
    [Range(0, 1)]
    private float blend;

    [SerializeField]
    private Transform targetA, targetB;

    private void Update()
    {
        if (targetA && targetB)
        {
            transform.position = Vector3.Lerp(targetA.position, targetB.position, blend);
        }
    }

    private void CaptureCurrentPosition()
    {
        currentPosition = transform.position;
    }

    private void Return()
    {
        transform.position = currentPosition;
    }

    private void Awake()
    {
        //Absolute first function
        Debug.Log("I am awake");
    }

    private void OnEnable()
    {
        Debug.Log("I was Enabled");
    }

    private void Start()
    {
        Debug.Log("I was started");
    }

    private void OnDisable()
    {
        Debug.Log("I was Disabled");
    }
}
