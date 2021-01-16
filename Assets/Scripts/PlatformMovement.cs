using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public GameObject waypointGroup;
    [SerializeField] float _moveSpeed = 2f;

    public bool loop;
    private bool loopingFwd = true;

    Waypoint[] _waypoints;
    int _index = 0;
    Waypoint _currTarget;

    private void Awake()
    {
        if(waypointGroup != null)
        {
            _waypoints = waypointGroup.GetComponentsInChildren<Waypoint>();
        }
    }

    Waypoint GetNextWaypoint()
    {
        int increment = loopingFwd ? 1 : -1;
        int next = _index + increment;
        bool nextInBounds = next >= 0 && next < _waypoints.Length;

        if (nextInBounds)
        {
            _index = next;
            return _waypoints[_index];
        }
        else
        {
            if (loop)
            {
                loopingFwd = !loopingFwd;

                return GetNextWaypoint();
            }
            else
                return null;

        }
    }

    void MoveTowardWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currTarget.transform.position, Time.deltaTime * _moveSpeed);

        if(transform.position == _currTarget.transform.position)
        {
            _currTarget = null;
        }
    }

    private void Start()
    {
        if(_waypoints[0] != null)
        {
            transform.position = _waypoints[0].transform.position;
        }
    }

    private void Update()
    {
        //if no waypoint, get the next waypoint
        if(_currTarget == null)
        {
            _currTarget = GetNextWaypoint();
        }
        else
        {
            MoveTowardWaypoint();
        }
    }

}
