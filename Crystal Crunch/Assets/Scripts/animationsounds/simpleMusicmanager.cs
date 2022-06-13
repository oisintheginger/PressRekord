using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;


public class simpleMusicmanager : MonoBehaviour
{
    public bool combatcheck = false;
    FMOD.Studio.EventDescription Event;
    FMOD.Studio.PARAMETER_DESCRIPTION Parameter;
    FMOD.Studio.PARAMETER_ID paraid;
    FMOD.Studio.EventInstance atmos;
    public float check;
    int tempcounter;
    public void Start()
    {
        Event = RuntimeManager.GetEventDescription("event:/Music");
        atmos = FMODUnity.RuntimeManager.CreateInstance("event:/cave atmosphere sounds");
        Event.getParameterDescriptionByName("enemy encounter",out Parameter);
        paraid = Parameter.id;
        StartCoroutine("musicchange");
    }
   
   
 

  public IEnumerator musicchange()
    {
        atmos.start();
        yield return new WaitForSecondsRealtime(Random.Range(7, 20));
        StartCoroutine("musicchange");
        yield return null;
    }


}
