using UnityEngine;


public class SpaceTransformation : MonoBehaviour
{
    [SerializeField] private Transform localSpaceReferencePoint;
    [SerializeField] private Transform transformationPoint;
    [SerializeField] private Vector2 worldPosition;

    public Vector2 TransformLocalToWorld(Transform local, Vector2 localPoint)
    {

        Vector2 worldPoint = local.position + (local.right * localPoint.x + local.up * localPoint.y);

        return worldPoint;
    }

    public Vector2 TransformWorldToLocal(Transform local, Vector2 worldPoint)
    {
        Vector2 localOffset = worldPoint - (Vector2)local.position;

        float localX = Vector2.Dot(localOffset, local.right);
        float localY = Vector2.Dot(localOffset, local.up);


        return new Vector2(localX, localY);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(localSpaceReferencePoint.position, 0.1f);

        transformationPoint.localPosition = TransformWorldToLocal(localSpaceReferencePoint, worldPosition);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transformationPoint.position, 0.1f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

}
