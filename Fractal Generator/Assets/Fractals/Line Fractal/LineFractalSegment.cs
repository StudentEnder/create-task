using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFractalSegment : FractalSegment
{
    public float Length { get; set; }
    public float Radius { get; set; }

    /// <summary>
    /// Empty construction. Make sure to set values if used.
    /// </summary>
    public LineFractalSegment() : base() { }

    public LineFractalSegment(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, float length, float radius) : base(prefab, position, rotation, scale)
    {
        Length = length;
        Radius = radius;
    }

    public LineFractalSegment(GameObject prefab, Transform transform, float length, float radius) : base(prefab, transform)
    {
        Length = length;
        Radius = radius;
    }

    /// <summary>
    /// LineFractalSegment with default starting values.
    /// </summary>
    /// <param name="prefab"></param>
    public LineFractalSegment(GameObject prefab) : base(prefab)
    {
        Length = 1;
        Radius = 1;
    }

    /// <summary>
    /// LineFractalSegment with default values except for specified <paramref name="length"/> and <paramref name="radius"/>.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="length"></param>
    /// <param name="radius"></param>
    public LineFractalSegment(GameObject prefab, float length, float radius) : base(prefab)
    {
        Length = length;
        Radius = radius;
    }

}
