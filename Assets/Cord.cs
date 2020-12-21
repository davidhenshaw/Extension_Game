using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    float distanceFromChainEnd = 0.6f;

    public int numLinks = 6;

    LineRenderer _line;
    Transform[] _links;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _links = GetComponentsInChildren<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateCord();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineRenderer();
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
            GameObject link = Instantiate(linkPrefab, transform);
            HingeJoint2D joint = link.GetComponent<HingeJoint2D>();
            joint.connectedBody = previousRB;

            if (i < numLinks - 1)
            {
                previousRB = link.GetComponent<Rigidbody2D>();
            }
            else
            {
                //ConnectRopeEnd(link.GetComponent<Rigidbody2D>());
            }

            _links[i+1] = link.transform;
        }
    }

}
