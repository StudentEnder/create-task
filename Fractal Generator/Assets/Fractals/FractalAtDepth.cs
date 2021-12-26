using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data for a fractal at a single depth. Stores a depth with <see cref="FractalSegment"/>(s)
/// </summary>
public struct FractalAtDepth
{
    
    public int depth;

    public FractalSegment[] fractalSegments;

    //public static FractalAtDepth zero = 

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
public struct FractalSegment
{
    public GameObject prefab;
    // transform data
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public FractalSegment(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        this.prefab = prefab;

        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
    }

    public FractalSegment(GameObject prefab, Transform transform)
    {
        this.prefab = prefab;

        position = transform.localPosition;
        rotation = transform.localRotation;
        scale = transform.localScale;
    }

    /// <summary>
    /// Segment with empty data
    /// </summary>
    /// <param name="prefab"></param>
    public FractalSegment(GameObject prefab)
    {
        this.prefab = prefab;

        position = Vector3.zero;
        rotation = Quaternion.identity;
        scale = Vector3.zero;
    }
}
