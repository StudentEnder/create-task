using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineFractalGenerator : FractalGenerator
{
    // Variables

    public GameObject lineObject;

    [Header("Depth:")]
    [Tooltip("Starting depth to spawn objects.")]
    public int minDepth = 0;
    [Tooltip("Ending depth to spawn objects.")]
    public int maxDepth = 1;

    [Header("Scaling:")]
    [Tooltip("Ratio between length and radius of the capsules.")]
    public float lengthToRadiusRatio = 1f;
    private float radiusToLengthRatio = 1f;
    [Tooltip("Length scalar applied to each depth relative to prior depth.")]
    public float lengthScalar = 1f;
    [Tooltip("Radius scalar applied to each depth relative to prior depth.")]
    public float radiusScalar = 1f;

    [Header("Rotation values:")]
    [Tooltip("Rotation applied to each depth relative to prior depth.")]
    public Quaternion rotationModifier;
    [Tooltip("Sets custom rotation of every Nth depth.")]
    public int skipStep = 1;

    [Header("Branching")]
    [Tooltip("How many fractal branches to create at each depth.")]
    public int branchAmount = 1;

    public override FractalAtDepth IncrementDepth()
    {
        FractalAtDepth nextFractal;
        nextFractal.depth = previousDepthData.depth + 1; // increment depth value.

        // increment segment(s)
        FractalSegment[] nextSegments = new FractalSegment[branchAmount];
        for (int i = 0; i < branchAmount; i++)
        {
            FractalSegment prevSegment = previousDepthData.fractalSegments[i];
            FractalSegment nextSegment;

            nextSegment.position = IncrementOffset(prevSegment);
            nextSegment.rotation = IncrementRotation(prevSegment);
            nextSegment.scale = prevSegment.scale; // Not currently changing scale

            nextSegments[i] = prevSegment;
        }
        nextFractal.fractalSegments = nextSegments;

        previousDepthData = nextFractal; // update previous depth's data to be current depth's data.
        return nextFractal;
    }

    private Quaternion IncrementRotation(FractalSegment prevSegment)
    {
        return prevSegment.rotation * rotationModifier;
    }

    private Vector3 IncrementOffset(FractalSegment prevSegment)
    {
        return Vector3.zero;
    }

    private float IncrementLength(FractalSegment prevSegment)
    {
        return 0;
    }

    private float IncrementRadius(FractalSegment prevSegment)
    {
        return 0;
    }

    private Vector3 IncrementVector(FractalSegment prevSegment)
    {
        return Vector3.zero;
    }


    /// <summary>
    /// Returns rotation of object for specified depth.
    /// </summary>
    /// <param name="currentDepth">The depth to calculate rotation for.</param>
    public Quaternion Rotation(int currentDepth)
    {
        if (skipStep != 0 && currentDepth % skipStep == 0) // skip a depth's rotation
        {
            return Quaternion.identity;
        }
        return MathUtils.QuatPow(rotationModifier, currentDepth);
    }

    /// <summary>
    /// Returns length of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public float Length(int currentDepth)
    {
        return lengthToRadiusRatio * Mathf.Pow(lengthScalar, currentDepth);
    }

    /// <summary>
    /// Returns radius of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public float Radius(int currentDepth)
    {
        return radiusToLengthRatio * Mathf.Pow(radiusScalar, currentDepth);
    }

    /// <summary>
    /// Returns vector of an object at specified depth, combining direction and magnitude of the object.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public Vector3 Vector(int currentDepth)
    {
        Vector3 vector = Vector3.right;
        vector = Rotation(currentDepth) * vector;
        vector *= Length(currentDepth);

        return vector;
    }

    /// <summary>
    /// Returns centered position of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public Vector3 Offset(int currentDepth)
    {
        Vector3 offset = Vector3.zero;
        if (currentDepth != 0)
        {
            for (int depth = 0; depth < currentDepth; depth++)
            {
                offset += .5f * (Vector(depth) + Vector(depth + 1));
            }

        }
        return offset;
    }
}
