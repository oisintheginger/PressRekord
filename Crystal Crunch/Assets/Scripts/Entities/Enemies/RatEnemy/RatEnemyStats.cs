using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class RatEnemyStats : EnemyStats
{
    public const string EnemyID = "enemy_SmallRat"; //used for taking note of 


    NewRatAI RatAIComp;
    RatAttack rAttackComp;
    public float lastStunTime = 0;
    public float stunTimeGap = 5f;
    Rigidbody2D rb;
    ParticleSystem ps;
    [SerializeField] GameObject healthdrop;
    [SerializeField] int healthpercentage;
   [SerializeField] GameObject rathusk;

    void Awake()
    {
        base.Awake();
        RatAIComp = this.gameObject.GetComponent<NewRatAI>();
        rAttackComp = this.gameObject.GetComponent<RatAttack>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        ps = gameObject.GetComponent<ParticleSystem>();
       
        if (SpriteObject != null && SpriteObject.material != null)
        {
            DefaultMaterial = SpriteObject.material;
        }
    }

    float hitGap = 5f;
    float lastHitAnimationTriggerTime;
    public override void TakeDamage(float damageTaken, BaseAttack incomingAttackType, GameObject gameObject = null)
    {

        GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);

        myStats.Health -= damageTaken;
        GetComponent<smallrataudio>().Rathurtsfx();

        if (myStats.Health <= 0)
        {
            Death();
            return;
        }
        StartCoroutine(FlashCoroutine());
        particlemmission();
       

       

        if (incomingAttackType is HeavyAttack && EnemyCurrentState != EnemyState.enemyStunned && Time.time - lastStunTime >= stunTimeGap)
        {
            StartCoroutine(Stunduration(stuntime));
            pushback(40);
        }

        if (incomingAttackType is Finallightattack && EnemyCurrentState != EnemyState.enemyStunned && Time.time - lastStunTime >= stunTimeGap)
        {
            StartCoroutine(Stunduration(stuntime/0.75f));
            pushback(40);
            GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerLeap, 0.2f);

        }
        if(incomingAttackType is counterAttack && EnemyCurrentState != EnemyState.enemyStunned && Time.time - lastStunTime >= stunTimeGap)
        {
            StartCoroutine(timecheck());
            StartCoroutine(Stunduration(stuntime *3));
            pushback(20);
            return;
        }
        else
        {
            pushback(8);
        }

        if (EnemyCurrentState == EnemyState.enemyRecovery || EnemyCurrentState == EnemyState.enemyStunned)
        {
            GetComponent<Animator>().SetTrigger("RatHit");
        }
    }

    void pushback(int timer)
    {
       
       rb.velocity = (transform.position - RatAIComp.PlayerRef.transform.position).normalized*timer;
            
    }

    IEnumerator timecheck()
    {
        Time.timeScale = 0.05f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
        yield break;
    }
   

    IEnumerator Stunduration(float Stunduration) // the stun duration (resets after new attack)
    {
        if (myStats.stun == false)
        {
            myStats.stun = true;
            StartCoroutine(RatAIComp.AttackCooldownFunction(2f));
            lastStunTime = Time.time;
            GetComponent<Animator>().SetBool("stunned", true);
            GetComponent<Animator>().SetTrigger("stunstart");
           

            
            yield return new WaitForSecondsRealtime(Stunduration);
            GetComponent<Animator>().SetBool("stunned", false);
            myStats.stun = false;


            yield break;
        }
        yield break;
    }

    void particlemmission()
    {
        var tex = ps.shape;
       Vector3 postest = (RatAIComp.PlayerRef.transform.position - transform.position);
        float degrees = Mathf.Atan2(-postest.y, -postest.x) * Mathf.Rad2Deg;
        tex.rotation = new Vector3(0, 0,degrees);
       
        ps.Play();
    }

    private void OnDestroy()
    {
        GameEventsSystem.gameEventsSystem.EnemyEvents.OnEnemyKilled(EnemyID); 

    }

    void Death()
    {
        int chance = Random.Range(0, healthpercentage);
        Debug.Log(chance);
        Vector2 pos = (RatAIComp.PlayerRef.transform.position - transform.position).normalized / 2;
        Quaternion rot = transform.rotation;
        Instantiate(rathusk, (Vector2)transform.position-pos, rot);
        if (chance >= healthpercentage-1)
        {
            Instantiate(healthdrop, pos + (Vector2)transform.position, rot);
        }
        GameEventsSystem.gameEventsSystem.EnemyEvents.OnEnemyKilled(EnemyID);
        this.gameObject.SetActive(false);

    }
    /*
    public void TakeDamage(float damageTaken, BaseAttack incomingAttackType = null, GameObject objectCheck = null)
    {
        myStats.Health -= damageTaken;
        GetComponent<smallrataudio>().Rathurtsfx();

        if (myStats.Health <= 0)
        {
            Destroy(this.gameObject, 0.6f * Time.deltaTime);
        }
        if(incomingAttackType is HeavyAttack)
        {

        }
    }
    */

}
