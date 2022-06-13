using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class particlestart : MonoBehaviour
{
    ParticleSystem part;
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponentInChildren<ParticleSystem>();

    }

   public void particlestarter()
    {
        part.Play();
    }

    public void particlestopper()
    {
        part.Stop();
    }
 
}
