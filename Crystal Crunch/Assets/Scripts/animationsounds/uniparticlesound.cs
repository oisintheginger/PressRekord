using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uniparticlesound : MonoBehaviour
{
    FMOD.Studio.EventInstance RatHurt;
    ParticleSystem partic;
    List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();
    int counter;
    [SerializeField] string eventer;
 
    // Start is called before the first frame update
    void Start()
    {
        partic = GetComponent<ParticleSystem>();
        //RatHurt = FMODUnity.RuntimeManager.CreateInstance("event:/Bug crawling");
        counter = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        var particleamount = new ParticleSystem.Particle[partic.particleCount];
        partic.GetParticles(particleamount);
       
      
       for (int i = 0; i < particleamount.Length - 1; i++)
       {
            Vector3 poscontainor = particleamount[i].position+transform.position;
            //FMOD.ATTRIBUTES_3D value = FMODUnity.RuntimeUtils.To3DAttributes(poscontainor);
            //RatHurt.set3DAttributes(value);
             FMODUnity.RuntimeManager.PlayOneShot(eventer, poscontainor);
               
                //RatHurt.start();
            //RatHurt.release();
       }
        

    }
       
       
}

