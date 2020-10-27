using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFovEncapsulate : MonoBehaviour
{
    [SerializeField] private List<PointData> points;



    private void OnDrawGizmos()
    {
        Camera camera = GetComponent<Camera>();

        Vector2 forwardDirection = Vector2.right;

        float furthestAngle = float.MinValue;

        foreach(PointData point in points)
        {
            Vector3 pointLocalPos = camera.transform.InverseTransformPoint(point.transform.position);

            Vector2 pointCentered = new Vector2(pointLocalPos.z, pointLocalPos.y);

            Vector2 directionToPoint = pointCentered.normalized;

            float angleToPoint = Mathf.Acos(Vector2.Dot(forwardDirection, directionToPoint));
            float radiusAngle = Mathf.Asin(point.Radius / pointLocalPos.magnitude);
            float angularDeviation = angleToPoint + radiusAngle;

            if(angularDeviation > furthestAngle)
            {
                furthestAngle = angularDeviation;
            }
        }

        camera.fieldOfView = furthestAngle * 2 * Mathf.Rad2Deg;

        DrawRadius();
    }

    private void DrawRadius()
    {
        foreach (PointData point in points)
        {
            Gizmos.DrawWireSphere(point.transform.position, point.Radius);
        }
    }
}
