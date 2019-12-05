using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraMain : MonoBehaviour {

    [Header("Base Settings")]
    public int auroraSeed;
    [Range(10, 1000)]
    public int auroraParticlesCount;
    [Range(0, 0.99f)]
    public float auroraAnimationFrequency;
    [Range(0, 360f)]
    public float auroraRotation;
    [Range(0, 1f)]
    public float auroraCurvature;
    public Vector3 auroraSizes;
    [Range(1f, 100f)]
    public float auroraParticleThickness;

    [Header("Volumetric Aurora")]
    public bool auroraVolumetric;
    public Vector2 auroraVolumetricRange;

    [Header("Aurora Lights")]
    public bool auroraLights;
    [Range(1, 100)]
    public int auroraLightsCount;
    public float auroraLightsRange;
    public float auroraLightsIntesity;

    [Header("Colors")]
    public Gradient auroraColorMain;

    [Header("Resources")]
    public Material auroraMaterialMain;

    void Start () {

        Initialzie();

	}

    //Local variables
    private ParticleSystem pSystem;
    private ParticleSystem.MainModule p_mMain;
    private ParticleSystem.EmissionModule p_mEmission;
    private ParticleSystemRenderer pRenderer;

    private ParticleSystem.Particle[] p_Particles;
    private Light[] l_Lights;

    // Main Aurora Initialization
    //
    private void Initialzie()
    {
        Random.InitState(auroraSeed);

        GameObject m_Particle = new GameObject("m_Particle");
        m_Particle.transform.SetParent(transform);

        //Create particle system
        pSystem = m_Particle.AddComponent<ParticleSystem>();
        pRenderer = m_Particle.GetComponent<ParticleSystemRenderer>();
        p_mEmission = pSystem.emission;
        p_mMain = pSystem.main;

        p_mEmission.enabled = false;
        p_mMain.startSpeed = 0;

        pRenderer.material = auroraMaterialMain;
        pRenderer.renderMode = ParticleSystemRenderMode.VerticalBillboard;
        pRenderer.maxParticleSize = 100f;

        p_Particles = new ParticleSystem.Particle[auroraParticlesCount];
        pSystem.Emit(auroraParticlesCount);
        pSystem.GetParticles(p_Particles);

        //Create lights
        if (auroraLights)
            InitializeLights();
    }

    //Lights Initialization
    //
    private void InitializeLights()
    {
        l_Lights = new Light[auroraLightsCount];

        Transform m_Lights = new GameObject("m_Lights").transform;
        m_Lights.SetParent(transform);

        for (int i = 0; i < auroraLightsCount; i++)
        {
            Transform obj_Light = new GameObject("AuroraLight " + i).transform;
            obj_Light.SetParent(m_Lights);

            Light l_Light = obj_Light.gameObject.AddComponent<Light>();

            l_Lights[i] = l_Light;
        }
    }

    //Base Aurora Update
    //
    private void FixedUpdate()
    {
        float angleOffset = 0;
        int lightOffset = (auroraParticlesCount - 1) / auroraLightsCount;
        if (auroraVolumetric)
            Random.InitState(auroraSeed);

        for (int i = 0; i < p_Particles.Length; i++)
        {
            float perlin = Mathf.PerlinNoise(Time.time * auroraAnimationFrequency, i * auroraCurvature);
            float offset = perlin * 2f - 1f;

            Vector3 p_Position = Quaternion.Euler(0, auroraRotation + angleOffset, 0) * new Vector3(offset * auroraSizes.x, 0, i * auroraSizes.z / auroraParticlesCount) + transform.position;
            Color p_Color = auroraColorMain.Evaluate((float)i / auroraParticlesCount);

            p_Particles[i].position = p_Position;
            p_Particles[i].startSize3D = new Vector3(auroraParticleThickness, auroraSizes.y, auroraParticleThickness);
            p_Particles[i].startColor = p_Color;

            if (auroraVolumetric)
                angleOffset += Random.Range(auroraVolumetricRange.x, auroraVolumetricRange.y);

            if (auroraLights && i != 0 && i % lightOffset == 0)
            {
                int n = i / (lightOffset + 1);
                l_Lights[n].transform.position = p_Position;
                l_Lights[n].color = p_Color;
                l_Lights[n].range = auroraLightsRange;
                l_Lights[n].intensity = auroraLightsIntesity;
            }
        }
        pSystem.SetParticles(p_Particles, auroraParticlesCount);
    }

}
