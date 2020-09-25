using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshCalculator))]
public class Assignment5Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MeshCalculator myScript = (MeshCalculator)target;
        if(GUILayout.Button("Calculate Area"))
        {
            myScript.Calculate();
        }
    }
}
