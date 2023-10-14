using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    [SerializeField]
    protected int number;
    [SerializeField]
    protected float fraction;
    [Space(50)]
    public string objectName;
    public Color color;

    [Header("These are my special colors")]
    [ColorUsageAttribute(false, false)]
    public Color noAlpha;
    [ColorUsageAttribute(false, true)]
    public Color noAlphaHDR;
    [ColorUsageAttribute(true, true)]
    public Color alphaHDR;

    [Tooltip("This changes the speed of something")]
    [Range(0, 5)]
    public float speed;
}