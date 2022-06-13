using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    enemyIdle,
    enemyWindup,
    enemyAttacking,
    enemyRecovery,
    enemyStunned,
    enemyImmune,
    enemyDied
}
public abstract class EnemyStats : Entity, IEnemyDamageable<float, BaseAttack>
{
    public GameObject Player;
    public float stuntime;
    [SerializeField]Animator tempanim;
    public EnemyState EnemyCurrentState;
    public Animator myAnimator;
    [Header("Stuff for Flash when damaged")]
    public SpriteRenderer SpriteObject; 
    [HideInInspector] public Material DefaultMaterial;
    public Material FlashMaterial;
    public void Awake()
    {
        myAnimator = this.GetComponent<Animator>();
    }

    /*
    public void TakeDamage(float damageTaken, GameObject objectCheck)
    {
        myStats.Health -= damageTaken;

        GetComponent<smallrataudio>().Rathurtsfx();
        if (myStats.Health <= 0)
        {
            Destroy(gameObject, 0.6f*Time.deltaTime);
        }

    }
    public void TakeDamage(float damageTaken, BaseAttack incomingAttackType = null, GameObject objectCheck = null)
    {
        myStats.Health -= damageTaken;
        GetComponent<smallrataudio>().Rathurtsfx();

        if (myStats.Health <= 0)
        {
            Destroy(this.gameObject, 0.6f * Time.deltaTime);
        }
    }

    IEnumerator Stunduration(float Stunduration) // the stun duration (resets after new attack)
    {

        if (Time.time - lastStunTime < 5f)
        {
            yield break;
        }
        lastStunTime = Time.time;

        myStats.stun = true;
        GetComponent<Animator>().SetTrigger("stunned");

        EnemyCurrentState = EnemyState.enemyStunned;
        float stuncount = Stunduration;
        while ( stuncount > 0 )
        {
            stuncount -= Time.deltaTime;
            yield return null;
        }
        myStats.stun = false;
        
        EnemyCurrentState = EnemyState.enemyIdle;
    }
    */

    public Color FlashColor;

    private void OnDestroy()
    {
        GameEventsSystem.gameEventsSystem.EnemyEvents.OnEnemyKilled(myIDString, this.gameObject);
    }

    public abstract void TakeDamage(float damageTaken, BaseAttack incomingAttackType, GameObject gameObject = null);

    public IEnumerator FlashCoroutine()
    {
        SpriteObject.material = FlashMaterial;
        FlashMaterial.SetColor("_Tint", FlashColor);
        yield return new WaitForSeconds(0.2f);
        SpriteObject.material = DefaultMaterial;
        yield break;
    }
}
