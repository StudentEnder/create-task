using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data for a fractal at a single depth. Stores a depth with <see cref="FractalSegment"/>(s)
/// </summary>
public class FractalAtDepth
{
    
    public int depth;

    public FractalSegment[] fractalSegments;

    public FractalAtDepth(int depth, FractalSegment[] fractalSegments)
    {
        this.depth = depth;
        this.fractalSegments = fractalSegments;
    }

    public FractalAtDepth(int depth, FractalSegment fractalSegment)
    {
        this.depth = depth;

        fractalSegments = new FractalSegment[1];
        fractalSegments[0] = fractalSegment;
    }
}

/// <summary>
/// Data for a single fractal segment, with a prefab, position, rotation, and scale.
/// </summary>
public class FractalSegment
{
    public GameObject Prefab { get; set; }
    // transform data
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }

    /// <summary>
    /// Empty construction. Make sure to set values if used.
    /// </summary>
    public FractalSegment() { } 

    public FractalSegment(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Prefab = prefab;

        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    public FractalSegment(GameObject prefab, Transform transform)
    {
        Prefab = prefab;

        Position = transform.localPosition;
        Rotation = transform.localRotation;
        Scale = transform.localScale;
    }

    /// <summary>
    /// FractalSegment with default starting values.
    /// </summary>
    /// <param name="prefab"></param>
    public FractalSegment(GameObject prefab)
    {
        Prefab = prefab;

        Position = Vector3.zero;
        Rotation = Quaternion.identity;
        Scale = Vector3.one;
    }
}
