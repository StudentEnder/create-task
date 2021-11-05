using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengthCapsule : MonoBehaviour
{
    [SerializeField] private Transform cylinder;
    [SerializeField] private Transform topCap;
    [SerializeField] private Transform bottomCap;

    public void SetScale(Vector3 scale)
    {
        Debug.Log(scale);
        scale *= .5f;
        cylinder.localScale = scale;

        topCap.localPosition = new Vector3(scale.x, topCap.localPosition.y, topCap.localPosition.z);
        bottomCap.localPosition = new Vector3(-scale.x, bottomCap.localPosition.y, bottomCap.localPosition.z);
    }
}
