using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawner : MonoBehaviour
{
    [SerializeField] GameObject player;

    public void Respawn()
    {
        player.transform.position = transform.position;
    }
}
