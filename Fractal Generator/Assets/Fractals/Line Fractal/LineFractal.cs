using System;
using UnityEngine;

public class LineFractal : MonoBehaviour
{
    public GameObject lineObject;

    // Variables

    [Header("Depth:")]
    // [Tooltip("Starting depth to spawn objects.")]
    // public int minDepth = 0;
    [Tooltip("Ending depth to spawn objects.")]
    public int maxDepth = 1;

    [Header("Scaling:")]
    [Tooltip("Length scalar applied to each depth relative to prior depth.")]
    public float lengthScalar = 1f;
    [Tooltip("Radius scalar applied to each depth relative to prior depth.")]
    public float radiusScalar = 1f;

    [Header("Offset values:")]
    public Vector3 offsetDirectionMultiplier = Vector3.right;
    public Vector3 offsetDirectionAdditive = Vector3.zero;
    public Vector3 scaleMultiplier = Vector3.one;

    [Header("Rotation values:")] 
    public Quaternion rotationModifier;

    // remove?
    // public delegate float DepthFunction(int depth);



    // Start is called before the first frame update
    void Start()
    {
        SpawnLines(maxDepth);
        // MathUtilsTests.Test();
    }

    /// <summary>
    /// Instantiates fractal objects up to specified depth.
    /// </summary>
    /// <param name="maxDepth"></param>
    public void SpawnLines(int maxDepth)
    {
        for (int depth = 0; depth <= maxDepth; depth++)
        {
            LengthCapsule newLine = Instantiate(lineObject, transform).GetComponent<LengthCapsule>();
            newLine.transform.localPosition = Offset(depth) + (depth * offsetDirectionAdditive);
            newLine.transform.localRotation = Rotation(depth);
            newLine.SetLength(Length(depth));
            newLine.SetRadius(Radius(depth));
            // newLine.transform.localScale = 
        }
    }
    
    /// <summary>
    /// Returns rotation of object for specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public Quaternion Rotation(int currentDepth)
    {
        return MathUtils.QuatPow(rotationModifier, currentDepth);
    }

    /// <summary>
    /// Returns length of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public float Length(int currentDepth)
    {
        return Mathf.Pow(lengthScalar, currentDepth);
    }

    /// <summary>
    /// Returns radius of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public float Radius(int currentDepth)
    {
        return Mathf.Pow(radiusScalar, currentDepth);
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
        for (int depth = 0; depth < currentDepth; depth++)
        {
            offset += .5f * (Vector(depth) + Vector(depth + 1));
        }
        return offset;
    }
}
