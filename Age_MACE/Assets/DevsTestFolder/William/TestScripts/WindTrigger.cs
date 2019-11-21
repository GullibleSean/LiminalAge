using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTrigger : MonoBehaviour
{
    public GameObject wind;

    public GameObject rain;

    public GameObject snow;

    [SerializeField]
    Vector3 currentWindForce;

    Vector3 dividedWindForce;

    float rotateTime = 1.5f;
    float rotateTimeCount = 0f;

    bool isInWind;

    private void OnTriggerEnter(Collider other)
    {
        var o = other.gameObject.GetComponentInParent<ParticleSystem>();
        if (o)
        {
            currentWindForce = wind.GetComponent<Wind>().GetWindForce();
            currentWindForce = new Vector3(-currentWindForce.z, 0f, currentWindForce.x);
            dividedWindForce = currentWindForce * 0.1f;
            //o.transform.rotation = Quaternion.Euler(dividedWindForce);
            rotateTimeCount = 0.0f;
            isInWind = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var o = other.gameObject.GetComponentInParent<ParticleSystem>();
        if (o)
        {
            //o.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isInWind = false;
        }
    }

    private void FixedUpdate()
    {
        if(isInWind && rotateTimeCount < rotateTime)
        {
            dividedWindForce += dividedWindForce * Time.deltaTime;
            rain.transform.rotation = Quaternion.Euler(dividedWindForce);
            snow.transform.rotation = Quaternion.Euler(dividedWindForce);
            rotateTimeCount += Time.deltaTime;
        }
        else
        {
            dividedWindForce -= dividedWindForce * Time.deltaTime;
            rain.transform.rotation = Quaternion.Euler(dividedWindForce);
            snow.transform.rotation = Quaternion.Euler(dividedWindForce);
        }

    }
}
