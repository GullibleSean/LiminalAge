using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public TimeManager timeManager;
    AudioSource audioSource;
    public AudioClip[] clips;
    public float[] audioStartTimes;
    bool[] areClipsPlayed;
    int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        areClipsPlayed = new bool[clips.Length];
        for (int i = 0; i < areClipsPlayed.Length; i++)
        {
            areClipsPlayed[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeManager.GetCurrentTime() > audioStartTimes[currentIndex])
        {
            currentIndex++;
        }
        if (!areClipsPlayed[currentIndex])
            PlayClip(currentIndex);

    }

    void PlayClip(int index)
    {
        audioSource.clip = clips[index];
        audioSource.Play(0);
        areClipsPlayed[index] = true;
        Debug.Log("Play clip called");
    }
}
