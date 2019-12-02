using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public TimeManager timeManager;
    public AudioSource[] clips;
    bool[] areClipsPlayed;

    // Start is called before the first frame update
    void Start()
    {
        areClipsPlayed = new bool[clips.Length];
        for (int i = 0; i < areClipsPlayed.Length; i++)
        {
            areClipsPlayed[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((timeManager.GetCurrentTime() < 45) && (areClipsPlayed[0] == false))
        {
            clips[0].Play(0);
            areClipsPlayed[0] = true;
        }
    }
}
