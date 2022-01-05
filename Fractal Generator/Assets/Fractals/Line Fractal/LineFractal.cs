using System;
using UnityEngine;

public class LineFractal : MonoBehaviour
{
    public LengthCapsule lineObject;

    private Pool<LengthCapsule> pool;
    [Tooltip("Amount of objects to pool in object pooler")]
    public int poolSize = 100;

    [Header("Depth:")]
    [Tooltip("Starting depth to spawn objects.")]
    public int minDepth = 0;
    [Tooltip("Ending depth to spawn objects.")]
    public int maxDepth = 1;

    [SerializeField]
    private LineFractalGenerator generator;


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
        // Initialize the pool
        if (pool != null) Dispose(); // If pool already exists, empty it before initialization.
        pool = new Pool<LengthCapsule>(lineObject, poolSize, transform);

    }

    // TODO remake Start (or OnEnable) to respect the editor buttons when in the editor, and Generate when in a build.

    /// <summary>
    /// Generates the fractal.
    /// </summary>
    public void Generate()
    {
        if (pool == null) Init();

        pool.ReleaseAllPool(); // make all pool object segments available for use.
        //radiusToLengthRatio = 1f / lengthToRadiusRatio; // Defines inverse of lengthToRadiusRatio for future use.

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
    /// Instantiates fractal objects between <paramref name="minDepth"/> and <paramref name="maxDepth"/>.
    /// </summary>
    /// <param name="minDepth"></param>
    /// <param name="maxDepth"></param>
    public void SpawnLines(int minDepth, int maxDepth)
    {
        generator.Init(); // Initialize the fractal generator

        SpawnData(generator.IncrementForward(minDepth)); // generate data up to minDepth, and spawn the fractal at that depth.

        // Intended spawning: minDepth is inclusive, maxDepth is exclusive
        for (int depth = minDepth; depth < maxDepth-1; depth++)
        {
            SpawnData(generator.IncrementDepth()); // increment the data and spawn it.
        }
    }

    public void SpawnData(FractalAtDepth fractalAtDepth)
    {
        foreach(FractalSegment segment in fractalAtDepth.fractalSegments)
        {
            LengthCapsule newLine = pool.Get(); // This pool currently ignores the gameobject saved in the segment. TODO somehow allow variable prefab spawning, but with a pool?
            newLine.transform.localPosition = segment.Position;
            newLine.transform.localRotation = segment.Rotation;
            newLine.transform.localScale = segment.Scale;

            // TODO make LineFractal behavior generic, with the Length/Line subtypes specifiable, avoiding these casts and making this more reusable.
            newLine.SetLength(((LineFractalSegment)segment).Length);
            newLine.SetRadius(((LineFractalSegment)segment).Radius);
        }
    }
}
