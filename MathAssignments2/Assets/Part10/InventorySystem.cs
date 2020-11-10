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
    [SerializeField] private float inventoryRadius = 0.5f;
    [SerializeField] private float[] itemRadius = new float[3]{0.05f, 0.05f, 0.05f};
    private void OnDrawGizmos()
    {
        using (new Handles.DrawingScope(transform.localToWorldMatrix))
        {
            Handles.DrawWireArc(default, Vector3.forward, Vector3.right, 45, inventoryRadius);
            Handles.DrawWireArc(default, Vector3.forward, Vector3.right, -45, inventoryRadius);
                
            int itemCount = itemRadius.Length;
            float[] anglesBetween = new float[itemCount - 1];

            for (int i = 0; i < anglesBetween.Length; i++)
            {
                float a = itemRadius[i];
                float b = itemRadius[i + 1];
                float abLength = a + b;

                float angle = Mathf.Acos(1f - (abLength * abLength) / (2 * inventoryRadius * inventoryRadius));
                anglesBetween[i] = angle;
            }
            
            float angRad = -anglesBetween.Sum()/2;
            for (int i = 0; i < itemCount; i++)
            {
                float radius = itemRadius[i];
                Vector3 itemCenter = AngleToDirection(angRad) * this.inventoryRadius;
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
