using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : Conductor
{
    public PowerOutlet connectedOutlet;
    public event Action<Rigidbody2D> pluggedIn;
    public event Action<Rigidbody2D> disconnected;

    private void Start()
    {
        _sink = GetComponentInParent<IPowerSink>();
        _source = null;
    }

    public override void DisconnectOutlet()
    {
        base.DisconnectOutlet();

        if(connectedOutlet != null)
        {
            disconnected?.Invoke(connectedOutlet.GetComponent<Rigidbody2D>());
            connectedOutlet.DisconnectPlug();
            connectedOutlet = null;
        }
    }

    public void ConnectOutlet(PowerOutlet outlet)
    {
        var powerSource = outlet.GetComponent<IPowerSource>();

        if (powerSource != null)
        {
            base.ConnectSourceToSink(powerSource);

            outlet.ConnectPlug(this);
            connectedOutlet = outlet;

            pluggedIn?.Invoke(outlet.GetComponent<Rigidbody2D>());
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
            ConnectOutlet(outlet);
        }
    }

}
