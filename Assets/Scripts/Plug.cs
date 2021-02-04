using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : Conductor
{
    PowerOutlet connectedOutlet;
    public event Action<Rigidbody2D> pluggedIn;
    public event Action<Rigidbody2D> disconnected;

    IPowerSink _sink;
    IPowerSource _source;

    private void Start()
    {
        _sink = GetComponentInParent<IPowerSink>();
        _source = null;
    }

    public void DisconnectOutlet()
    {
        if(_sink != null && _source != null)
            base.DisconnectSinkFromSource(_sink, _source);

        if(connectedOutlet != null)
        {
            disconnected?.Invoke(connectedOutlet.GetComponent<Rigidbody2D>());
            connectedOutlet.DisconnectPlug();
            connectedOutlet = null;
        }
    }

    public void ConnectOutlet(PowerOutlet outlet)
    {
        var mySink = GetComponentInParent<IPowerSink>();
        var otherSource = outlet.GetComponent<IPowerSource>();

        var mySource = GetComponentInParent<IPowerSource>();
        var otherSink = outlet.GetComponent<IPowerSink>();

        // I want to draw power from other
        /* Me(Sink) -----> Other(source)*/
        if (mySink != null && otherSource != null)
        {
            base.ConnectSourceToSink(mySink, otherSource);
            _sink = mySink;
            _source = otherSource;

            outlet.ConnectPlug(this);
            connectedOutlet = outlet;

            pluggedIn?.Invoke(outlet.GetComponent<Rigidbody2D>());
            return;
        }

        // Other wants to draw power from me
        /* Me(Source) <----- Other(sink)*/
        if (mySource != null && otherSink != null)
        {
            base.ConnectSourceToSink(otherSink, mySource);
            _sink = otherSink;
            _source = mySource;

            outlet.ConnectPlug(this);
            connectedOutlet = outlet;

            pluggedIn?.Invoke(outlet.GetComponent<Rigidbody2D>());
            return;
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
