using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, -90f);
        transform.LookAt(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.right, 1.5f * Time.deltaTime);
        transform.LookAt(Vector3.zero);
    }
}
