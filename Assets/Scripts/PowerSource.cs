using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour, IPowerSource
{
    [SerializeField] float _chargeRate;

    public float RequestCharge(float requested)
    {
        return Mathf.Clamp(requested, 0, _chargeRate * Time.deltaTime);
    }

    public void OnConnect(IPowerSink sink)
    {
        Debug.Log(sink.ToString() + " connected");
    }

    public void OnDisconnect(IPowerSink sink)
    {
        Debug.Log(sink.ToString() + " disconnected");
    }
}
