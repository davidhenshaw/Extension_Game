using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script makes objects move more when pulled on by a DistanceJoint2D
[RequireComponent(typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour
{
    DistanceJoint2D joint;
    Rigidbody2D myRigidbody;

    [Tooltip("How much this object will react to forces from a DistanceJoint2D")]
    public float sensitivity = 1;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void OnPlugConnect()
    {
        joint = GetComponent<DistanceJoint2D>();    
    }

    public void OnPlugDisconnect()
    {
        joint = null;  
    }

    bool IsPulling()
    {
        Transform otherObj = joint.attachedRigidbody.gameObject.transform;

        float dist = Vector2.Distance(otherObj.position, transform.position);

        return dist >= joint.distance;
    }

    private void FixedUpdate()
    {
        joint = GetComponent<DistanceJoint2D>();

        if (joint != null)
        {
            myRigidbody.AddForce(joint.reactionForce * sensitivity * -1);
        }
    }
}
