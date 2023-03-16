using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrawSphereScript : MonoBehaviour
{
    public float radius = 1f;
    public Color color = Color.red;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
