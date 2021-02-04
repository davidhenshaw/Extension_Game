using System.Collections;
using System.Collections.Generic;
using UOP1.StateMachine;
using UnityEngine;

public class respawner : MonoBehaviour
{
    PlayerController player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        player.DeathAnimFinished += Respawn;
    }

    private void OnDisable()
    {
        player.DeathAnimFinished -= Respawn;
    }

    public void Respawn()
    {
        player.transform.position = transform.position;
        player.Reset();
    }
}
