using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    public PowerOutlet connectedOutlet;
    public event Action<Rigidbody2D> connected;
    public event Action<Rigidbody2D> disconnected;

    public void Disconnect()
    {
        if(connectedOutlet != null)
        {
            disconnected?.Invoke(connectedOutlet.GetComponent<Rigidbody2D>());
            connectedOutlet.Disconnect();
        }
    }

    public bool IsConnected()
    {
        return connectedOutlet != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerOutlet outlet = collision.GetComponentInParent<PowerOutlet>();
        if (outlet != null)
        {
            outlet.Connect(this);
            connected?.Invoke(outlet.GetComponent<Rigidbody2D>());
        }
    }

}
