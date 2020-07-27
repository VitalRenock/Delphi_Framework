using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curves_Debug : MonoBehaviour
{
    [Header("Debug options")]
    [Min(1)]
    public int SphereCount = 100;
    public float SphereSize = 0.2f;

    public Color StartColor = Color.white;
    public Color EndColor = Color.red;

    [Header("Curve options")]
    public float varA = 0;
    public float varB = 0;
    public float varC = 0;
    public float varD = 0;
    public float varE = 0;

    private void OnDrawGizmos()
    {
        for (int z = 0; z < SphereCount; z++)
        {
            Gizmos.color = Color.Lerp(StartColor, EndColor, (float)z / SphereCount);

            //float resultA = z * varA;
            //float resultA = (z + varA) * varB;
            float resultA = 0f - (Mathf.Pow(z, 2f) * varA);
            //float resultA = z * Mathf.Cos((z / varA) + varB);
            //float resultA = Mathf.Exp(z / varB) * (-varA / 10000000f);
            //float resultA = Mathf.Cos(z) * varA;
            //float resultA = Mathf.Cos(z) * varA + varB;
            //float resultA = Mathf.Cos(z + varA) * varB;
            //float resultA = (Mathf.Cos(z + varA) * varB) + varC;
            //float resultA = Mathf.Sin(z) * varA + varB;
            //float resultA = Mathf.Tan(z) * varA + varB;
            //float resultA = -Mathf.Log(z + varA) * varB;
            //float resultA = Mathf.Log(z, varA);
            //float resultA = Mathf.Pow(z, varA);
            //float resultA = Mathf.Pow(varA, z);
            //float resultA = Mathf.Gamma(z, varA, varB);
            //float resultA = Mathf.PingPong(z, varA);
            //float resultA = Mathf.Floor(Mathf.Cos(z) * varA);
            //float resultA = Mathf.Floor((Mathf.Cos(z) * varA) * varB);
            //float resultA = Mathf.Lerp(varA, varB, z) * varC;
            //float resultA = Mathf.InverseLerp(varA, varB, z) * varC;

            Gizmos.DrawSphere(new Vector3(0f, resultA, z), SphereSize);
        }
    }
}
