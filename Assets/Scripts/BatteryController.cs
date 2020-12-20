using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    [SerializeField] float batteryTime = 30;
    [SerializeField] float batteryLevelStart = 0.5f;
    float startChargingTime; 
    float batteryLevel;
    float doneChargingTime;
    bool isLosingCharge = true;
    float chargingRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Slider>().value = batteryLevelStart;
        StartCoroutine(DecreasingBattery());
        //batteryLevel = batteryLevelStart;
        //doneChargingTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            isLosingCharge = false;
        }

        if (Input.GetMouseButton(0))
        {
            AddBatteryPower();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isLosingCharge = true;
            StartCoroutine(DecreasingBattery());
        }



        //if(Input.GetMouseButtonUp(0))
        //{
        //    doneChargingTime = Time.time;
        //}

        //batteryLevel = (doneChargingTime + Time.time) / batteryTime;

        //GetComponent<Slider>().value = batteryLevel;

        //if(GetComponent<Slider>().value >= 1)
        //{
        //    print("Game Over");
        //}

        //if(Input.GetMouseButtonDown(0))
        //{
        //    startChargingTime = Time.time;
        //}

        //if(Input.GetMouseButton(0))
        //{
        //    AddBatteryPower(startChargingTime);
        //}
    }

    void AddBatteryPower()
    {
            GetComponent<Slider>().value = GetComponent<Slider>().value - (chargingRate * Time.deltaTime);
    }

    IEnumerator DecreasingBattery()
    {
        while(isLosingCharge)
        {
            GetComponent<Slider>().value = GetComponent<Slider>().value + .03f;
            yield return new WaitForSeconds(1);
        }
    }
}
