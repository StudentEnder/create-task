using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RecursiveObjectFractal : ObjectFractal
{   

    /// <summary>
    /// Initializes fractal spawn information. This must be called before the object can create children.
    /// </summary>
    /// <param name="parent"></param>
    public override void Initialize(ObjectFractal parent)
    {
        transform.parent = parent.transform; // sets the gameObject hierarchy to make children the children.
        base.Initialize(parent);
    }
}
