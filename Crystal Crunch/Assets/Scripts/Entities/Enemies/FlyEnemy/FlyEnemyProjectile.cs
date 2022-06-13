using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemyProjectile : MonoBehaviour
{
    public float MyDamage = .5f;
    public float DamageRadius = 3f;
    Animator MyAnim;
    FMOD.Studio.EventInstance spitcollision;
    public List<Vector2> MyPath;
    public GameObject MyHitIndicatorRef;
    public float ProjectileSpeed = 60f;
    public float ProjectileSpeedMult = 100;
    int CurrentIndex = 0;
    public bool countered = false;
   public Vector2 origin;
    Rigidbody2D rb;
    Collider2D col;
    LightAttack attacktype;

    private void Awake()
    {
        MyAnim = this.gameObject.GetComponentInChildren<Animator>();
       
        
       spitcollision =  FMODUnity.RuntimeManager.CreateInstance("event:/fly spit collision");
        
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        attacktype = GetComponent<LightAttack>();
    }
    bool hitPlayer = false;
    public LayerMask PlayerLayer;   
    public void DoProjectileEffect()
    {
        MyAnim.SetTrigger("Splash");
        Collider2D Cols = Physics2D.OverlapCircle(this.transform.position, DamageRadius, PlayerLayer);
        
        if (Cols!=null && hitPlayer == false)
        {
            GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerDamage(MyDamage, Cols.gameObject);
            hitPlayer = true;
        }
    }

    private void FixedUpdate()
    {
        if (MyPath == null)
        {
            CurrentIndex = 0;
            return;
        }
        if (countered == false)
        {
            MoveAlongPath();
        }
       
    }

    public void flyspittingsound()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/fly spit", transform.position);
        spitcollision.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        spitcollision.start();
    }

   public void pushback(int timer,Vector2 pos)
    {
        transform.parent = null;
        rb.velocity = (origin-(Vector2)transform.position).normalized * timer;
        

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(col.isActiveAndEnabled  && countered == true)
        {
            if (collision.gameObject.layer == 23)
            {
                collision.gameObject.GetComponent<IEnemyDamageable<float, BaseAttack>>().TakeDamage(10, attacktype);
                MyAnim.SetTrigger("Splash");
            }
            if(collision.gameObject.layer == 10)
            {
                MyAnim.SetTrigger("Splash");
            }
        }
    }

    void MoveAlongPath()
    {
        if(MyPath!=null && CurrentIndex < MyPath.Count)
        {
            float SpeedVariable = ProjectileSpeed + (((CurrentIndex) / MyPath.Count) * ProjectileSpeedMult* Time.fixedDeltaTime);

            this.transform.position = Vector2.Lerp(this.transform.position, MyPath[CurrentIndex], SpeedVariable);
            if (Vector2.Distance(this.transform.position, MyPath[CurrentIndex]) < .1f)
            {

                if (CurrentIndex < MyPath.Count - 1)
                {
                    CurrentIndex += 1;
                }
                if (CurrentIndex >= MyPath.Count - 1)
                {
                    MyHitIndicatorRef.SetActive(false);
                    this.GetComponent<FlyEnemyProjectile>().DoProjectileEffect();
                }
            }
        }
    }

    public void splash()
    {
        MyAnim.SetTrigger("Splash");
    }

    public void DestroyProjectile()
    {
       
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, DamageRadius);
    }
}
