using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGrouping : MonoBehaviour
{
    //Saves and later restores the colliding object's previous parent transform 
    // so that hierarchy functionality is not broken
    Dictionary<GameObject, Transform> parentDict;

    private void Start()
    {
        parentDict = new Dictionary<GameObject, Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform objParent = collision.gameObject.transform.parent;

        if (objParent != null)
            parentDict.Add(collision.gameObject, objParent);

        collision.gameObject.transform.SetParent(this.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Transform origParent;
        parentDict.TryGetValue(collision.gameObject, out origParent);

        if (origParent == null)
        {
            collision.gameObject.transform.SetParent(null);
        }
        else
        {
            collision.gameObject.transform.SetParent(origParent);
        }

        parentDict.Remove(collision.gameObject);
    }

    private void OnDestroy()
    {
        Transform parent;

        foreach (GameObject obj in parentDict.Keys)
        {
            parentDict.TryGetValue(obj, out parent);

            if (parent != null)
                obj.transform.SetParent(parent);
        }
    }
}
