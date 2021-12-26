using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FractalGenerator
{
    protected int depth = 0;
    protected FractalAtDepth previousDepthData;

    /// <summary>
    /// Increments depth data by <paramref name="depthsForward"/> depths.
    /// </summary>
    /// <param name="depthsForward"></param>
    /// <returns>Returns the final depth</returns>
    /// 
    public FractalAtDepth IncrementDepth(int depthsForward)
    {
        FractalAtDepth fractal = IncrementDepth();
        for (int i = 1; i < depthsForward; i++)
        {
            fractal = IncrementDepth();
        }

        return fractal;
    }

    public abstract FractalAtDepth IncrementDepth();
}
