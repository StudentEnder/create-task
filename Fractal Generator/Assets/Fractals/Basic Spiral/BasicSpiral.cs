using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpiral : RecursiveObjectFractal
{
    [Header("Basic Spiral")]
    public GameObject childObject;
    public Quaternion childRotation;
    public Vector3 childScaleMultiplier = Vector3.one;

    public Transform topTransform;
    public Transform bottomTransform;

    public override void Prepare(RecursiveObjectFractal parent)
    {
        transform.localScale = childScaleMultiplier;
        transform.localRotation = childRotation;
    }

    public override void CreateChildren()
    {
        Instantiate(childObject, topTransform.position, Quaternion.identity).GetComponent<BasicSpiral>().Initialize(this);
        //Instantiate(gameObject, topTransform.position, childRotation, transform).GetComponent<BasicSpiral>().Initialize(this);
    }
}
