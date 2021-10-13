using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectFractal : MonoBehaviour
{
    [Header("Object Fractal")]
    [Tooltip("Current Depth")]
    [SerializeField]
    protected int depth; // current depth
    [Tooltip("Depth distance from final object spawn. This decrements until 0, as the final depth spawn.")]
    [SerializeField]
    protected int depthCountdown;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (depthCountdown > 0) // don't make more children if depthCountdown of 0 is reached.
        {
            CreateChildren();
        }
    }

    /// <summary>
    /// Initializes fractal spawn information. This must be called before the object can create children.
    /// </summary>
    /// <param name="parent"></param>
    public virtual void Initialize(ObjectFractal parent)
    {
        depth = parent.depth + 1; // increases recursion depth value
        depthCountdown = parent.depthCountdown - 1; // decrements depthCountdown from the parent's value.
        name = RenameDepth(parent.name, depth);
        Prepare(parent);
    }

    private static string RenameDepth(string parentString, int depth)
    {
        if (depth == 1) return parentString + " Depth: " + depth;
        string[] splitParentString = parentString.Split(' ');
        string finalString = "";
        for (int i = 0; i < splitParentString.Length - 1; i++)
        {
            finalString += splitParentString[i];
        }
        finalString += " " + depth;
        return finalString;
    }

    /// <summary>
    /// Fractal-specific initialization behavior. Called in <see cref="Initialize(ObjectFractal)"/>.
    /// </summary>
    /// <param name="parent"></param>
    public abstract void Prepare(ObjectFractal parent);

    /// <summary>
    /// Fractal-specific children creation behavior. Called in <see cref="Start"/> as <see cref="depthCountdown"/> is over 0. <see cref="Initialize(ObjectFractal)"/> must first be ran.
    /// </summary>
    public abstract void CreateChildren();

}
