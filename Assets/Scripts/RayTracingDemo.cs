using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RayTracingDemo : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10f)]
    private float rayLength;
    private RaycastHit hitInfo;
    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * rayLength, Color.yellow);
        if (Physics.Raycast(transform.position, Vector3.down, rayLength))
        {
            Debug.Log("I hit something");
        } 

        if(Physics.Raycast(transform.position,Vector3.down, out hitInfo, rayLength))
        {
            hitInfo.transform.localScale = new Vector3(.5f, .5f, .5f);
        }
    }
}
 