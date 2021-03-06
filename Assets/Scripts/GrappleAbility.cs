﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Cord Ability is also required to use this ability
[RequireComponent(typeof(Rigidbody2D))]
public class GrappleAbility : Ability
{
    public Transform followTarget;
    [SerializeField] float maxForce = 50;
    public float _spacing = 1.5f;

    bool isActive = false;
    Rigidbody2D myRigidbody;
    TargetJoint2D myJoint;
    CordAbility cordAbility;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        cordAbility = GetComponent<CordAbility>();
    }

    public override void DoAbility()
    {
        isActive = !isActive;

        if(isActive)
        {
            CreateJoint();
        }
        else
        {
            DestroyJoint();
        }
    }

    void CreateJoint()
    {
        if (myJoint != null)
            return;

        myJoint = gameObject.AddComponent<TargetJoint2D>();
        myJoint.target = followTarget.position;
        myJoint.maxForce = maxForce;

        cordAbility.dynamicShrinking = true;
    }

    private void DestroyJoint()
    {
        if (myJoint == null)
            return;

        isActive = false;
        Destroy(myJoint);

        cordAbility.dynamicShrinking = false;
    }

    private Vector3 GetFinalPosition()
    {
        Vector3 targetToMeDir = (transform.position - followTarget.position).normalized;

        return followTarget.position + (targetToMeDir * _spacing);
    }

    private void Update()
    {
        float _threshold = 0.1f;
        if(isActive && myJoint != null)
        {
            myJoint.target = GetFinalPosition();

            //When you get close to the target point, remove the TargetJoint
            if (Vector2.Distance(myJoint.target, transform.position) < _threshold)
                DestroyJoint();
        }
    }

}
