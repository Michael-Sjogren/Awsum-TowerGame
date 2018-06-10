
using UnityEditor;
using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    [MenuItem("Custom/Snap To Ground %g")]
    public static void Ground()
    {
        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            var transform = Selection.transforms[i];
            var hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 10f);
            for (int j = 0; j < hits.Length; j++)
            {
                var hit = hits[j];

                if(hit.collider.gameObject == transform.gameObject)
                {
                    continue;
                }

                transform.position = hit.point;
                break;
            }
        }
    }
}
