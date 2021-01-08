﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Ability _ability;
    Cord _cord;
    float _cordLength;
    Vector2 aimDir;
    DistanceJoint2D joint;

    // Start is called before the first frame update
    void Start()
    {
        aimDir = new Vector2(1, 1);
        //_cord.Plug.pluggedIn += CreateJoint;
        //_cord.Plug.disconnected += DestroyJoint;
    }

    // Update is called once per frame
    void Update()
    {

        ReadInput();
    }

    private void OnEnable()
    {
        //if (_cord.Plug == null)
        //    return;

        //_cord.Plug.pluggedIn += CreateJoint;
        //_cord.Plug.disconnected += DestroyJoint;
    }

    private void OnDisable()
    {
        //_cord.Plug.pluggedIn -= CreateJoint;
        //_cord.Plug.disconnected -= DestroyJoint;
    }

    void CreateJoint(Rigidbody2D rb)
    {
        if (joint != null)
            return;

        joint = gameObject.AddComponent<DistanceJoint2D>();
        joint.autoConfigureDistance = false;
        joint.connectedBody = rb;

        joint.autoConfigureDistance = false;
        joint.distance = _cordLength;
        joint.maxDistanceOnly = true;
    }

    void DestroyJoint(Rigidbody2D rb)
    {
        if (joint == null)
            return;

        joint = gameObject.GetComponent<DistanceJoint2D>();
        Destroy(joint);
    }

    void ReadInput()
    {
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    if (_cord.IsRetracted)
        //        _cord.Release();
        //    else
        //        _cord.Retract();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    _cord.EjectPlug(aimDir);
        //}

        if(Input.GetMouseButtonDown(0))
        {
            if(_ability != null)
                _ability.DoAbility();
        }

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDir = (mouse - (Vector2)transform.position).normalized;
        Debug.DrawLine(mouse, transform.position);
    }

}
