using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RaycastBounce : MonoBehaviour
{
    [SerializeField] private int numberOfBounces = 1;

    private List<Vector3> hitPoints = new List<Vector3>();
    private RaycastHit hit;


    private void BounceRay(int counter, Vector3 origin, Vector3 direction)
    {
        Vector3 newDirection = Vector3.zero;
        if (counter < 0)
        {
            return;
        }
        if(Physics.Raycast(origin, direction, out hit, Mathf.Infinity))
        {
            Vector3 normal = hit.normal;
            hitPoints.Add(hit.point);
            newDirection = direction - 2 * Vector3.Dot(direction, normal) * normal;


        }
        else
        {
            return;
        }

        BounceRay(counter - 1, hit.point, newDirection);
    }


    private void OnDrawGizmos()
    {
        BounceRay(numberOfBounces, transform.position, transform.forward);
        Handles.color = Color.red;
        Vector3 previousVector = transform.position;
        if(hitPoints.Count != 0)
        {
            
            foreach (Vector3 vec in hitPoints)
            {
                Handles.DrawLine(previousVector, vec);
                previousVector = vec;
            }
        }

        hitPoints.Clear();
    }
}
