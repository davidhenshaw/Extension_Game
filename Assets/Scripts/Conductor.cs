using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Conductor : MonoBehaviour
{
    protected IPowerSource _source;
    protected IPowerSink _sink;

    public virtual void ConnectSourceToSink(IPowerSource ps)
    {
        _source = ps;

        _source.OnConnect(_sink);
        _sink.OnConnect(_source);
    }

    public virtual void DisconnectOutlet()
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
    float RequestCharge(float requested);
    void OnConnect(IPowerSink sink);
    void OnDisconnect(IPowerSink sink);
}
