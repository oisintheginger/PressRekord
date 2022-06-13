using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.ParticleSystemJobs;

public class Bosstinai : EnemyAI
{
    public bool isMoveable = true;
    IAstarAI AIComp;
    bool AttackReady = true;
    Vector2 AIDestVector = new Vector2();
    bool attackchainreset;
    [SerializeField] GameObject wavespawner;
    [SerializeField] GameObject rocks;
    [SerializeField] ParticleSystem particle;

    void Start()
    {
        this.MyEnemyStats = this.GetComponent<tinnoodlestats>();
        this.PlayerRef = base.PlayerRef;
        InvokeRepeating("PlayerDetection", 0.1f, 0.2f);

        AIDestVector = PlayerRef.transform.position;
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent += ResetOnDeath;

    }

   public void Update()
    {
        if (PlayerDetected == true && attackchainreset == false)
        {
            attackchainreset = true;
            MyAnim.SetTrigger("start");
        }
        else if (PlayerDetected == false)
        {
            attackchainreset = false;
            MyAnim.SetTrigger("stop");
        }
    }

    void ResetOnDeath()
    {
        PlayerDetected = false;
        attackchainreset = false;
        MyAnim.SetTrigger("stop");
    }

    public void screamspawn()
    {
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerHeavyAttack, 1f);
        Instantiate(wavespawner, gameObject.transform.position, gameObject.transform.rotation);
    }

    void attackpick()
    {
        MyAnim.SetInteger("attack", Random.Range(1, 3));
    }

    void particlestart()
    {
        particle.gameObject.SetActive(true);
        particle.Play();
       
    }

    void screamintro()
    {
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerHeavyAttack, 1f);
    }

    void rockspawn()
    {
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerHeavyAttack, 0.2f);
        Vector2 spawnrange = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        Instantiate(rocks, (Vector2)PlayerRef.transform.position + spawnrange, transform.rotation);
    }

    void particlestop()
    {
        particle.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent -= ResetOnDeath;
    }
}
