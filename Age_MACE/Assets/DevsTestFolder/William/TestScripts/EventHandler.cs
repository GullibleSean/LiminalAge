using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public MeshRenderer[] leaves;

    public MeshRenderer BigTree;

    public MeshRenderer grass;
    public MeshRenderer terrian;

    public Material daySky;
    public Light sun;
    public Material nightSky;

    public ParticleSystem[] rainParticles;

    public ParticleSystem[] snowParticles;

    public ParticleSystem[] leafParticles;

    public GameObject fogParticles;

    public GameObject auroraParticles;

    [SerializeField]
    Color SummerColor = new Color(0.2f, 0.6f, 0.2f);
    [SerializeField]
    Color FallColor = new Color(1.0f, 0f, 0f);
    [SerializeField]
    Color WinterColor = new Color(1.0f, 1.0f, 1.0f);
    [SerializeField]
    Color SpringColor = new Color(1.0f, 1.0f, 0.0f);

    [SerializeField]
    Color CurrentColor;

    Color STF_Color;
    Color FTW_Color;
    Color WTS_Color;

    Color DTNSky_Color;
    float DTNAtmosphereThickness;

    Color SkyTintColor;
    float SkyAtmoSpehereThickness;

    Color SkyOriginalTintColor;
    float SkyOriginalAtmosphereThickness;   

    [SerializeField]
    float TimeTracker;

    float Stage1;
    float Stage2;
    float Stage3;
    float Stage4;
    float Stage5;
    float Stage6;
    float Stage7;
    float DTNDuration;

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

        STF_Color = (FallColor - SummerColor) / GetComponent<TimeManager>().GetSTFDuration();
        FTW_Color = (WinterColor - FallColor) / GetComponent<TimeManager>().GetFTWDuration();
        WTS_Color = (SpringColor - WinterColor) / GetComponent<TimeManager>().GetWTSDuration();
        DTNDuration = GetComponent<TimeManager>().GetDTNDuration();
        DTNSky_Color = (nightSky.GetColor("_SkyTint") - daySky.GetColor("_SkyTint")) / DTNDuration;
        DTNAtmosphereThickness = (nightSky.GetFloat("_AtmosphereThickness") - daySky.GetFloat("_AtmosphereThickness")) / DTNDuration;

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
        fogParticles.SetActive(false);
        auroraParticles.SetActive(false);

        RenderSettings.skybox = daySky;
        SkyTintColor = daySky.GetColor("_SkyTint");
        SkyAtmoSpehereThickness = daySky.GetFloat("_AtmosphereThickness");

        SkyOriginalTintColor = SkyTintColor;
        SkyOriginalAtmosphereThickness = SkyAtmoSpehereThickness;
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

            if (TimeTracker > (Stage4 - DTNDuration))
            {
                SkyTintColor += DTNSky_Color * Time.deltaTime;
                SkyAtmoSpehereThickness += DTNAtmosphereThickness * Time.deltaTime;
                RenderSettings.skybox.SetColor("_SkyTint", SkyTintColor);
                RenderSettings.skybox.SetFloat("_AtmosphereThickness", SkyAtmoSpehereThickness);
            }
        }
        else if (TimeTracker < Stage5)
        {
            CurrentColor = WinterColor;

            fogParticles.SetActive(true);

            DisableParticles(rainParticles);
            EnableParticles(snowParticles);
            snowParticleRate++;
            SetParticlesRateOverTime(snowParticles, snowParticleRate);

            RenderSettings.skybox = nightSky;
            sun.gameObject.SetActive(false);
            daySky.SetColor("_SkyTint", SkyOriginalTintColor);
            daySky.SetFloat("_AtmosphereThickness", SkyOriginalAtmosphereThickness);
        }
        else if (TimeTracker < Stage6)
        {
            CurrentColor += WTS_Color * Time.deltaTime;

            snowParticleRate -= 10;
            SetParticlesRateOverTime(snowParticles, snowParticleRate);
        }
        else if (TimeTracker < Stage7)
        {
            fogParticles.SetActive(false);

            DisableParticles(snowParticles);
            CurrentColor = SpringColor;

            auroraParticles.SetActive(true);
        }

        foreach (var leaf in leaves)
        {
            leaf.materials[1].color = CurrentColor;
        }
        terrian.material.color = CurrentColor;
        BigTree.materials[0].color = CurrentColor;
        grass.material.color = CurrentColor;
    }
}