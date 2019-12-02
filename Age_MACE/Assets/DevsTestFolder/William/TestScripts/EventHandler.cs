﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public MeshRenderer leaf;

    public MeshRenderer grass;

    public Material daySky;
    public Light sun;
    public Material nightSky;

    private MeshRenderer[] subGrass;

    public ParticleSystem[] rainParticles;

    public ParticleSystem[] snowParticles;

    public ParticleSystem[] leafParticles;

    [SerializeField]
    Color SummerColor = new Color(0f, 1.0f, 0f);
    [SerializeField]
    Color FallColor = new Color(1.0f, 0.4f, 0f);
    [SerializeField]
    Color WinterColor = new Color(1.0f, 1.0f, 1.0f);
    [SerializeField]
    Color SpringColor = new Color(0.4f, 1.0f, 0.2f);

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

    int rainParticleRate;
    int snowParticleRate;
    int leafParticleRate;

    void DisableParticles(ParticleSystem[] particles)
    {
        foreach (var particle in particles)
        {
            particle.gameObject.SetActive(false);
        }
    }

    void EnableParticles(ParticleSystem[] particles)
    {
        foreach (var particle in particles)
        {
            particle.gameObject.SetActive(true);
            var emission = particle.emission;
            emission.rateOverTime = 0;
        }
    }

    void SetParticlesRateOverTime(ParticleSystem[] particles, int rate)
    {
        foreach (var particle in particles)
        {
            if(particle.GetComponent<LeafFalling>())
            {
                var main = particle.main;
                main.startColor = CurrentColor;
            }
            var emission = particle.emission;
            emission.rateOverTime = rate;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeTracker = 0.0f;

        subGrass = grass.GetComponentsInChildren<MeshRenderer>();

        STF_Color = (FallColor - SummerColor) / GetComponent<TimeManager>().GetSTFDuration();
        FTW_Color = (WinterColor - FallColor) / GetComponent<TimeManager>().GetFTWDuration();
        WTS_Color = (SpringColor - WinterColor) / GetComponent<TimeManager>().GetWTSDuration();

        Stage1 = GetComponent<TimeManager>().GetSummerStage();
        Stage2 = GetComponent<TimeManager>().GetSTFStage();
        Stage3 = GetComponent<TimeManager>().GetFallStage();
        Stage4 = GetComponent<TimeManager>().GetFTWStage();
        Stage5 = GetComponent<TimeManager>().GetWinterStage();
        Stage6 = GetComponent<TimeManager>().GetWTSStage();
        Stage7 = GetComponent<TimeManager>().GetSpringStage();


        rainParticleRate = 0;
        snowParticleRate = 0;
        leafParticleRate = 0;

        DisableParticles(snowParticles);
        DisableParticles(rainParticles);
        DisableParticles(leafParticles);

        RenderSettings.skybox = daySky;
    }

    void FixedUpdate()
    {
        TimeTracker = GetComponent<TimeManager>().GetCurrentTime();
        if (TimeTracker < Stage1)
        {
            CurrentColor = SummerColor;

            EnableParticles(rainParticles);
            rainParticleRate++;
            SetParticlesRateOverTime(rainParticles, rainParticleRate);
        }
        else if (TimeTracker < Stage2)
        {
            CurrentColor += STF_Color * Time.deltaTime;

            rainParticleRate -= 10;
            SetParticlesRateOverTime(rainParticles, rainParticleRate);
        }
        else if (TimeTracker < Stage3)
        {
            CurrentColor = FallColor;

            EnableParticles(leafParticles);
            leafParticleRate++;
            SetParticlesRateOverTime(leafParticles, leafParticleRate);
        }
        else if (TimeTracker < Stage4)
        { 
            CurrentColor += FTW_Color * Time.deltaTime;

            leafParticleRate -= 10;
            SetParticlesRateOverTime(leafParticles, leafParticleRate);
        }
        else if (TimeTracker < Stage5)
        {
            CurrentColor = WinterColor;

            DisableParticles(rainParticles);
            EnableParticles(snowParticles);
            snowParticleRate++;
            SetParticlesRateOverTime(snowParticles, snowParticleRate);
        }
        else if (TimeTracker < Stage6)
        {
            CurrentColor += WTS_Color * Time.deltaTime;

            snowParticleRate -= 10;
            SetParticlesRateOverTime(snowParticles, snowParticleRate);
        }
        else if (TimeTracker < Stage7)
        {
            DisableParticles(snowParticles);
            CurrentColor = SpringColor;

            RenderSettings.skybox = nightSky;
            sun.gameObject.SetActive(false);
        }

        leaf.materials[0].color = CurrentColor;
        grass.material.color = CurrentColor;
        foreach (var grass in subGrass)
        {
            grass.material.color = CurrentColor;
        }
    }
}
