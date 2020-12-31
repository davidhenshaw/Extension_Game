using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour, IPowerSink
{
    [SerializeField] float _powerDrawRate;
    [SerializeField] Battery _battery;
    bool _isWorking;

    public void OnConnect(IPowerSource source)
    {
    }

    public void OnDisconnect(IPowerSource source)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _battery.OnConnect(this);
        _isWorking = true;
    }

    // Update is called once per frame
    void Update()
    {
        float usableCharge = _battery.RequestCharge(_powerDrawRate * Time.deltaTime);

        if (usableCharge <= 0 && _isWorking)
        {
            Debug.Log("I'm out of battery!", this);
            _isWorking = false;
        }
        else if(usableCharge > 0)
        {
            _isWorking = true;
        }

    }
}
