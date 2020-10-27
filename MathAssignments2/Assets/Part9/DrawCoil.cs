using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawCoil : MonoBehaviour
{
    [SerializeField, Min(1)] private int turns = 5;
    [SerializeField, Min(0.25f)] private float radius = 1f;
    [SerializeField, Min(1)] private float height = 4f;
    [SerializeField, Range(5, 50)] private int smoothness = 20;
    [SerializeField] private Color startColor = Color.red;
    [SerializeField] private Color endColor = Color.green;
    [SerializeField] private bool torus = false;

    private const float TAU = 6.28f;

    private void OnDrawGizmos()
    {
        int totalPoints = turns * smoothness;
        
        Vector3[] pointArray = new Vector3[totalPoints];

        for (int i = 0; i < totalPoints; i++)
        {
            float t = i / (totalPoints - 1f);

            pointArray[i] = torus ? CalculateTorusPoint(t, height, radius, turns) : CalculateCoilPoint(t, height, radius, turns);
            
            

        }

        for (int i = 0; i < pointArray.Length - 1; i++)
        {
            float t = i / (totalPoints - 1f);
            Handles.color = Color.Lerp(startColor, endColor, t);
            Handles.DrawAAPolyLine(pointArray[i], pointArray[i +1]);
            
        }
        
    }

    private static Vector3 CalculateCoilPoint(float t, float height, float radius, int totalPoints)
    {
        float tWinding = t * totalPoints;
        float angRad = tWinding * TAU;
        Vector3 point = AngleToDirection(angRad) * radius;
        point.z = height * t;
        return point;
    }
    
    static Vector3 CalculateTorusPoint(float t, float circumference, float radius, int turns)
    {
        float torusRadius = circumference / TAU;
        Vector3 corePoint = AngleToDirection(t * TAU) * torusRadius;

        float tWinding = t * turns;
        float angRad = tWinding * TAU;

        Vector2 localPoint = AngleToDirection(angRad) * radius;
        Vector3 xLocal = corePoint.normalized;
        Vector3 yLocal = Vector3.forward;

        return corePoint + localPoint.x * xLocal + localPoint.y * yLocal;
    }

    private static Vector2 AngleToDirection(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
}
