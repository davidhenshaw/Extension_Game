using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform spawnLocation;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Spawn();
        }
    }

    void Spawn()
    {
        Instantiate(prefab, spawnLocation.position, Quaternion.identity);
    }
}
