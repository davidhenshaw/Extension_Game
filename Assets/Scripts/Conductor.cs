using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    IPowerSource _source;
    IPowerSink _sink;

    private void Start()
    {
        _sink = GetComponentInParent<IPowerSink>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _source = collision.GetComponentInChildren<IPowerSource>();

        if(_source != null)
        {
            _source.OnConnect(_sink);
            _sink.OnConnect(_source);
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
