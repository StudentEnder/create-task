using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFractalSegment : FractalSegment
{
    /// <summary>
    /// The length of the segment.
    /// </summary>
    public float Length { get; set; }

    /// <summary>
    /// The radius of the segment.
    /// </summary>
    public float Radius { get; set; }

    /// <summary>
    /// The direction and magnitude of the segment. 
    /// </summary>
    public Vector3 Vector { get; set; }

    /// <summary>
    /// Empty construction. Make sure to set values if used.
    /// </summary>
    public LineFractalSegment() : base() { }

    /// <summary>
    /// Constructor with every segment value specified.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="scale"></param>
    /// <param name="length"></param>
    /// <param name="radius"></param>
    /// <param name="vector"></param>
    public LineFractalSegment(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, float length, float radius, Vector3 vector) : base(prefab, position, rotation, scale)
    {
        Length = length;
        Radius = radius;
        Vector = vector;
    }

    /// <summary>
    /// Constructor with every segment value specified, except for the auto-calculated <see cref="Vector">Vector</see>.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="scale"></param>
    /// <param name="length"></param>
    /// <param name="radius"></param>
    public LineFractalSegment(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, float length, float radius) : base(prefab, position, rotation, scale)
    {
        Length = length;
        Radius = radius;
        CalculateVector();
    }

    /// <summary>
    /// Constructor taking a Transform in place of position, rotation, and scale.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="transform"></param>
    /// <param name="length"></param>
    /// <param name="radius"></param>
    /// <param name="vector"></param>
    public LineFractalSegment(GameObject prefab, Transform transform, float length, float radius, Vector3 vector) : base(prefab, transform)
    {
        Length = length;
        Radius = radius;
        Vector = vector;
    }

    /// <summary>
    /// LineFractalSegment with default starting values.
    /// </summary>
    /// <param name="prefab"></param>
    public LineFractalSegment(GameObject prefab) : base(prefab)
    {
        Length = 1;
        Radius = 1;
        CalculateVector();
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
        CalculateVector();
    }

    /// <summary>
    /// Gets the magnitude and direction of the segment, and assigns it to this segment's <see cref="Vector">Vector</see>.<br />
    /// Requires <see cref="Length">Length</see>, <see cref="FractalSegment.Rotation">Rotation</see>, and <see cref="FractalSegment.Scale">Scale</see> to calculate.
    /// </summary>
    /// <param name="segment"></param>
    public void CalculateVector()
    {
        Vector = Length * Scale.x * (Rotation * Vector3.right);
    }

    /// <summary>
    /// Copies the object's values into a new LineFractalSegment.
    /// </summary>
    /// <returns>Returns the new LineFractalSegment.</returns>
    public new LineFractalSegment Copy()
    {
        return new LineFractalSegment(Prefab, Position, Rotation, Scale, Length, Radius, Vector);
    }

}
