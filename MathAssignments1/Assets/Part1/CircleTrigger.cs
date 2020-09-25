using UnityEditor;
using UnityEngine;

public class CircleTrigger : MonoBehaviour
{
    [SerializeField] private Transform triggerObject;
    [Range(0.5f, 5.0f)]
    [SerializeField] private float radius;

    private void OnDrawGizmos()
    {
        Vector3 transformPosition = transform.position;
        float distance = Vector2.Distance(transformPosition, triggerObject.position);

        if(distance > radius)
        {
            Handles.color = Color.red;
        }
        else
        {
            Handles.color = Color.green;
        }

        Handles.DrawWireDisc(transformPosition, transform.forward, radius);
    }

}
