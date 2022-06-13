using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using FMODUnity;
using FMOD.Studio;
public class AudioEventsSystem
{
    public event Action<EventInstance> PlayAudioClip; 
    public void OnPlayAudioClip(EventInstance ClipToPlay)
    {
        if(PlayAudioClip!=null)
        {
            PlayAudioClip.Invoke(ClipToPlay);
        }
    }
}
