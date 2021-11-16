using UnityEngine;

public static class MathUtilsTests
{
    public static void Test()
    {
        Debug.Log("Vector3ComponentMultiply Test: " + TestVector3ComponentMultiply());
    }

    private static bool TestVector3ComponentMultiply()
    {
        bool passedTest = true;
        Vector3 vector1 = new Vector3(3f, 5f, 0f);
        Vector3 vector2 = new Vector3(1f, 2f, 13f);
        Vector3 vector3 = new Vector3(7f, -3f, 8f);
        Vector3 expectedOutput = new Vector3(21f, -30f, 0f);

        if (MathUtils.Vector3ComponentMultiply(vector1, vector2, vector3) != expectedOutput)
        {
            passedTest = false;
            Debug.Log("Vector3ComponentMultiply 3-parameter test failed.");
        }

        if (MathUtils.Vector3ComponentMultiply(expectedOutput, Vector3.one) != expectedOutput)
        {
            passedTest = false;
            Debug.Log("Vector3ComponentMultiply by Vector3.one test failed.");
        }

        if (MathUtils.Vector3ComponentMultiply(expectedOutput, Vector3.zero) != Vector3.zero)
        {
            passedTest = false;
            Debug.Log("Vector3ComponentMultiply by Vector3.zero test failed.");
        }

        return passedTest;
    }
}
