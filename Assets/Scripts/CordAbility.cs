using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordAbility : Ability
{
    Vector3 endTarget;
    [SerializeField] float _maxRange;
    [SerializeField] float _ejectionSpeed;

    [SerializeField] Plug plug;
    [SerializeField] VerletRope rope;

    bool _isEjected = false;
    bool _isRetracting = false;
    DistanceJoint2D myJoint;

    private void OnEnable()
    {
        plug.pluggedIn += CreateJoint;
        plug.disconnected += DestroyJoint;
    }

    private void OnDisable()
    {
        plug.pluggedIn -= CreateJoint;
        plug.disconnected -= DestroyJoint;
    }

    private void Awake()
    {
        rope.startPoint = this.transform;
        rope.endPoint = plug.transform;
    }

    public override void DoAbility()
    {
        if(_isEjected)
        {
            Retract();

            _isEjected = false;
        }
        else
        {
            Eject();

            _isEjected = true;
        }
    }

    void Eject()
    {
        //Set the endTarget variable based on the mouse position and ability range
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 dir = (mousePos - transform.position).normalized;
        float dist = Vector3.Distance(mousePos , transform.position);

        if(Mathf.Abs(dist) > _maxRange)
        {
            endTarget = transform.position + (dir * _maxRange);
        }
        else
        {
            endTarget = transform.position + (dir * dist);
        }

        //Make the plug object dynamic so it can be "jointed" with other objects during its path
        plug.GetComponent<Rigidbody2D>().isKinematic = false;
        plug.GetComponent<Collider2D>().enabled = true;

        //Start moving the plug
        StartCoroutine(MovePlug_co(_ejectionSpeed));
    }

    // Moves the plug in a straight line towards the endTarget position
    IEnumerator MovePlug_co(float speed)
    {
        while (plug.transform.position != endTarget)
        {
            plug.transform.position = Vector3.MoveTowards(plug.transform.position, endTarget, speed * Time.deltaTime);
            yield return null;
        }
        _isRetracting = false;
    }

    // Moves the plug in a straight line towards the local transform (0,0,0) position, and makes it kinematic
    // [TO BE IMPLEMENTED] Also tells the rope to reduce its number of nodes to remove rope slack
    void Retract()
    {
        _isRetracting = true;
        endTarget = transform.localToWorldMatrix.MultiplyPoint(Vector3.zero);

        //Make the plug object kinematic so it will stay where we put it;
        plug.GetComponent<Rigidbody2D>().isKinematic = true;
        plug.GetComponent<Collider2D>().enabled = false;

        //Disconnect the plug from any outlet it was connected to
        plug.DisconnectOutlet();

        StartCoroutine(MovePlug_co(_ejectionSpeed));
    }

    private void Update()
    {
        //We need to run this in update since if the player is moving, the destination will not be the same as the player location
        if(_isRetracting)
        {
            endTarget = transform.localToWorldMatrix.MultiplyPoint(Vector3.zero);
        }
    }

    void CreateJoint(Rigidbody2D rb)
    {
        if (myJoint != null)
            return;

        myJoint = gameObject.AddComponent<DistanceJoint2D>();
        myJoint.autoConfigureDistance = false;
        myJoint.connectedBody = rb;

        myJoint.autoConfigureDistance = false;
        myJoint.distance = _maxRange;
        myJoint.maxDistanceOnly = true;
    }

    void DestroyJoint(Rigidbody2D rb)
    {
        if (myJoint == null)
            return;

        myJoint = gameObject.GetComponent<DistanceJoint2D>();
        Destroy(myJoint);
    }
}
