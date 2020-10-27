using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        
    }
    [SerializeField] private float arcRadius = 0.5f;
    [SerializeField] private float[] itemRadii = new float[3]{0.05f, 0.05f, 0.05f};
    private void OnDrawGizmos()
    {
        using (new Handles.DrawingScope(transform.localToWorldMatrix))
        {
            Handles.DrawWireArc(default, Vector3.forward, Vector3.right, 45, arcRadius);
            Handles.DrawWireArc(default, Vector3.forward, Vector3.right, -45, arcRadius);
                
            int itemCount = itemRadii.Length;
            float[] anglesBetween = new float[itemCount - 1];

            for (int i = 0; i < anglesBetween.Length; i++)
            {
                float a = itemRadii[i];
                float b = itemRadii[i + 1];
                float abLength = a + b;

                float ang = Mathf.Acos(1f - (abLength * abLength) / (2 * arcRadius * arcRadius));
                anglesBetween[i] = ang;
            }
            
            float angRad = -anglesBetween.Sum()/2;
            for (int i = 0; i < itemCount; i++)
            {
                float radius = itemRadii[i];
                Vector3 itemCenter = AngleToDirection(angRad) * arcRadius;
                Handles.DrawWireDisc(itemCenter, Vector3.forward, radius);
                if(i < itemCount - 1)
                    angRad += anglesBetween[i];

            }

        }
    }
    
    private Vector2 AngleToDirection(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
}
