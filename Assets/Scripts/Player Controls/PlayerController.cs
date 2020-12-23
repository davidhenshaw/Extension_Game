using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Cord cord;
    Vector2 aimDir;

    // Start is called before the first frame update
    void Start()
    {
        aimDir = new Vector2(1, 1);
        cord.Plug.connected += CreateJoint;
        cord.Plug.disconnected += DestroyJoint;
    }

    // Update is called once per frame
    void Update()
    {

        ReadInput();
    }

    private void OnEnable()
    {
        if (cord.Plug == null)
            return;

        cord.Plug.connected += CreateJoint;
        cord.Plug.disconnected += DestroyJoint;
    }

    private void OnDisable()
    {
        cord.Plug.connected -= CreateJoint;
        cord.Plug.disconnected -= DestroyJoint;
    }

    void CreateJoint(Rigidbody2D rb)
    {
        DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D>();
        joint.autoConfigureDistance = false;
        joint.connectedBody = rb;

        joint.autoConfigureDistance = false;
        joint.distance = 6.8f;
        joint.maxDistanceOnly = true;
    }

    void DestroyJoint(Rigidbody2D rb)
    {
        DistanceJoint2D joint = gameObject.GetComponent<DistanceJoint2D>();
        Destroy(joint);
    }

    void ReadInput()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (cord.IsRetracted)
                cord.Release();
            else
                cord.Retract();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            cord.EjectPlug(aimDir);
        }

        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDir = (mouse - (Vector2)transform.position).normalized;
        Debug.DrawLine(mouse, transform.position);
    }

}
