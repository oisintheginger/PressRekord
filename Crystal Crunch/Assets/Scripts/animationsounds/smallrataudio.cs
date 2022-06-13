using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallrataudio : MonoBehaviour
{
    FMOD.Studio.EventInstance RatHurt;
    FMODUnity.StudioEventEmitter ratattack;
    // Start is called before the first frame update
    void Start()
    {
        ratattack = GetComponent <FMODUnity.StudioEventEmitter>();
        //Ratattack = FMODUnity.RuntimeManager.CreateInstance("event:/small rat attack");
        RatHurt = FMODUnity.RuntimeManager.CreateInstance("event:/small rat damage");
    }

    public void Ratattacksfx()
    {
        ratattack.Play();
    }

    public void Rathurtsfx()
    {
        RatHurt.start();
    }
   
}
