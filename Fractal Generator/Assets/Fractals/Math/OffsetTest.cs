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

    [Header("Rotation values:")] 
    public Quaternion rotationAmount;



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

    public void QuatTest()
    {
        Quaternion quaternion = Quaternion.identity;
        Debug.Log("Original: " + quaternion);
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

    /*  Old Offset function, optimized in new function
    public float Offset(int currentDepth)
    {
        float offset = 0f;
        for (int depth = 0; depth < currentDepth; depth++)
        {
            offset += Scale(depth + 1);
        } 

        // Final offset to account for placement being from the center of the object.
        return offset +.5f - (.5f * Scale(currentDepth));
    } */
    
    /// <summary>
    /// Returns centered position of object at specified depth.
    /// </summary>
    /// <param name="currentDepth"></param>
    /// <returns></returns>
    public float Offset(int currentDepth)
    {
        float offset = 0f;
        for (int depth = 0; depth < currentDepth; depth++)
        {
            offset += .5f * (Scale(depth) + Scale(depth++));
        }
        return offset;
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
