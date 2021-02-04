using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerListener : MonoBehaviour, IPowerSink
{
    [SerializeField] float powerDraw = 10f;

    public UnityEvent OnPowerReceived;
    public UnityEvent OnPowerLost;

    IPowerSource source;
    bool isPowered = false;

    public void OnConnect(IPowerSource src)
    {
        source = src;
    }

    public void OnDisconnect(IPowerSource src)
    {
        source = null;
        isPowered = false;
        OnPowerLost?.Invoke();
    }

    private void CheckForPower()
    {
        float requiredCharge = powerDraw * Time.deltaTime;
        float receivedCharge = source.RequestCharge(powerDraw * Time.deltaTime);

        if( receivedCharge >= requiredCharge )
        {
            if(!isPowered)
            {
                isPowered = true;
                OnPowerReceived?.Invoke();
            }
        }
        else
        {
            if(isPowered)
            {
                isPowered = false;
                OnPowerLost?.Invoke();
            }
        }
    }

    void Update()
    {
        if(source != null)
            CheckForPower();
    }
}
