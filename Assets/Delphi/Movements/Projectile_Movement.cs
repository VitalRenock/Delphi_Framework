using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Movement : MonoBehaviour
{
    [Header("Movement")]
    public float Speed = 100f;
    public Space Space = Space.World;

    public float FallFactor = 0.005f;

    float distance = 0f;
    Vector3 startPosition;

    private void Start() => startPosition = transform.position;
    private void Update()
    {
        //transform.Translate(new Vector3(0f, 0f - (Mathf.Pow(distance, 2f) * FallFactor), Speed) * Time.deltaTime, Space);
        //distance = Vector3.Distance(transform.position, startPosition);

        transform.Translate(new Vector3(0f, -FallFactor, Speed * Time.deltaTime));
    }
}
