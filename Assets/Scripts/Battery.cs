using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IPowerSink, IPowerSource, ISliderUIModel
{
    [SerializeField] float _chargeRate;
    [SerializeField] float _capacity;
    float _currCharge;

    IPowerSource _source;

    public void OnConnect(IPowerSource source)
    {
        _source = source;
    }

    public void OnDisconnect(IPowerSource source)
    {
        _source = null;
    }

    public float GetSliderValue()
    {
        return _currCharge/_capacity;
    }

    public float RequestCharge(float requested)
    {
        float prevCharge = _currCharge;

        _currCharge -= Mathf.Abs(requested);
        _currCharge = Mathf.Clamp(_currCharge, 0, _capacity);

        return prevCharge - _currCharge;
    }

    public void OnConnect(IPowerSink sink)
    {
    }

    public void OnDisconnect(IPowerSink sink)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _currCharge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(_source != null)
        {
            _currCharge += _source.RequestCharge(_chargeRate * Time.deltaTime);
            _currCharge = Mathf.Clamp(_currCharge, 0, _capacity);
        }
    }


}
