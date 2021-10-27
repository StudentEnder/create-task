using UnityEngine;

public class OffsetTest : MonoBehaviour
{
    public GameObject lineObject;

    public int maxDepth = 1;

    public Vector3 offsetDirectionMultiplier = Vector3.right;
    public Vector3 offsetDirectionAdditive = Vector3.zero;
    public Vector3 scaleMultiplier = Vector3.one;

    // amount of rotation applied each depth.
    public Quaternion rotationAmount;

    public float scalar = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnLines();
    }

    public void SpawnLines()
    {
        for (int depth = 0; depth <= maxDepth; depth++)
        {
            //GameObject newLine = Instantiate(lineObject, transform.position + offsetDirectionMultiplier * Offset(depth) + (depth * offsetDirectionAdditive), Quaternion.identity, transform);

            GameObject newLine = Instantiate(lineObject, transform);
            newLine.transform.localPosition = (offsetDirectionMultiplier * Offset(depth)) + (depth * offsetDirectionAdditive);
            newLine.transform.localRotation = Quaternion.identity;
            newLine.transform.localScale = new Vector3(Scale(depth), newLine.transform.localScale.y , newLine.transform.localScale.z );
        }
    }

    public void QuatTest()
    {
        Quaternion quaternion = Quaternion.identity;
        Debug.Log("Original: " + quaternion);
    }

    public float Offset(int currentDepth)
    {
        float offset = 0f;
        for (int depth = 0; depth < currentDepth; depth++)
        {
            offset += Scale(depth + 1);
        } 

        // Final offset to account for placement being from the center of the object.
        return offset +.5f - (.5f * Scale(currentDepth));
    }

    public float Offset2(int currentDepth)
    {
        float offset = 0f;
        for (int depth = 0; depth <= currentDepth; depth++)
        {
            offset += Scale(depth + 1) + Scale(depth - (3f/5));
        }
        return offset;
    }

    public float Scale(float currentDepth)
    {
        float scale = Mathf.Pow(scalar, currentDepth);
        return scale;
    }
}
