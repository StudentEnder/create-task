using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static Quaternion QuatPow(Quaternion rotationModifier, int power)
    {
        Quaternion rotation = Quaternion.identity;
        for (int i = 0; i < power; i++)
        {
            rotation = rotation * rotationModifier;
        }
        return rotation;
    }
}
