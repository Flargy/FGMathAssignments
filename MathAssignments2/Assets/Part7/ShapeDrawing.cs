using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShapeDrawing : MonoBehaviour
{
    [SerializeField] private int numberOfVertexes = 3;
    [SerializeField] private int density = 1;

    const float TAU = 6.28318530718f;

    private Mesh mesh;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3[] vertecies = new Vector3[numberOfVertexes];
        for(int i = 1; i <= numberOfVertexes; i++)
        {
            float radian = i / (float)numberOfVertexes * TAU;
            vertecies[i - 1] = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0.0f);

            Gizmos.DrawSphere(vertecies[i - 1], 0.05f);
            Gizmos.color = Color.green;
        }

        Handles.color = Color.magenta;

        for(int i = 0; i < vertecies.Length - density; i++)
        {
            Handles.DrawLine(vertecies[i], vertecies[i + density]);
        }
        Handles.DrawLine(vertecies[0], vertecies[vertecies.Length - density]);
        if(density > 1)
        {
            Handles.DrawLine(vertecies[numberOfVertexes - 1], vertecies[density - 1]);
        }

    }
}
