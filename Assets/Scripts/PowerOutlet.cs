using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HingeJoint2D))]
public class PowerOutlet : MonoBehaviour
{
    Rigidbody2D _myRigidbody;
    HingeJoint2D joint;

    [SerializeField] Transform connectionPoint;

    private void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        joint = GetComponent<HingeJoint2D>();

        if (connectionPoint == null)
            connectionPoint = transform;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectCordEnd(Rigidbody2D endRB)
    {
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = endRB;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = connectionPoint.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Plug plug = collision.GetComponentInParent<Plug>();

        if(plug != null)
        {
            ConnectCordEnd(plug.GetComponent<Rigidbody2D>());
        }
    }
}
