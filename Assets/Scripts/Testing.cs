using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Respawn()
    {
        var res = FindObjectOfType<respawner>();
        res.Respawn();
        GetComponent<Animator>().SetBool("isDead", false);
    }
}
