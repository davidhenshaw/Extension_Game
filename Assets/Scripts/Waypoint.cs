using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Tooltip("Time that a platform will spend at this Waypoint before going to the next")]
    public float waitTime = 0;
    float _elapsed = 0;
    bool running = false;
    bool isDone = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(running)
        {
            _elapsed += Time.deltaTime;
            if(_elapsed >= waitTime)
            {
                isDone = true;
            }
        }
    }

    public void OnArrival()
    {
        StartTimer(waitTime);
    }

    private void StartTimer(float waitTime)
    {
        _elapsed = 0;
        running = true;
        isDone = false;
    }

    public bool ConditionMet()
    {
        return isDone;
    }
}
