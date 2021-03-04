using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SeparatingAxisTheorem : MonoBehaviour
{
    [SerializeField] private Transform position1;
    [SerializeField] private Transform position2;
    
    [SerializeField] private List<Vector2> pos1Points = new List<Vector2>();
    [SerializeField] private List<Vector2> pos2Points = new List<Vector2>();

    Shape shape1 = new Shape();
    Shape shape2 = new Shape();

    private void OnDrawGizmos()
    {
        shape1.UpdatePosition(position1.position);
        shape2.UpdatePosition(position2.position);
        
        Handles.color = Color.red;
        for (int i = 0; i < shape1.Points.Count; i++)
        {
            int j = i + 1;
            
            Handles.DrawLine(shape1.Position + shape1.Points[i], shape1.Position + shape1.Points[j % 3]);
        }
        
        for (int i = 0; i < shape2.Points.Count; i++)
        {
            int j = i + 1;
            
            Handles.DrawLine(shape2.Position + shape2.Points[i], shape2.Position + shape2.Points[j % 3]);
        }
        
        
        // Test starts here

        if (CollisionTest(shape1, shape2, 0, false))
        {
            Debug.Log("there was collision");
        }
        else
        {
            Debug.Log("there was no collision");
        }

    }

    private bool CollisionTest(Shape poly1, Shape poly2, int iteration, bool looped)
    {
        Vector2 normalVec;
        int j = iteration + 1;
        if (j >= poly1.Points.Count)
        {
            j = 0;
        }
        normalVec.x = -(poly1.Points[j].y - poly1.Points[iteration].y);
        normalVec.y = poly1.Points[j].x - poly1.Points[iteration].x;

        normalVec.Normalize();


        float p1min = Vector2.Dot(normalVec, poly1.Points[0]);
        float p1max = p1min;

        for (int i = 0; i < poly1.Points.Count; i++)
        {
            float dot = Vector2.Dot(normalVec, poly1.Points[i]);
            p1min = Mathf.Min(p1min, dot);
            p1max = Mathf.Max(p1max, dot);
        }

        Vector2 offset = new Vector2(poly1.Position.x - poly2.Position.x, poly1.Position.y - poly2.Position.y);
        float polyOffset = Vector2.Dot(normalVec, offset);
        p1max += polyOffset;
        p1min += polyOffset;
        
        float p2min = Vector2.Dot(normalVec, poly2.Points[0]);
        float p2max = p2min;

        for (int i = 0; i < poly2.Points.Count; i++)
        {
            float dot = Vector2.Dot(normalVec, pos2Points[i]);
            p2min = Mathf.Min(p2min, dot);
            p2max = Mathf.Max(p2max, dot);
        }

        if (p1min - p2max > 0 || p2min - p1max > 0)
        {
            return false;
        }
        if(iteration + 1 < poly1.Points.Count)
        {
            return CollisionTest(poly1, poly2, iteration + 1, looped);
        }
        if (iteration == poly1.Points.Count && looped == false)
        {
            return CollisionTest(poly2, poly1, 0, true);
        }

        return true;
    }
}
