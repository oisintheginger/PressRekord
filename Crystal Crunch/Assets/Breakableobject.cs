using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class Breakableobject : EnemyStats
{
    GameObject PlayerRef;
    ParticleSystem ps;
    [SerializeField] GameObject wooddestroyed;
    [SerializeField] GameObject healthdrop;
    [SerializeField] bool healthcheck;
    FMOD.Studio.EventInstance crack;
    [SerializeField] BaseAttack AttackVunerability;

    public void Start()
    {
        PlayerRef = FindObjectOfType<PlayerStats>().gameObject;
        ps = GetComponent<ParticleSystem>();
        this.gameObject.TryGetComponent<BaseAttack>(out AttackVunerability);
        crack = FMODUnity.RuntimeManager.CreateInstance("event:/Boxbreak");
    }

    public override void TakeDamage(float damageTaken, BaseAttack incomingAttackType, GameObject gameObject = null)
    {
        if(AttackVunerability!=null && AttackVunerability.GetType() != incomingAttackType.GetType())
        {
            return;
        }

        myStats.Health -= damageTaken;
        particlemmission();
        GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);

        if (myStats.Health <= 0)
        {
            if (healthcheck == true)
            {
                Instantiate(healthdrop, transform.position, transform.rotation);
            }
            if(wooddestroyed!=null)
            {
                crack.start();
                Instantiate(wooddestroyed, transform.position, transform.rotation);
            }
            GameEventsSystem.gameEventsSystem.EnemyEvents.OnEnemyKilled(myIDString);
            this.gameObject.SetActive(false);
            myStats.Health = myStats.MaxHealth;

        }

    }

    void particlemmission()
    {
        var tex = ps.shape;
        Vector3 postest = (PlayerRef.transform.position - transform.position);
        float degrees = Mathf.Atan2(-postest.y, -postest.x) * Mathf.Rad2Deg;
        tex.rotation = new Vector3(0, 0, degrees);

        ps.Play();
    }

}
