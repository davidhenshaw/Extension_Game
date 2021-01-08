using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VerletRope : MonoBehaviour
{
    LineRenderer lineRenderer;
    List<RopeSegment> _segments = new List<RopeSegment>();


    [SerializeField] float _segmentSpacing = 0.25f;
    [SerializeField] float _thickness = 0.2f;
    [SerializeField] int _numSegments = 30;
    [SerializeField] Transform _followTransform;
    [SerializeField] Transform _endPoint;
    [Space]
    [Tooltip("How many times the rope simulation applies physics constraints. More repetition is more accurate but also more costly")]
    [SerializeField] [Range(1, 100)] int _precision = 20;
    [SerializeField] [Range(-10,0)] float _gravity; 

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>(); 
        if(_followTransform == null)
        {
            _followTransform = transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GenerateSegments(mousePos);
        }

        if(_numSegments == _segments.Count)
        {
            Simulate();
            DrawRope();
        }

    }

    void DrawRope()
    {
        lineRenderer.positionCount = _numSegments;
        lineRenderer.startWidth = _thickness;
        lineRenderer.endWidth = _thickness;

        Vector3[] linePositions = new Vector3[_numSegments];
        for(int i = 0; i < _numSegments; i++)
        {
            linePositions[i] = _segments[i].currPos;
        }

        lineRenderer.SetPositions(linePositions);
    }

    void GenerateSegments(Vector2 startPoint)
    {
        _segments.Clear();

        Vector2 spawnPos = startPoint;
        for(int i =0; i < _numSegments; i++)
        {
            _segments.Add(new RopeSegment(spawnPos));
            spawnPos.y -= _segmentSpacing;
        }
    }

    //Verlet integration moves each point in the line based on how far it moved in the previous frame (simulating inertia)
    // other forces are also added such as gravity 
    void Simulate()
    {
        //SIMULATION

        Vector2 gravity = new Vector2(0, _gravity);
        for(int i = 0; i < _segments.Count; i++)
        {
            RopeSegment seg = _segments[i];
            Vector2 d = seg.currPos - seg.prevPos;
            seg.prevPos = seg.currPos;

            seg.currPos += d;   //inertia
            seg.currPos += gravity * Time.deltaTime; //gravity

            //Changes to the struct "seg" are not saved in the List so the updated value needs to be
            // passed back in to the List
            _segments[i] = seg; 
        }

        /*CONSTRAINTS*/
        for(int i = 0; i < _precision; i++)
        {
            ApplyConstraints();
        }

    }

    void ApplyConstraints()
    {
        RopeSegment firstSeg = _segments[0];
        firstSeg.currPos = _followTransform.position;
        _segments[0] = firstSeg;

        if (_endPoint != null)
        {
            RopeSegment lastSeg = _segments[_numSegments - 1];
            lastSeg.currPos = _endPoint.position;
            _segments[_numSegments - 1] = lastSeg;
        }

        for(int i = 0; i < _segments.Count - 1; i++)    //constraints run for every space between segments, so it runs for numSegments-1
        {
            RopeSegment currSeg = _segments[i];
            RopeSegment nextSeg = _segments[i + 1];

            float dist = Vector2.Distance(currSeg.currPos, nextSeg.currPos);
            float error = dist - _segmentSpacing;

            Vector2 correction = (currSeg.currPos - nextSeg.currPos).normalized * error;

            if (i != 0)
            {
                currSeg.currPos -= correction * 0.5f;
                _segments[i] = currSeg;
                nextSeg.currPos += correction * 0.5f;
                _segments[i + 1] = nextSeg;
            }
            else
            {
                nextSeg.currPos += correction;
                _segments[i + 1] = nextSeg;
            }
        }
    }

    public struct RopeSegment
    {
        public Vector2 currPos;
        public Vector2 prevPos;

        public RopeSegment(Vector2 pos)
        {
            currPos = pos;
            prevPos = pos;
        }
    }

}

