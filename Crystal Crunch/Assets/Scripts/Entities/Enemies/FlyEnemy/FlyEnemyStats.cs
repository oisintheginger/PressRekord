using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class FlyEnemyStats : EnemyStats
{
    ParticleSystem ps;
    FlyAI flyaicomp;
    FMOD.Studio.EventInstance flyHurt;
    FMOD.Studio.EventInstance flydeath;
    FMOD.Studio.EventInstance flyidle;
    FMOD.Studio.EventInstance flyspitting;

    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
        flyaicomp = GetComponent<FlyAI>();
       flyHurt= FMODUnity.RuntimeManager.CreateInstance("event:/fly damaged");
        flyidle = FMODUnity.RuntimeManager.CreateInstance("event:/fly neutral");
        flyspitting = FMODUnity.RuntimeManager.CreateInstance("event:/fly spit");
        flydeath = FMODUnity.RuntimeManager.CreateInstance("event:/Bug explosion");
    }

    public override void TakeDamage(float damageTaken, BaseAttack incomingAttackType, GameObject gameObject = null)
    {
        myStats.Health -= damageTaken;
        // particlemmission();
        //FMODUnity.RuntimeManager.PlayOneShot("event:/fly damaged", transform.position);
        GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);
        
        flyHurt.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        flyHurt.start();
        if (myStats.Health <= 0)
        {
            GetComponent<Animator>().SetTrigger("death");
            this.gameObject.GetComponent<FlyAttack>().ProjectileTargetIndicator.SetActive(false);
            myStats.Health = myStats.MaxHealth;
            
        }
       
    }

    public void flydeathsound()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/Bug explosion", transform.position);
        flydeath.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        flydeath.start();
    }

    public void flyidlesound()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/fly neutral",transform.position);
        flyidle.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        flyidle.start();
    }

    public void flyspittingsound()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/fly spit", transform.position);
        flyspitting.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        flyspitting.start();
    }


    void particlemmission()
    {
        var tex = ps.shape;
        Vector3 postest = (flyaicomp.PlayerRef.transform.position - transform.position);
        float degrees = Mathf.Atan2(-postest.y, -postest.x) * Mathf.Rad2Deg;
        tex.rotation = new Vector3(0, 0, degrees);

        ps.Play();
    }

    public void active()
    {
        this.gameObject.SetActive(false);
    }
}
