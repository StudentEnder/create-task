using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data for a fractal at a single depth. Stores a depth with <see cref="FractalSegment"/>(s)
/// </summary>
public class FractalAtDepth
{
    /// <summary>
    /// The depth this data was calculated for.
    /// </summary>
    public int depth;

    /// <summary>
    /// Each segment at <see cref="depth">depth</see>. Multiple segments represent branches, all at one depth.
    /// </summary>
    public FractalSegment[] fractalSegments;

    /// <summary>
    /// Constructor for a list of segments at the depth.
    /// </summary>
    /// <param name="depth"></param>
    /// <param name="fractalSegments"></param>
    public FractalAtDepth(int depth, FractalSegment[] fractalSegments)
    {
        this.depth = depth;
        this.fractalSegments = fractalSegments;
    }

    /// <summary>
    /// Constructor for just one segment at the depth. This is stored as a 1 length array of segments.
    /// </summary>
    /// <param name="depth"></param>
    /// <param name="fractalSegment"></param>
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
    /// <summary>
    /// Prefab of the segment, supporting variable prefabs on each segment.
    /// </summary>
    public GameObject Prefab { get; set; }

    // transform data
    /// <summary>
    /// The position of the segment.
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    /// The rotation of the segment.
    /// </summary>
    public Quaternion Rotation { get; set; }

    /// <summary>
    /// The scale of the segment.
    /// </summary>
    public Vector3 Scale { get; set; }

    /// <summary>
    /// Empty construction. Make sure to set values if used.
    /// </summary>
    public FractalSegment() { } 

    /// <summary>
    /// Constructor specifying all values.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="scale"></param>
    public FractalSegment(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        Prefab = prefab;

        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    /// <summary>
    /// Constructor with Transform in place of position, rotation, and scale.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="transform"></param>
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

    /// <summary>
    /// Copies the object's values into a new FractalSegment.
    /// </summary>
    /// <returns>Returns the new FractalSegment.</returns>
    public FractalSegment Copy()
    {
        return new FractalSegment(Prefab, Position, Rotation, Scale);
    }
}
