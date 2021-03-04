using System.Collections.Generic;
using UnityEngine;

public class Shape
{
    public Vector2 Position;
    public List<Vector2> Points;

    public Shape(Vector2 position)
    {
        this.Position = position;
        Points = new List<Vector2>();
        
        Points.Add(new Vector2(-0.5f, -0.5f));
        Points.Add(new Vector2(0, 0.5f));
        Points.Add(new Vector2(0.5f, -0.5f));
    }
    
    public Shape()
    {
        this.Position = Vector3.zero;
        Points = new List<Vector2>();
        
        Points.Add(new Vector2(-0.5f, -0.5f));
        Points.Add(new Vector2(0, 0.5f));
        Points.Add(new Vector2(0.5f, -0.5f));
    }

    public void UpdatePosition(Vector2 newPos)
    {
        Position = newPos;
    }
}
