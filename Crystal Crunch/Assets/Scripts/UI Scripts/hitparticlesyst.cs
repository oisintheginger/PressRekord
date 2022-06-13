using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class hitparticlesyst : MonoBehaviour
{
    ParticleSystem part;
    public int eminum;
    bool timed = false;
    float timer = 0;
    Animator anim;
    FMOD.Studio.EventInstance iconsfx;
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        GameEventsSystem.gameEventsSystem.hitevent.actionparticle += particleemitter;
        iconsfx = FMODUnity.RuntimeManager.CreateInstance("event:/icon popper");
        anim = GetComponent<Animator>();
    }

    public void particleemitter(float minsp,float maxsp, int emmittersum)
    {
        anim.SetBool("doublecheck", false);
        anim.SetTrigger("start");
        iconsfx.start();
        timer = 0;
        var tex = part.textureSheetAnimation;
        tex.startFrame = new ParticleSystem.MinMaxCurve(minsp/6, maxsp/6);
        //tex.startFrame = ((float)Random.RandomRange(minsp, maxsp)) / 4;
      
        var emi = part.emission;
        eminum = eminum + emmittersum > 0 ? eminum + emmittersum : 1;
        emi.rateOverTime = eminum;
        part.Play();
        StartCoroutine(reset());
    }

    IEnumerator reset()
    {
        if(timed == false)
        {
            timed = true;
            while (timer <= 8)
            {
                timer = timer + Time.deltaTime;
                yield return null;
            }
            particleemitter(0, 1, -100);
            anim.SetBool("doublecheck", true);
            anim.ResetTrigger("Start");
            anim.SetTrigger("stop");
            timed = false;
            
        }
        yield return null;
    }

    void partstop()
    {
        part.Stop();
    }
}
