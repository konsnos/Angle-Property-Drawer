using UnityEngine;
using KonLab.Attributes;

public class Example : MonoBehaviour
{
    [Angle]
    public float angle;

    /// <summary>
    /// This angle has an offset of 90 degrees.
    /// </summary>
    [Angle(90f)]
    public float angle2;
}