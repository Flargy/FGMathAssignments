using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointData : MonoBehaviour
{
    public float Radius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
