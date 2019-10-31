using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WindDirection
{
    North,
    NorthWest,
    West,
    SouthWest,
    South,
    SouthEast,
    East,
    NorthEast
}

public class Wind : MonoBehaviour
{
    public GameObject wind;
    WindDirection direction;

    [SerializeField]
    float windCD;
    [SerializeField]
    float CDCount;
    [SerializeField]
    float randomX;
    [SerializeField]
    float randomZ;

    [SerializeField]
    float windSpeedMin = 50.0f;
    [SerializeField]
    float windSpeedMax = 150.0f;

    Vector3 windForce;
    Vector3 calculatedForce;

    int randomDirection;

    int randomXSize;
    int randomZSize;

    public Vector3 GetWindForce()
    {
        return windForce;
    }

    // Start is called before the first frame update
    void Start()
    {
        wind.SetActive(false);
        wind.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.0f);
    }

    
    // Update is called once per frame
    void Update()
    {
        if(CDCount > windCD)
        {
            CalculateWindForce();
            WindReset();
        }
        windForce = calculatedForce * Time.deltaTime;
        wind.transform.Translate(windForce);
        CDCount += Time.deltaTime;
        WindBorderCheck();
    }


    void WindReset()
    {
        randomDirection = Random.Range(0, 7);
        direction = (WindDirection)randomDirection;
        windCD = Random.Range(6.0f, 10.0f);
        CDCount = 0.0f;
        windForce = new Vector3(0f, 0f, 0f);
        wind.SetActive(true);
        wind.transform.localScale -= new Vector3(randomXSize, 0.0f, randomXSize);
        randomXSize = Random.Range(40, 130);
        randomZSize = Random.Range(40, 130);
        wind.transform.localScale += new Vector3(randomXSize, 0.0f, randomXSize);
    }

    void WindBorderCheck()
    {
        if ((wind.transform.position.x > 100) || (wind.transform.position.x < -100) || (wind.transform.position.z > 100) || (wind.transform.position.z < -100))
        {
            wind.SetActive(false);
            windForce = new Vector3(0f, 0f, 0f);
        }
    }

    void CalculateWindForce()
    {
        switch (direction)
        {
            case WindDirection.North:
                wind.transform.position = new Vector3(0.0f, transform.position.y, 50.0f);
                calculatedForce = new Vector3(0f, 0f, Random.Range(-windSpeedMin, -windSpeedMax));
                break;
            case WindDirection.NorthWest:
                wind.transform.position = new Vector3(-50.0f, transform.position.y, 50.0f);
                calculatedForce = new Vector3(Random.Range(windSpeedMin, windSpeedMax), 0f, Random.Range(-windSpeedMin, -windSpeedMax));
                break;
            case WindDirection.West:
                wind.transform.position = new Vector3(-50.0f, transform.position.y, 0.0f);
                calculatedForce = new Vector3(Random.Range(windSpeedMin, windSpeedMax), 0f, 0f);
                break;
            case WindDirection.SouthWest:
                wind.transform.position = new Vector3(-50.0f, transform.position.y, -50.0f);
                calculatedForce = new Vector3(Random.Range(windSpeedMin, windSpeedMax), 0f, Random.Range(windSpeedMin, windSpeedMax));
                break;
            case WindDirection.South:
                wind.transform.position = new Vector3(0.0f, transform.position.y, -50.0f);
                calculatedForce = new Vector3(0f, 0f, Random.Range(windSpeedMin, windSpeedMax));
                break;
            case WindDirection.SouthEast:
                wind.transform.position = new Vector3(50.0f, transform.position.y, -50.0f);
                calculatedForce = new Vector3(Random.Range(-windSpeedMin, -windSpeedMax), 0f, Random.Range(windSpeedMin, windSpeedMax));
                break;
            case WindDirection.East:
                wind.transform.position = new Vector3(50.0f, transform.position.y, 0.0f);
                calculatedForce = new Vector3(Random.Range(-windSpeedMin, -windSpeedMax), 0f, 0f);
                break;
            case WindDirection.NorthEast:
                wind.transform.position = new Vector3(50.0f, transform.position.y, 50.0f);
                calculatedForce = new Vector3(Random.Range(-windSpeedMin, -windSpeedMax), 0f, Random.Range(-windSpeedMin, -windSpeedMax));
                break;
        }
    }
}
