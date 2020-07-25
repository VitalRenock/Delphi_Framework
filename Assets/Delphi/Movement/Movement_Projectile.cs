using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float Speed = 100f;
    public Vector3 Direction = Vector3.forward;
    public Space Space = Space.World;

    [Header("Gravity")]
    public bool GravityEnable = true;
    public float GravityForce = 20f;
    public Vector3 GravityDirection = Vector3.down;

    [Header("Trajectoire")][Min(1)]
    public int SphereCount = 100;
    public float SphereSize = 0.02f;
    public Color StartColor = Color.black;
    public Color EndColor = Color.white;
    public float varA = 0;
    public float varB = 0;
    public float varC = 0;
    public float varD = 0;
    public float varE = 0;


    private void Update()
    {
        //Vector3 direction = Direction.normalized * Speed;

        //if (GravityEnable)
        //    direction += GravityDirection.normalized * GravityForce;

        //transform.Translate(direction * Time.deltaTime, Space);
    }

    private void OnDrawGizmos()
    {
        //for (int i = 0; i < SphereCount; i++)
        //{
        //    float t = (float)i / SphereCount;
        //    Gizmos.color = Color.Lerp(Color.black, Color.white, t);

        //    float resultA = Mathf.Cos(i) * varA + varB;
        //    float resultA = Mathf.Sin(i) * varA + varB;
        //    float resultA = Mathf.Tan(i) * varA + varB;
        //    float resultA = Mathf.Pow(i, varA);
        //    float resultA = Mathf.Pow(varA, i);
        //    float resultA = Mathf.Gamma(i, varA, varB);
        //    float resultA = Mathf.PingPong(i, varA);
        //    float resultA = Mathf.Floor(Mathf.Cos(i) * varA);
        //    float resultA = Mathf.Floor((Mathf.Cos(i) * varA) * varB);
        //    float resultA = Mathf.Lerp(varA, varB, i) * varC;
        //    float resultA = Mathf.InverseLerp(varA, varB, i) * varC;

        //    Gizmos.DrawSphere(new Vector3(i, resultA, 0), SphereSize * 1.1f);
        //}

        for (int x = 0, i = 0; x < SphereCount; x++)
            for (int y = 0; y < SphereCount; y++)
                for (int z = 0; z < SphereCount; z++, i++)
                {
                    float t = (float)i / Mathf.Pow(SphereCount, 3);
                    Gizmos.color = Color.Lerp(StartColor, EndColor, t);

                    //float resultA = Mathf.Cos(x + z) * varA;
                    //float resultA = (Mathf.Cos(x + z + varA) * varB) + varC;
                    float resultA = Mathf.PerlinNoise((x * varB) + varD, (z * varC) + varE) * varA;


                    Gizmos.DrawSphere(new Vector3(x, resultA, z), SphereSize);
                }
    }
}
