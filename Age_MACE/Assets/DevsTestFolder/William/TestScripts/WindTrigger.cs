using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrigger : MonoBehaviour
{
    public GameObject wind;
    /*
    private void OnTriggerEnter(Collider other)
    {
        //var o = other.gameObject.GetComponent<LeafFalling>();
        if (o)
        {
            o.WindIsBlowing();
            o.SetWindForece(wind.GetComponent<Wind>().GetWindForce()* 0.1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //var o = other.gameObject.GetComponent<LeafFalling>();
        if (o)
        {
            o.WindIsBlowing();
            o.SetWindForece(wind.GetComponent<Wind>().GetWindForce()* 0.1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //var o = other.gameObject.GetComponent<LeafFalling>();
        if (o)
        {
            o.WindIsNotBlowing();
            o.SetWindForece(new Vector3(0f, 0f, 0f));
        }
    }
    */
}
