using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengthCapsule : MonoBehaviour
{
    [SerializeField] private Transform cylinder;
    [SerializeField] private Transform topCap;
    [SerializeField] private Transform bottomCap;

    public void SetLength(float length)
    {
        
        length *= .5f;
        // cylinder y axis changes because cylinder object starts vertical, but we rotated it horizontal in prefab.
        cylinder.localScale = new Vector3(cylinder.localScale.x, length, cylinder.localScale.z);

        topCap.localPosition = new Vector3(length, topCap.localPosition.y, topCap.localPosition.z);
        bottomCap.localPosition = new Vector3(-length, bottomCap.localPosition.y, bottomCap.localPosition.z);
    }

    public void SetRadius(float radius)
    {
        // cylinder y axis constant because y axis is parallel to cylinder direction
        cylinder.localScale = new Vector3(radius, cylinder.localScale.y, radius);

        topCap.localScale = new Vector3(radius, radius, radius);
        bottomCap.localScale = new Vector3(radius, radius, radius);
    }
}
