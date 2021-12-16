using System;
using UnityEngine;

public class LineFractal : MonoBehaviour
{
    public LengthCapsule lineObject;

    private Pool<LengthCapsule> pool;
    [Tooltip("Amount of objects to pool in object pooler")]
    public int poolSize = 100;

    // Variables

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

    /// <summary>
    /// Dispose the object pooler and its reference.
    /// </summary>
    public void Dispose()
    {
        pool?.Dispose(); // if pool isn't null (? operator), Dispose of it before assigning a new reference.
        pool = null; // just to be explicit. Definitely don't want to reference a disposed object pooler.
    }

    /// <summary>
    /// Prepares fractal to be generated.
    /// </summary>
    public void Init()
    {
        if (pool != null) Dispose(); // If pool already exists, empty before initialization.

        pool = new Pool<LengthCapsule>(lineObject, poolSize, transform);
    }

    // TODO remake Start (or OnEnable) to respect the editor buttons when in the editor, and Generate when in a build.

    /// <summary>
    /// Generates the fractal. 
    /// This first destroys the object's children, then respawns them with the current parameters.
    /// </summary>
    public void Generate()
    {
        if (pool == null) Init();

        pool.ReleaseAllPool();
        radiusToLengthRatio = 1f / lengthToRadiusRatio;

        SpawnLines(minDepth, maxDepth);
    }

    /// <summary>
    /// Destroys all this object's children.
    /// </summary>
    public void DestroyChildren()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
#endif
            //Debug.Log("lineFractal children destruction while in PLAY");
            foreach (Transform child in transform)
            {
                // Destroy only works normally when playing.
                Destroy(child.gameObject);
            }
#if UNITY_EDITOR
        }
        // TODO: test the #if tags in a game build (for no compiler errors when this function is called)
        else
        {
            // child destruction when in edit mode.
            //Debug.Log("lineFractal children destruction while in EDIT");
            // editor child destruction code from StackOverflow user Paul Delobbel: https://stackoverflow.com/questions/38120084/how-can-we-destroy-child-objects-in-edit-modeunity3d
            // decrements, deleting the first child, until no children are left.
            for (int i = transform.childCount; i > 0; i--)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }
#endif
    }

    /// <summary>
    /// Instantiates fractal objects up to specified depth.
    /// </summary>
    /// <param name="maxDepth"></param>
    public void SpawnLines(int minDepth, int maxDepth)
    {
        for (int depth = minDepth; depth < maxDepth; depth++)
        {
            SpawnLine(depth);
        }
    }

    /// <summary>
    /// Instantiates a single fractal object at the specified depth
    /// </summary>
    /// <param name="depth">Depth to spawn fractal object at</param>
    public void SpawnLine(int depth)
    {
        LengthCapsule newLine = pool.Get();
        newLine.transform.localPosition = Offset(depth);
        newLine.transform.localRotation = Rotation(depth);
        newLine.SetLength(Length(depth));
        newLine.SetRadius(Radius(depth));
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
   