using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Conductor : MonoBehaviour
{
    IPowerSource _source;
    IPowerSink _sink;

    private void Start()
    {
        _sink = GetComponentInParent<IPowerSink>();
    }

    public virtual void Connect(IPowerSource ps)
    {
        _source = ps;

        _source.OnConnect(_sink);
        _sink.OnConnect(_source);
    }

    public virtual void Disconnect()
    {
        if (_source != null)
        {
            _source.OnDisconnect(_sink);
            _sink.OnDisconnect(_source);
        }
    }
}

public interface IPowerSink
{
    void OnConnect(IPowerSource source);
    void OnDisconnect(IPowerSource source);
}

public interface IPowerSource
{
    float GetCharge(float requested);
    void OnConnect(IPowerSink sink);
    void OnDisconnect(IPowerSink sink);
}
