using UnityEngine;

public class OffsetTest : MonoBehaviour
{
    public GameObject lineObject;

    public int maxDepth = 1;

    public Vector3 offsetDirectionMultiplier = Vector3.right;
    public Vector3 offsetDirectionAdditive = Vector3.zero;
    public Vector3 scaleMultiplier = Vector3.one;

    // Start is called before the first frame update
    void Start()
    {
        SpawnLines();
    }

    public void SpawnLines()
    {
        for (int depth = 0; depth < maxDepth; depth++)
        {
            GameObject newLine = Instantiate(lineObject, offsetDirectionMultiplier * Offset(depth) + (depth * offsetDirectionAdditive), Quaternion.identity);
            newLine.transform.localScale = Vector3.right * Scale(depth);
        }
    }

    public float Offset(int currentDepth)
    {
        float offset = 0f;
        for (int depth = 0; depth < currentDepth; depth++)
        {
            offset += 1f / Mathf.Pow(2, depth);
        }

        return offset;
    }

    public float Scale(int currentDepth)
    {
        float scale = 0f;
        for (int depth = 0; depth < currentDepth; depth++)
        {
            // TODO inverse scale for depth
        }

        return scale;
    }
}
