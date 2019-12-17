using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : Singleton<AudioController>
{
    [SerializeField]
    AudioMixerSnapshot daySnapShot;
    [SerializeField]
    AudioMixerSnapshot nightSnapShot;

    public void DayMusic(float time)
    {
        daySnapShot.TransitionTo(time);
    }

    public void NightMusic(float time)
    {
        nightSnapShot.TransitionTo(time);
    }
}
