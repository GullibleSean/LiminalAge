﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Liminal.SDK.Core;
using Liminal.Core.Fader;

public class TimeManager : MonoBehaviour
{
     //public Slider slider;

    [SerializeField]
    float SummerDuration = 10.0f;
    [SerializeField]
    float FallDuration = 10.0f;
    [SerializeField]
    float WinterDuration = 10.0f;
    [SerializeField]
    float SpringDuration = 10.0f;

    [SerializeField]
    float SummTransFallDuration = 5.0f;
    [SerializeField]
    float FallTransWintDuration = 5.0f;
    [SerializeField]
    float WintTransSpriDuration = 5.0f;

    [SerializeField]
    float TotalDuration;

    [SerializeField]
    float TimeTracker;

    float SummerStage;
    float STFStage;
    float FallStage;
    float FTWStage;
    float WinterStage;
    float WTSStage;
    float SpringStage;

    Color fadeColor;
    float fadeDuration;
    bool callFader = false;

    public float GetCurrentTime()
    {
        return TimeTracker;
    }

    public float GetSTFDuration()
    {
        return SummTransFallDuration;
    }

    public float GetFTWDuration()
    {
        return FallTransWintDuration;
    }
    
    public float GetWTSDuration()
    {
        return WintTransSpriDuration;
    }


    public float GetSummerStage()
    {
        return SummerStage;
    }

    public float GetSTFStage()
    {
        return STFStage;
    }

    public float GetFallStage()
    {
        return FallStage;
    }
    
    public float GetFTWStage()
    {
        return FTWStage;
    }

    public float GetWinterStage()
    {
        return WinterStage;
    }

    public float GetWTSStage()
    {
        return WTSStage;
    }

    public float GetSpringStage()
    {
        return SpringStage;
    }


    void Awake()
    {
        TotalDuration = SummerDuration + SummTransFallDuration + FallDuration + FallTransWintDuration + WinterDuration + WintTransSpriDuration + SpringDuration;
        TimeTracker = 0.0f;

        SummerStage = SummerDuration;
        STFStage = SummerStage + SummTransFallDuration;
        FallStage = STFStage + FallDuration;
        FTWStage = FallStage + FallTransWintDuration;
        WinterStage = FTWStage + WinterDuration;
        WTSStage = WinterStage + WintTransSpriDuration;
        SpringStage = WTSStage + SpringDuration;
    }

    void Fader()
    {
        if (callFader == true)
        {
            var fader = ScreenFader.Instance;
            fader.FadeTo(fadeColor, fadeDuration);
            callFader = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TimeTracker < (TotalDuration + fadeDuration))
        {
            TimeTracker += Time.deltaTime;
            if (TimeTracker > TotalDuration)
            {
                callFader = true;
                Fader();
            }
        }
        else
        {
            ExperienceApp.End();
        }
        //slider.value = TimeTracker / TotalDuration;
    }
}
