using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Conductor : MonoBehaviour
{
    public virtual void ConnectSinkToSource(IPowerSink sink, IPowerSource source)
    {
        source.OnConnect(sink);
        sink.OnConnect(source);
    }

    public virtual void DisconnectSinkFromSource(IPowerSink sink, IPowerSource source)
    {
        source.OnDisconnect(sink);
        sink.OnDisconnect(source);
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
