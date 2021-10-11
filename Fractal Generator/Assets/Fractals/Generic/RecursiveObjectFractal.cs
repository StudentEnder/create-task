using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecursiveObjectFractal : MonoBehaviour
{   
    [Header("Recursive Object Fractal")]
    [Tooltip("Current Depth")]
    [SerializeField]
    protected int depth; // current depth
    [Tooltip("Depth distance from final object spawn. This decrements until 0, as the final depth spawn.")]
    [SerializeField]
    protected int depthCountdown;

    //private bool initialized = false;

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
    public void Initialize(RecursiveObjectFractal parent)
    {
        transform.parent = parent.transform; // sets the gameObject hierarchy to make children the children.
        depth = parent.depth + 1; // increases recursion depth value
        depthCountdown = parent.depthCountdown - 1; // decrements depthCountdown from the parent's value.
        name = RenameDepth(parent.name, depth);
        Prepare(parent);
        //initialized = true;
    }

    private static string RenameDepth(string parentString, int depth)
    {
        if (depth == 1) return parentString + " Depth: " + depth;
        string[] splitParentString = parentString.Split(' ');
        string finalString = "";
        for (int i = 0; i < splitParentString.Length-1; i++)
        {
            finalString += splitParentString[i];
        }
        finalString += " " + depth;
        return finalString;
    }

    /// <summary>
    /// Fractal-specific initialization behavior. Called in <see cref="Initialize(RecursiveObjectFractal)"/>.
    /// </summary>
    /// <param name="parent"></param>
    public abstract void Prepare(RecursiveObjectFractal parent);

    /// <summary>
    /// Fractal-specific children creation behavior. Called in <see cref="Start"/> as <see cref="depthCountdown"/> is over 0. <see cref="Initialize(RecursiveObjectFractal)"/> must first be ran.
    /// </summary>
    public abstract void CreateChildren();

}
