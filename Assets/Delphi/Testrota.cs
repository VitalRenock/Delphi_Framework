using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testrota : MonoBehaviour
{
    public float Angle = 0f;
    public Vector2 Old = Vector2.zero;
    [ShowInInspector][ReadOnly]
    Vector2 New = Vector2.zero;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Old, 0.1f);


        float newX = (Old.x * Mathf.Cos(Angle)) + (Old.y * Mathf.Sin(Angle));
        float newY = (Old.x * Mathf.Sin(Angle)) + (Old.y * Mathf.Cos(Angle));
        New = new Vector2(newX, newY);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(New, 0.1f);
    }
}
