using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Cord : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D hook;
    [SerializeField]
    private GameObject linkPrefab;

    [SerializeField]
    private GameObject plugPrefab;

    [Space]
    [SerializeField]
    float chainSpacing = 0.6f;

    [Space]
    [SerializeField] float ejectionForce = 10;
    public int numLinks = 6;
    [Space]
    [SerializeField] float resetLinkTime = 0.3f;

    private bool _isRetracted = false;
    public bool IsRetracted { get => _isRetracted; }
    Plug _plug;
    public Plug Plug { get => _plug; }

    LineRenderer _line;
    Transform[] _links;


    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _links = GetComponentsInChildren<Transform>();
        GenerateCord();
    }

    private void OnEnable()
    {
        _plug = GetComponentInChildren<Plug>();        
    }

    private void OnDisable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

        //GenerateCord();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineRenderer();
    }

    [ContextMenu("Retract")]
    public void Retract()
    {
        Rigidbody2D[] rigidBodies = GetComponentsInChildren<Rigidbody2D>();
        Rigidbody2D myRigidBody = GetComponent<Rigidbody2D>();

        DisconnectPlug();

        foreach(Rigidbody2D rb in rigidBodies)
        {
            if (rb == myRigidBody) //Ignore the parent's rigidbody
                continue;

            rb.isKinematic = true;
            rb.simulated = false;
            rb.transform.localPosition = Vector3.zero;
        }

        _isRetracted = true;
    }

    [ContextMenu("Release")]
    public void Release()
    {
        Rigidbody2D[] rigidBodies = GetComponentsInChildren<Rigidbody2D>();
        Rigidbody2D myRigidBody = GetComponent<Rigidbody2D>();

        foreach (Rigidbody2D rb in rigidBodies)
        {
            if (rb == myRigidBody) //Ignore the parent's rigidbody
                continue;

            rb.isKinematic = false;
            rb.simulated = true;
        }

        _isRetracted = false;
    }

    [ContextMenu("Eject Plug")]
    public void EjectPlug(Vector2 dir)
    {
        //Vector2 dir = Vector2.right;
        if (_isRetracted)
            Release();

        Rigidbody2D plugRb = GetComponentInChildren<Plug>().GetComponent<Rigidbody2D>();

        LockLinkRotations(dir);


        StartCoroutine(ReleaseLinks_co(resetLinkTime));
        StartCoroutine(AddForceToPlug_co(plugRb, dir, ejectionForce, resetLinkTime));
        //plugRB.AddForce(dir * ejectionForce, ForceMode2D.Impulse);
    }

    public bool IsPlugged()
    {
        return GetComponentInChildren<Plug>().IsConnected();
    }

    void LockLinkRotations(Vector2 dir)
    {
        float angle = Vector2.SignedAngle(Vector2.down, dir);
        Rigidbody2D[] rigidBodies = GetComponentsInChildren<Rigidbody2D>();
        Rigidbody2D myRigidBody = GetComponent<Rigidbody2D>();

        foreach (Rigidbody2D rb in rigidBodies)
        {
            if (rb == myRigidBody) //Ignore the parent's rigidbody
                continue;

            rb.transform.rotation = Quaternion.Euler(0,0,angle);
            rb.freezeRotation = true;
        }
    }

    void ReleaseLinkRotations()
    {
        Rigidbody2D[] rigidBodies = GetComponentsInChildren<Rigidbody2D>();
        Rigidbody2D myRigidBody = GetComponent<Rigidbody2D>();

        foreach (Rigidbody2D rb in rigidBodies)
        {
            if (rb == myRigidBody) //Ignore the parent's rigidbody
                continue;

            rb.freezeRotation = false;
        }
    }

    IEnumerator AddForceToPlug_co(Rigidbody2D rb, Vector2 dir, float force, float time)
    {
        yield return new WaitForSeconds(time);

        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }

    IEnumerator ReleaseLinks_co(float time)
    {
        yield return new WaitForSeconds(time);
        ReleaseLinkRotations();
    }

    private void DisconnectPlug()
    {
        Plug p = GetComponentInChildren<Plug>();
        p.Disconnect();
    }

    void UpdateLineRenderer()
    {
        //Check number of links equals number of line segments
        if (_line.positionCount != _links.Length)
        {
            _line.positionCount = _links.Length;
        }

        _line.SetPositions(GetLinkPositions());
    }

    Vector3[] GetLinkPositions()
    {
        //we are skipping the first element in the _links array
        // because it is the parent rope object and not a link

        Vector3[] ret = new Vector3[_links.Length];

        for(int i = 0; i < _links.Length; i++)
        {
            ret[i] = _links[i].position;
        }

        return ret;
    }

    void GenerateCord()
    {
        Rigidbody2D previousRB = hook;
        _links = new Transform[numLinks + 1];
        _links[0] = hook.transform;

        for (int i = 0; i < numLinks; i++)
        {
            GameObject link = null;
            //HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            //joint.connectedBody = previousRB;
            if(i < numLinks - 1)
            {
                link = Instantiate(linkPrefab, transform);
            }
            else
            {
                link = Instantiate(plugPrefab, transform);
            }

            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;
            joint.connectedAnchor = new Vector2(0f, -chainSpacing);
            previousRB = link.GetComponent<Rigidbody2D>();

            _links[i+1] = link.transform;
        }
    }



}
