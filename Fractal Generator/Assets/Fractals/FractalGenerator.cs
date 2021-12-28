using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class FractalGenerator
{
    protected int depth = 0;
    protected FractalAtDepth previousDepthData;

    protected FractalGenerator() { } // prevent construction because it's used as serializable with Unity.

    /// <summary>
    /// Call Init before the generator is used.
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// Increments forward <paramref name="depthsForward"/> depths, essentially skipping in-between depths.
    /// </summary>
    /// <param name="depthsForward"></param>
    /// <returns>Returns the resulting data at the end of the increments.</returns>
    public FractalAtDepth IncrementForward(int depthsForward)
    {
        if (depthsForward < 0) return null;
        if (depthsForward == 0) return previousDepthData;

        FractalAtDepth fractal = IncrementDepth();
        for (int i = 1; i < depthsForward; i++)
        {
            fractal = IncrementDepth();
        }

        return fractal;
    }

    public abstract FractalAtDepth IncrementDepth();
}
