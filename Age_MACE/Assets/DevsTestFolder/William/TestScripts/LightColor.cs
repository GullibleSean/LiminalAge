using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColor : MonoBehaviour
{
    Light lt;

    public GameObject timeManager;
    [SerializeField]
    Color startColor = new Color(255f, 153f, 0f);
    [SerializeField]
    Color midColor = new Color(255f, 255f, 204f);
    [SerializeField]
    Color EndColor = new Color(1f, 0f, 0f);
    [SerializeField]
    Color CurrentColor;

    Color STM_Color;
    Color MTE_Color;

    [SerializeField]
    float TimeTracker;

    void Awake()
    {
        lt = GetComponent<Light>();
        TimeTracker = 0.0f;
        STM_Color = (midColor - startColor) / 60f;
        MTE_Color = (EndColor - midColor) / 60f;
        CurrentColor = startColor;
    }

    // Darken the light completely over a period of 10 seconds.
    void FixedUpdate()
    {
        TimeTracker = timeManager.GetComponent<TimeManager>().GetCurrentTime();
        if(TimeTracker < 60f)
        {
            CurrentColor += STM_Color * Time.deltaTime;
        }
        else if(TimeTracker <= 120f)
        {
            CurrentColor += MTE_Color * Time.deltaTime;
        }

        lt.color = CurrentColor;
    }
}
