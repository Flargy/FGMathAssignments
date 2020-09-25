using UnityEditor;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    [Range(0.5f, 2f)]
    [SerializeField] private float gunHeight = 1f;
    [Range(0.5f, 2f)]
    [SerializeField] private float barrelDistance = 1.0f;
    [Range(0.5f, 2f)]
    [SerializeField] private float barrelLength = 1.0f;

    private RaycastHit hit;

    private Vector3 hitlocation = Vector3.zero;
    private Vector3 normal = Vector3.zero;

    private Vector3 localRight;
    private Vector3 localForward;

    private Vector3[] corners;

    private Matrix4x4 transformMatrix;
   

    void ShootRaycast()
    {
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity))
        {
            hitlocation = hit.point;
            normal = hit.normal;
        }
    }

    private void DrawPosition(Vector3 pos)
    {
        Handles.color = Color.cyan;
        Handles.DrawLine(pos, transform.forward);
        Handles.color = Color.red;
        Handles.DrawLine(pos, transform.right);
        Handles.color = Color.green;
        Handles.DrawLine(pos, transform.up);
    }

    private void DrawBasisVectors()
    {
        Handles.DrawLine(hitlocation, normal + hitlocation);
        Handles.color = Color.red;
        localRight = Vector3.Cross(normal, transform.forward).normalized;
        Handles.DrawLine(hitlocation, localRight.normalized + hitlocation);
        Handles.color = Color.cyan;
        localForward = Vector3.Cross(localRight, normal).normalized;
        Handles.DrawLine(hitlocation, localForward + hitlocation);
    }

    private void DrawWireFrame()
    {
        corners = new Vector3[]{
             // bottom 4 positions:
            new Vector3( 1, 0, 1 ),
            new Vector3( -1, 0, 1 ),
            new Vector3( -1, 0, -1 ),
            new Vector3( 1, 0, -1 ),
            // top 4 positions:
            new Vector3( 1, 2, 1 ),
            new Vector3( -1, 2, 1 ),
            new Vector3( -1, 2, -1 ),
            new Vector3( 1, 2, -1 )
        }; 

        for(int i = 0; i < corners.Length; i ++)
        {
            corners[i] = transformMatrix.MultiplyPoint(corners[i]);
        }

        Handles.color = Color.white;

        for(int i = 0; i < corners.Length/2 - 1; i++)
        {
            Handles.DrawLine(corners[i], corners[i + 1]);
        }
        Handles.DrawLine(corners[0], corners[3]);

        for (int i = 4; i < corners.Length - 1; i++)
        {
            Handles.DrawLine(corners[i], corners[i + 1]);
        }
        Handles.DrawLine(corners[4], corners[7]);

        for (int i = 0; i < corners.Length / 2 ; i++)
        {
            Handles.DrawLine(corners[i], corners[i + 4]);
        }



    }


    private void DrawTurretBarrels()
    {
        float halfDist = barrelDistance / 2;
        Vector3 gunBarrelDistancePos = transformMatrix.MultiplyPoint(new Vector3(halfDist, gunHeight, 0f));
        Vector3 gunBarrelDistanceNeg = transformMatrix.MultiplyPoint(new Vector3(-halfDist, gunHeight, 0f));
        Vector3 gunTurretHeight = transformMatrix.MultiplyPoint(new Vector3(0f, gunHeight, 0f));
        Vector3 gunBarrelLengthPos = transformMatrix.MultiplyPoint(new Vector3(halfDist, gunHeight, barrelLength));
        Vector3 gunBarrelLengthNeg = transformMatrix.MultiplyPoint(new Vector3(-halfDist, gunHeight, barrelLength));

        Vector3 zeroPoint = transformMatrix.MultiplyPoint(Vector3.zero);

        Handles.color = Color.magenta;

        Handles.DrawLine(zeroPoint, gunTurretHeight); ;
        Handles.DrawLine(gunBarrelDistancePos, gunBarrelDistanceNeg);
        Handles.DrawLine(gunBarrelDistanceNeg, gunBarrelLengthNeg);
        Handles.DrawLine(gunBarrelDistancePos, gunBarrelLengthPos);


    }

    private void OnDrawGizmos()
    {
        ShootRaycast();
        Vector3 pos = transform.position;

        DrawPosition(pos);

        DrawBasisVectors();

        transformMatrix = new Matrix4x4(localRight, hit.normal.normalized, localForward, new Vector4(hit.point.x, hit.point.y, hit.point.z, 1));

        DrawWireFrame();

        DrawTurretBarrels();

    }


}
