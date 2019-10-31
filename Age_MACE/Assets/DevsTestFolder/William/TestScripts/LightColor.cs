using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColor : MonoBehaviour
{
    Light lt;

    public GameObject timeManager;

    [SerializeField]
    Color SummerColor = new Color(1.0f, 1.0f, 0f);
    [SerializeField]
    Color FallColor = new Color(1.0f, 0.4f, 0f);
    [SerializeField]
    Color WinterColor = new Color(1.0f, 1.0f, 1.0f);
    [SerializeField]
    Color SpringColor = new Color(1.0f, 1.0f, 0.6f);

    [SerializeField]
    Color CurrentColor;

    Color STF_Color;
    Color FTW_Color;
    Color WTS_Color;

    [SerializeField]
    float TimeTracker;

    float Stage1;
    float Stage2;
    float Stage3;
    float Stage4;
    float Stage5;
    float Stage6;
    float Stage7;

    void Start()
    {
        lt = GetComponent<Light>();
        TimeTracker = 0.0f;

        STF_Color = (FallColor - SummerColor) / timeManager.GetComponent<TimeManager>().GetSTFDuration();
        FTW_Color = (WinterColor - FallColor) / timeManager.GetComponent<TimeManager>().GetFTWDuration();
        WTS_Color = (SpringColor - WinterColor) / timeManager.GetComponent<TimeManager>().GetWTSDuration();

        Stage1 = timeManager.GetComponent<TimeManager>().GetSummerStage();
        Stage2 = timeManager.GetComponent<TimeManager>().GetSTFStage();
        Stage3 = timeManager.GetComponent<TimeManager>().GetFallStage();
        Stage4 = timeManager.GetComponent<TimeManager>().GetFTWStage();
        Stage5 = timeManager.GetComponent<TimeManager>().GetWinterStage();
        Stage6 = timeManager.GetComponent<TimeManager>().GetWTSStage();
        Stage7 = timeManager.GetComponent<TimeManager>().GetSpringStage();
    }

    // Darken the light completely over a period of 10 seconds.
    void Update()
    {
        TimeTracker = timeManager.GetComponent<TimeManager>().GetCurrentTime();
        if (TimeTracker < Stage1)
        {
            CurrentColor = SummerColor;
        }
        else if (TimeTracker < Stage2)
        {
            CurrentColor +=  STF_Color * Time.deltaTime;
        }
        else if (TimeTracker < Stage3)
        {
            CurrentColor = FallColor;
        }
        else if (TimeTracker < Stage4)
        {
            CurrentColor += FTW_Color * Time.deltaTime;
        }
        else if (TimeTracker < Stage5)
        {
            CurrentColor = WinterColor;
        }
        else if (TimeTracker < Stage6)
        {
            CurrentColor +=  WTS_Color * Time.deltaTime;
        }
        else if (TimeTracker < Stage7)
        {
            CurrentColor = SpringColor;
        }

        lt.color = CurrentColor;

        RenderSettings.skybox.SetColor("_SkyTint", CurrentColor);
    }
}
