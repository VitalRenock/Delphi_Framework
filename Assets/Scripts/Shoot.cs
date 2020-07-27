using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [AssetsOnly]
    public GameObject BulletPrefab;
    [Range(0,2)]
    public int MouseButton = 0;
    public Transform SpawnPoint;

    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseButton))
            Instantiate(BulletPrefab, SpawnPoint.position, transform.rotation);
    }
}
