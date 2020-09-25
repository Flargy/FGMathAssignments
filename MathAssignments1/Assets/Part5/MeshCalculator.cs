using UnityEngine;

public class MeshCalculator : MonoBehaviour
{
    [SerializeField] private Mesh mesh;

    

    public void Calculate()
    {
        float area = 0f;
        int[] triangles;
        Vector3[] vertecies;
        int vertex1, vertex2, vertex3;
        triangles = mesh.triangles;
        vertecies = mesh.vertices;
        

        for(int i = 0; i < triangles.Length; i += 3)
        {
            vertex1 = triangles[i];
            vertex2 = triangles[i + 1];
            vertex3 = triangles[i + 2];

            Vector3 point1 = vertecies[vertex1];
            Vector3 point2 = vertecies[vertex2] - point1;
            Vector3 point3 = vertecies[vertex3] - point1;

            area += Vector3.Cross(point2,point3).magnitude / 2;
            
        }

        Debug.Log(area);
    }
}
