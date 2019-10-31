using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafFalling : MonoBehaviour
{
    public GameObject leaf;

    [SerializeField]
    float directionChangeCD;
    [SerializeField]
    float CDCount;
    [SerializeField]
    float randomX;
    [SerializeField]
    float randomZ;
    [SerializeField]
    bool isWindBlowing;

    float Xlimit;
    float Zlimit;

    [SerializeField]
    Vector3 windForce;

    public void SetWindForece(Vector3 wf)
    {
        windForce = wf;
    }

    public void WindIsBlowing()
    {
        isWindBlowing = true;
    }

    public void WindIsNotBlowing()
    {
        isWindBlowing = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        FallReset();
        isWindBlowing = false;
    }

    void FallReset()
    {
        randomX = Random.Range(-0.5f, 0.5f);
        randomZ = Random.Range(-0.5f, 0.5f);
        directionChangeCD = Random.Range(0.1f, 0.9f);
        CDCount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isWindBlowing)
        {
            if(CDCount > directionChangeCD)
            {
                var temp = new Vector3(randomX, 0.0f, randomZ);
                leaf.GetComponent<Rigidbody>().AddForce(temp);
                FallReset();
            }
            CDCount += Time.deltaTime;
        }
        else
        {
            leaf.GetComponent<Rigidbody>().AddForce(windForce * 0.15f);
        }
    }
}
