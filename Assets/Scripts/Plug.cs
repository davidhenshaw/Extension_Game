using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : MonoBehaviour
{
    public PowerOutlet connectedOutlet;
    bool canConnect = true;

    public void Disconnect()
    {
        if(connectedOutlet != null)
        {
            connectedOutlet.Disconnect();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PowerOutlet outlet = collision.GetComponentInParent<PowerOutlet>();
        if (outlet != null)
        {
            outlet.Connect(this);
        }
    }

}
