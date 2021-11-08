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
        cylinder.localScale = new Vector3(cylinder.localScale.x, length, cylinder.localScale.z);

        topCap.localPosition = new Vector3(length, topCap.localPosition.y, topCap.localPosition.z);
        bottomCap.localPosition = new Vector3(-length, bottomCap.localPosition.y, bottomCap.localPosition.z);
    }

    public void SetRadius(float radius)
    {

    }
}
