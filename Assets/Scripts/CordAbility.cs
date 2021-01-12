using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordAbility : Ability
{
    Vector3 endTarget;
    [SerializeField] float _maxRange;
    float _currLength;
    [SerializeField] float _ejectionSpeed;

    [SerializeField] Plug plug;
    [SerializeField] VerletRope rope;

    bool _isEjected = false;
    bool _isRetracting = false;
    DistanceJoint2D joint;

    [Tooltip("Sets the cord's max length to the current distance between the start and end point")]
    public bool dynamicShrinking = false;

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
        _currLength = _maxRange;

        rope.startPoint = this.transform;
        rope.endPoint = plug.transform;
    }

    public float GetMaxRange()
    {
        return _maxRange;
    }

    public void SetCordLength(float value)
    {
        _currLength = Mathf.Clamp(value, 0, _maxRange);

        //Resize the joint's max distance
        joint.distance = _currLength;
        //Resize the VerletRope
    }

    public void ResetCordLength()
    {
        _currLength = _maxRange;
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
            if (!_isRetracting)
            {
                Eject();
                _isEjected = true;
            }
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
        plug.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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

        if (dynamicShrinking)
        {
            //Get distance between start and end point of rope
            float currDist = (rope.GetStartPoint() - rope.GetEndPoint()).magnitude;

            //Replace joint max distance if current length is shorter than the max
            joint.distance = Mathf.Min(joint.distance, currDist);

            //Resize the verlet rope
        }
    }

    void CancelMovePlug()
    {
        StopAllCoroutines();
    }

    void CreateJoint(Rigidbody2D otherRb)
    {
        if (joint != null)  //If there's already a joint, don't make another one
            return;

        CancelMovePlug();

        joint = otherRb.gameObject.AddComponent<DistanceJoint2D>();
        joint.autoConfigureDistance = false;
        joint.connectedBody = this.GetComponent<Rigidbody2D>();
        joint.distance = _maxRange;
        joint.maxDistanceOnly = true;
    }

    void DestroyJoint(Rigidbody2D rb)
    {
        if (joint == null)  //If there's no joint, don't do anything
            return;

        Destroy(joint);
    }
}
