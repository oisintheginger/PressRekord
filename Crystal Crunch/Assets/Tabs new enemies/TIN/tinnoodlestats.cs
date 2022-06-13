using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class tinnoodlestats : EnemyStats
{
    ParticleSystem ps;
    tinnoodleai tinai;
    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
        tinai = GetComponent<tinnoodleai>();
    }
    public override void TakeDamage(float damageTaken, BaseAttack incomingAttackType, GameObject gameObject = null)
    {
        myStats.Health -= damageTaken;
        myAnimator.SetTrigger("hit");
        particlemmission();
        if (myStats.Health <= 0)
        {
            GetComponent<Animator>().SetTrigger("death");
            myStats.Health = myStats.MaxHealth;

        }

    }

    void particlemmission()
    {
        var tex = ps.shape;
        Vector3 postest = (tinai.PlayerRef.transform.position - transform.position);
        float degrees = Mathf.Atan2(-postest.y, -postest.x) * Mathf.Rad2Deg;
        tex.rotation = new Vector3(0, 0, degrees);

        ps.Play();
    }

    void deactivate()
    {
        gameObject.SetActive(false);
    }
}
