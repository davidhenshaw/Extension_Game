using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Ability _ability1;
    [SerializeField] Ability _ability2;
    Cord _cord;
    float _cordLength;
    Vector2 aimDir;
    DistanceJoint2D joint;

    // Start is called before the first frame update
    void Start()
    {
        aimDir = new Vector2(1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        ReadInput();
    }

    void ReadInput()
    {
        if(Input.GetMouseButtonDown(0))//Left click
        {
            if(_ability1 != null)
                _ability1.DoAbility();
        }

        if(Input.GetMouseButtonDown(1)) //Right click
        {
            if (_ability2 != null)
                _ability2.DoAbility();
        }

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDir = (mouse - (Vector2)transform.position).normalized;
        Debug.DrawLine(mouse, transform.position);
    }

}
