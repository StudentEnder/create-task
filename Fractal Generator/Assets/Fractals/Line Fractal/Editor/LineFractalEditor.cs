using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineFractal))]
public class LineFractalEditor : Editor
{
    public override void OnInspectorGUI()
    {

        LineFractal lineFractal = (LineFractal)target;
        if (lineFractal == null) return;

        Undo.RecordObject(lineFractal, "Change LineFractal");

        if (GUILayout.Button("Dispose"))
        {
            lineFractal.Dispose();
        }

        if (GUILayout.Button("Initialize"))
        {
            lineFractal.Init();
        }

        DrawDefaultInspector();


        if (GUILayout.Button("Generate"))
        {
            lineFractal.Generate();
            // TODO: display a progress bar, or at least a completion message, when done generating.
            // TODO: Somehow prevent children generated in Edit mode being saved to the scene.
        }



    }
}
