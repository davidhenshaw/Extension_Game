using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensorHandler : MonoBehaviour
{
    [SerializeField] Collider2D groundCollider;


    public Collider2D GetGroundCollider()
    {
        return groundCollider;
    }
}
