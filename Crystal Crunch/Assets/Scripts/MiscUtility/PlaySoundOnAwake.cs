using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAwake : MonoBehaviour
{
    FMOD.Studio.EventInstance Trainnoise;
    [SerializeField] string soundtoplay = "event:/train sfx";
    void Start()
    {
        Trainnoise = FMODUnity.RuntimeManager.CreateInstance(soundtoplay);
        Trainnoise.start();
    }

}
