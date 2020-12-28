using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : Conductor
{
    public PowerOutlet connectedOutlet;
    public event Action<Rigidbody2D> connected;
    public event Action<Rigidbody2D> disconnected;

    public override void Disconnect()
    {
        base.Disconnect();

        if(connectedOutlet != null)
        {
            disconnected?.Invoke(connectedOutlet.GetComponent<Rigidbody2D>());
            connectedOutlet.Disconnect();
        }
    }

    public void Connect(PowerOutlet outlet)
    {
        var powerSource = outlet.GetComponent<IPowerSource>();

        if (powerSource != null)
        {
            base.Connect(powerSource);

            outlet.Connect(this);
            connected?.Invoke(outlet.GetComponent<Rigidbody2D>());
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
            Connect(outlet);
        }
    }

}
