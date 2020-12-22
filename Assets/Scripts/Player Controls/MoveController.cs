using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    public readonly float inputDeadZone = 0.05f;
    Rigidbody2D _myRigidbody;

    private void Awake()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 inputAxis, MovementInfo mInfo)
    {
        float dxSpeed = 0; // the change in xSpeed this frame
        Vector2 vel = _myRigidbody.velocity;
        //Horizontal Movement
        if (Mathf.Abs(inputAxis.x) > inputDeadZone)
        {
            dxSpeed = mInfo.acceleration * Time.deltaTime * Mathf.Sign(inputAxis.x);
        }
        else
        {
            //apply speed in the opposite direction of the current velocity
            dxSpeed = mInfo.deceleration * Time.deltaTime * Mathf.Sign(vel.x);
        }

        // add the deceleration to the dxSpeed if trying to turn around
        if( Mathf.Sign(inputAxis.x) * Mathf.Sign(vel.x) == -1)
        {
            dxSpeed += mInfo.deceleration * Time.deltaTime * Mathf.Sign(vel.x);
        }

        vel.x = Mathf.Clamp( vel.x + dxSpeed, -1*mInfo.maxVelocity, mInfo.maxVelocity );
        
        //Vertical Movement
        //vel.y = _myRigidbody.velocity.y;

        // Do movement
        _myRigidbody.velocity = vel;
    }

    public void ApplyForce(Vector2 jump)
    {
        _myRigidbody.AddForce(jump, ForceMode2D.Impulse);
    }

    public void KillHorizontalVelocity()
    {
        //_myRigidbody.velocity = new Vector2(0, _myRigidbody.velocity.y);
    }

    public void KillVerticalVelocity()
    {
        _myRigidbody.velocity = new Vector2(_myRigidbody.velocity.x, 0);
    }

    public Vector2 GetVelocity()
    {
        return _myRigidbody.velocity;
        //return currVelocity;
    }

    public void SetVelocity(Vector2 value)
    {
        _myRigidbody.velocity = value;
    }
}

public struct MovementInfo
{
    public float acceleration;
    public float deceleration;
    public float maxVelocity;
}
