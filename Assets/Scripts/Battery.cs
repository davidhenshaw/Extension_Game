using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IPowerSink, IPowerSource, ISliderUIModel
{
    [SerializeField] float _chargeRate;
    [SerializeField] float _capacity;
    [SerializeField] float _startingCharge;
    float _currCharge;

    IPowerSource _source;


    // Start is called before the first frame update
    void Start()
    {
        _currCharge = _startingCharge;
    }


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
        requested = Mathf.Abs(requested);

        if(_currCharge > requested)
        {
            _currCharge -= requested;
            return requested;
        }
        else
        {
            var whatsLeft = _currCharge;
            _currCharge = 0;
            return whatsLeft;
        }
    }

    public void OnConnect(IPowerSink sink)
    {
    }

    public void OnDisconnect(IPowerSink sink)
    {
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
