using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrigger : MonoBehaviour
{
    public GameObject wind;
    [SerializeField]
    Vector3 currentWindForce;
    private void OnTriggerEnter(Collider other)
    {

        var o = other.gameObject.GetComponentInParent<ParticleSystem>();
        if (o)
        {
            Debug.Log("collided with rain");
            currentWindForce = wind.GetComponent<Wind>().GetWindForce();
            //o.transform.Rotate(currentWindDirection.x * 10, 0f, currentWindDirection.z * 10);
            o.transform.rotation = Quaternion.Euler(-currentWindForce.z / 150 * 90f, 0f, currentWindForce.x / 150 * 90f);
        }
    }
        
    private void OnTriggerStay(Collider other)
    {
        var o = other.gameObject.GetComponentInParent<ParticleSystem>();
        if (o)
        {
           // o.WindIsBlowing();
          //  o.SetWindForece(wind.GetComponent<Wind>().GetWindForce()* 0.1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var o = other.gameObject.GetComponentInParent<ParticleSystem>();
        if (o)
        {
            o.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
