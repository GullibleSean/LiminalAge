using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandRotation : MonoBehaviour
{
    [SerializeField]
    private float mCooldown;
    [SerializeField]
    private float mInterval;
    private int mSpeed;
    private Vector3 mDirection;
    public GameObject vrAvatar;

    public bool isClockwise;

    Vector3 rotateVec;

    float speed;

    private void Start()
    {
        mCooldown = 0.0f;
        mInterval = Random.Range(1f, 3f);
        mSpeed = Random.Range(1, 2);
        mDirection = Vector3.zero;

        speed = Random.Range(2f, 3f);

        if (isClockwise)
            rotateVec = Vector3.up;
        else
            rotateVec = Vector3.down;
    }

    private void FixedUpdate()
    {
        if (mCooldown > mInterval)
        {
            mSpeed = -mSpeed;
            mCooldown = 0.0f;
        }
        mDirection.y = mSpeed;
        transform.Translate(mDirection * Time.deltaTime);
        mCooldown += Time.deltaTime;
        transform.RotateAround(vrAvatar.transform.position, rotateVec, speed * Time.deltaTime);
    }
}
