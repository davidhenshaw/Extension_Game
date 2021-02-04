using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternalConductor : Conductor
{
    [SerializeField] MonoBehaviour powerSource;
    [SerializeField] MonoBehaviour powerSink;

    private void Awake()
    {
        IPowerSource source = powerSource as IPowerSource;
        IPowerSink sink = powerSink as IPowerSink;

        if (source != null && sink != null)
            base.ConnectSinkToSource(sink, source);
        else
            Debug.LogWarning($"Could not connect {powerSource.name} as a source to {powerSink.name} as a sink!");
    }

}
