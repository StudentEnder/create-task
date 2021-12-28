using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineFractalGenerator : FractalGenerator
{
    // Variables

    public GameObject lineObject;

    [Header("Scaling:")]
    [Tooltip("Ratio between length and radius of the capsules.")]
    public float lengthToRadiusRatio = 1f;
    [Tooltip("Length scalar applied to each depth relative to prior depth.")]
    public float lengthScalar = 1f;
    [Tooltip("Radius scalar applied to each depth relative to prior depth.")]
    public float radiusScalar = 1f;

    [Header("Rotation values:")]
    [Tooltip("Rotation applied to each depth relative to prior depth.")]
    public Quaternion rotationModifier;
    [Tooltip("Sets custom rotation of every Nth depth.")]
    public int skipStep = 1;

    [Header("Branching: (INCOMPLETE)")]
    [Tooltip("How many fractal branches to create at each branch point. 1 causes no branching.")]
    [Min(1)]
    public int branchesEachSplit = 1;
    [Tooltip("How many segments spawn between the start of each branch. 0 causes no branching. 1 causes branching at every segment.")]
    [Min(0)]
    public int branchIncrement = 10;

    private LineFractalSegment nextSegment; // saved to avoid construction when incrementing.

    public override void Init()
    {
        nextSegment = new LineFractalSegment();

        // always start with just one segment.
        previousDepthData = new FractalAtDepth(0, new LineFractalSegment(lineObject, lengthToRadiusRatio, 1f / lengthToRadiusRatio));
    }

    public override FractalAtDepth IncrementDepth()
    {
        depth++;
        // increment segment(s)
        LineFractalSegment[] nextSegments = new LineFractalSegment[branchesEachSplit];

        // TODO: Branching currently won't work because everything is still the same per branch, and there are no 'branching points'. Implement branching correctly.
        for (int i = 0; i < branchesEachSplit; i++)
        {
            // TODO test this cast. If polymorphism doesn't work like this (maintaining data), then make FractalAtDepth generic, taking LineFractalSegment as the specified type.
            LineFractalSegment prevSegment = (LineFractalSegment)previousDepthData.fractalSegments[i];

            nextSegment.Prefab = lineObject;
            nextSegment.Length = IncrementLength(prevSegment);
            nextSegment.Radius = IncrementRadius(prevSegment);

            nextSegment.Rotation = IncrementRotation(prevSegment);
            nextSegment.Scale = IncrementScale(prevSegment);

            nextSegment.Position = IncrementPosition(prevSegment, nextSegment);

            nextSegments[i] = nextSegment;
        }

        FractalAtDepth nextFractal = new FractalAtDepth(depth, nextSegments);

        previousDepthData = nextFractal; // update previous depth's data to be current depth's data.
        return nextFractal;
    }

    /// <summary>
    /// The incremented position of the segment. <br />
    /// Requires <paramref name="currentSegment"/>, so this should be called after calculating <see cref="IncrementLength(LineFractalSegment)">Length</see>, <see cref="IncrementRotation(LineFractalSegment)">Rotation</see>, and <see cref="IncrementScale(LineFractalSegment)">Scale</see>.
    /// </summary>
    /// <param name="prevSegment"></param>
    /// <param name="currentSegment"></param>
    /// <returns>Position coordinates in absolute space.</returns>
    private Vector3 IncrementPosition(LineFractalSegment prevSegment, LineFractalSegment currentSegment)
    {
        // TODO: Test Scale. Is x the correct scale component of the vector's magnitude?
        // direction and magnitude of objects.
        Vector3 segmentVector = currentSegment.Rotation * Vector3.right * (currentSegment.Length * currentSegment.Scale.x);

        // Efficiency: don't need to calculate this twice per depth because each depth's vector is already calculated above.
        // TODO replace this calculation with a call to cached vector in the previous segment's object.
        Vector3 prevSegmentVector = prevSegment.Rotation * Vector3.right * (prevSegment.Length * prevSegment.Scale.x);
        
        // TODO check this math
        return .5f * (prevSegmentVector + segmentVector);
    }

    private Quaternion IncrementRotation(LineFractalSegment prevSegment)
    {
        return prevSegment.Rotation * rotationModifier;
    }

    private Vector3 IncrementScale(LineFractalSegment prevSegment)
    {
        return prevSegment.Scale;
    }

    private float IncrementLength(LineFractalSegment prevSegment)
    {
        return prevSegment.Length * lengthScalar;
    }

    private float IncrementRadius(LineFractalSegment prevSegment)
    {
        return prevSegment.Radius * radiusScalar;
    }
}
