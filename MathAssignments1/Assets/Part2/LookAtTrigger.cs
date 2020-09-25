using UnityEditor;
using UnityEngine;

public class LookAtTrigger : MonoBehaviour
{
    [SerializeField] private Transform triggerObject;
    [Range(0f, 0.95f)]
    [SerializeField] private float triggerRange;

    private void OnDrawGizmos()
    {
        Vector3 transformPosition = transform.position;
        Vector3 objectPosition = triggerObject.position;
        float angle = Vector2.Dot(transform.right, (objectPosition - transformPosition).normalized);


        Handles.color = angle <= triggerRange ? Color.red : Color.green;
        

        Handles.DrawLine(transformPosition, objectPosition);
    }
}
