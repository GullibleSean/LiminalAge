using UnityEngine;
using System.Collections.Generic;

public class FPSCount : MonoBehaviour
{
    [SerializeField]
    TextMesh text;

    [SerializeField]
    float fpsSampleRange = 10;

    private Queue<float> previousFPSvalues;

    private void Awake()
    {
        previousFPSvalues = new Queue<float>();
    }

    private void Update()
    {
        float fps = (1f / Time.unscaledDeltaTime);

        if (previousFPSvalues.Count >= fpsSampleRange)
            previousFPSvalues.Dequeue();

        previousFPSvalues.Enqueue(fps);

        float averageFPS = 0;

        foreach (float value in previousFPSvalues)
        {
            averageFPS += value;
        }

        averageFPS /= previousFPSvalues.Count;

        text.text = averageFPS.ToString("F1");

        print("fpsSampleRange" + fpsSampleRange);
        print("previousFPSvalue" + previousFPSvalues);
        print("unscaledDeltaTime" + Time.unscaledDeltaTime);
        print("averageFPS" + averageFPS);
    }
}