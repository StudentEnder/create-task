using UnityEngine;

public static class KinematicUtils
{
    public static Vector3 DragForce(float dragCoefficient, Vector3 velocity)
    {
        return 0.5f * dragCoefficient * -velocity.normalized * velocity.sqrMagnitude;
    }

}