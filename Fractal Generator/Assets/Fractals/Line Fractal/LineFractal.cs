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
    [Tooltip("Scalar applied to each depth relative to prior depth.")]
    public float scalar = 1f;

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
    }

    /// <summary>
    /// Instantiates fractal objects up to specified depth.
    /// </summary>
    /// <param name="maxDepth"></param>
    public void SpawnLines(int maxDepth)
    {
        for (int depth = 0; depth <= maxDepth; depth++)
        {
            GameObject newLine = Instantiate(lineObject, transform);
            newLine.transform.localPosition = Offset(depth) + (depth * offsetDirectionAdditive);
            newLine.transform.localRotation = Rotation(depth);
            newLine.GetComponent<LengthCapsule>().SetLength(Scale(depth));
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
    /// Returns scale of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public float Scale(int currentDepth)
    {
        float scale = Mathf.Pow(scalar, currentDepth);
        return scale;
    }

    /// <summary>
    /// Returns vector of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public Vector3 Vector(int currentDepth)
    {
        Vector3 vector = Vector3.right;
        vector = Rotation(currentDepth) * vector;
        vector = Scale(currentDepth) * vector;

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
