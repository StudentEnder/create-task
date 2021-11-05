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

    public static Vector3 Vector3Pow(Vector3 vector, int power)
    {
        return new Vector3(Mathf.Pow(vector.x, power), Mathf.Pow(vector.y, power), Mathf.Pow(vector.z, power));
    }

    public static Vector3 Vector3Pow(Vector3 vector, Vector3 power)
    {
        return new Vector3(Mathf.Pow(vector.x, power.x), Mathf.Pow(vector.y, power.y), Mathf.Pow(vector.z, power.z));
    }

    public static Vector3 Vector3ComponentMultiply(Vector3 a, Vector3 b)
    {
        Vector3 output = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            output[i] = a[i] * b[i];
        }
        return output;
    }
}
