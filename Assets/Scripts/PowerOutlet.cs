using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PowerOutlet : MonoBehaviour
{
    Rigidbody2D _myRigidbody;
    HingeJoint2D joint;
    Plug connectedPlug;
    [SerializeField] Transform connectionPoint;

    private void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();

        if (connectionPoint == null)
            connectionPoint = transform;
    }

    public void Connect(Plug plug)
    {
        if (connectedPlug != null)
            return;
        else
        {
            connectedPlug = plug;
            plug.connectedOutlet = this;
        }


        Rigidbody2D endRB = plug.GetComponent<Rigidbody2D>();

        joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = endRB;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = connectionPoint.localPosition;
    }

    public void Disconnect()
    {
        Destroy(joint);
        connectedPlug = null;
    }

}
