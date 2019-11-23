using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingRock : MonoBehaviour
{
    [SerializeField]
    private float mCooldown;
    [SerializeField]
    private float mInterval;
    private int mSpeed;
    private Vector3 mDirection;


    // Start is called before the first frame update
    void Start()
    {
        mCooldown = 0.0f;
        mInterval = Random.Range(1f, 3f);
        mSpeed = Random.Range(5, 10);
        mDirection = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if(mCooldown > mInterval)
        {
            mSpeed = -mSpeed;
            mCooldown = 0.0f;
        }
        mDirection.y = mSpeed;
        transform.Translate(mDirection * Time.deltaTime);
        mCooldown += Time.deltaTime;
    }
}
