using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GrappleAbility : Ability
{
    public Transform followTarget;
    [SerializeField] float maxForce = 50;
    public float _spacing = 1.5f;

    bool isActive = false;
    Rigidbody2D myRigidbody;
    TargetJoint2D myJoint;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
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
    }

    private void DestroyJoint()
    {
        if (myJoint == null)
            return;

        isActive = false;
        Destroy(myJoint);
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

            Vector2 flattenedPos = transform.position;


            if (Vector2.Distance(myJoint.target, flattenedPos) < _threshold)
                DestroyJoint();
        }



    }

}
