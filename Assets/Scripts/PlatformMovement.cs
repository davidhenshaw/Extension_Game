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
    private bool waitTimePassed = false;

    private void Awake()
    {
        if(waypointGroup != null)
        {
            _waypoints = waypointGroup.GetComponentsInChildren<Waypoint>();
        }
        //Manually set the first target
        _currTarget = _waypoints[0];
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

    IEnumerator Wait_co(float time)
    {
        yield return new WaitForSeconds(time);
        waitTimePassed = true;
    }

    void MoveTowardWaypoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _currTarget.transform.position, Time.deltaTime * _moveSpeed);

        if(HaveArrived())
            StartCoroutine(Wait_co(_currTarget.waitTime));
    }

    bool HaveArrived()
    {
        return transform.position == _currTarget.transform.position;
    }

    private void Start()
    {
        //Teleport to the first waypoint
        if(_waypoints[0] != null)
        {
            transform.position = _waypoints[0].transform.position;
        }

        MoveTowardWaypoint();
    }

    private void Update()
    {
        if(HaveArrived())
        {
            if(waitTimePassed)
            {
                _currTarget = GetNextWaypoint();
                waitTimePassed = false;
            }
        }
        else
        {
            MoveTowardWaypoint();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (waypointGroup == null)
            return;

        if (_waypoints != null)
        {
            for(int i = 0; i < _waypoints.Length - 1; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(_waypoints[i].transform.position, _waypoints[i + 1].transform.position);
            }
        }
        else
        {
            _waypoints = waypointGroup.GetComponentsInChildren<Waypoint>();
        }



    }

}
