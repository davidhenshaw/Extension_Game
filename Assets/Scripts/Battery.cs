using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IPowerSink
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
            _currCharge += _source.GetCharge(_chargeRate * Time.deltaTime);
            _currCharge = Mathf.Clamp(_currCharge, 0, _capacity);

            Debug.Log("Battery charge: " + _currCharge);
        }
    }

}
