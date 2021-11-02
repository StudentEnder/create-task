using System;
using UnityEngine;

public class OffsetTest : MonoBehaviour
{
    public GameObject lineObject;



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

    // [Header("Rotation values:")] 
    // public Quaternion rotationAmount;



    public delegate float DepthFunction(int depth);



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
            newLine.transform.localPosition = (Offset(depth) * offsetDirectionMultiplier)  + (depth * offsetDirectionAdditive);
            newLine.transform.localRotation = Quaternion.identity;
            newLine.transform.localScale = new Vector3(Scale(depth), newLine.transform.localScale.y , newLine.transform.localScale.z );
        }
    }

    /// <summary>
    /// Returns scaling of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public float Scale(float currentDepth)
    {
        float scale = Mathf.Pow(scalar, currentDepth);
        return scale;
    }
    
    /// <summary>
    /// Returns centered position of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public float Offset(int currentDepth)
    {
        return Summation(0, currentDepth, (int depth) =>
       {
           return .5f * (Scale(depth) + Scale(depth + 1));
       });
    }

    public float Summation(int start, int max, DepthFunction f)
    {
        float sum = 0f;
        for (int depth = start; depth < max; depth++)
        {
            sum += f(depth);
        }

        return sum;
    }

    /// <summary>
    /// Returns rotation of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public Quaternion Rotate(float currentDepth)
    {
        return Quaternion.identity;
    }
}
