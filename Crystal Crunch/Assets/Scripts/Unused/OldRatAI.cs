using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class OldRatAI : RedoneEnemyAi
{
    /*
    public LayerMask playerLayer;
    Collider2D[] cols;
    BoxCollider2D Ratcollider;
    Collider2D Playercollider;
    EnemyStats StatsComponent;
    RatAttack Ratattackscript;
    Animator Anim;
    Rigidbody2D ratrb;
    bool Detected = false;
    bool AttackReady = true;
    bool resettrack = true;
    bool stepdone = true;
    [SerializeField] bool Issmallrat;
    
    Vector2 startingpos;

    [SerializeField] Animator spriteAnim;

    private void Awake()
    {
        cols = new Collider2D[5];
        StatsComponent = this.gameObject.GetComponent<EnemyStats>();
        Ratattackscript = gameObject.GetComponent<RatAttack>();
       // Ratattackscript.Maxnumberofattacks = Issmallrat==true ? 1 : 3;
        ratrb = GetComponent<Rigidbody2D>();
        startingpos = transform.position;
        Ratcollider = GetComponent<BoxCollider2D>();
        Anim = GetComponent<Animator>();
    }

    private void Update()
    {

        PlayerDetection();
        this.gameObject.GetComponent<Animator>().SetBool("PlayerDetected", Detected);
    }

    private void FixedUpdate()
    {
        if (StatsComponent.Player != null ) 
        {
            FollowerPlayer(StatsComponent.Player);
            Physics2D.IgnoreCollision(Ratcollider,Playercollider);
        }
        else if(resettrack == false)
        {
            if (stepdone == false)
            {
                resetdata();
            }
            if (Vector2.Distance(transform.position, startingpos) <= 0.5)
            {
                resetdata();
                stepdone = false;
                resettrack = true;


            }
            else
            {
                stepdone = true;
                ratrb.position = Vector2.MoveTowards(transform.position, pathcheck(startingpos), 5 * Time.deltaTime);
            }
        }
       
    }

    void FollowerPlayer(GameObject Player)
    {
        if (stepdone ==true)
        {
            resetdata();
            stepdone = false;
        }
        if (AttackReady == true && StatsComponent.myStats.stun == false )//stops movement when attacking and stunned
        {

            ratrb.position = Vector2.MoveTowards(transform.position, pathcheck(Player.transform.position),5*Time.deltaTime);
            resettrack = false;
            stepdone = false;
            spriteAnim.SetBool("Running", true);
        }
        else
        {
            resetdata();
            
            spriteAnim.SetBool("Running", false);
        }
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if(StatsComponent.Player !=null && other.gameObject==StatsComponent.Player && AttackReady == true && StatsComponent.myStats.stun == false) // checks wether attack is ready or charachter is stunned
        {
           // StartCoroutine("ratattack");
        }
    }
    /*
    IEnumerator ratattack() // the cooldown for the attack
    {
        Anim.SetBool("stunned", StatsComponent.myStats.stun);
        AttackReady = false;
        Anim.SetTrigger("PlayerAttack");
        spriteAnim.SetTrigger("Attack");
        while(Ratattackscript.Attackcount != Ratattackscript.Maxnumberofattacks)
        {
        Anim.SetBool("stunned", StatsComponent.myStats.stun);
        yield return null;
        }
        yield return new WaitForSeconds(0.4f);
        ratrb.velocity = Vector3.zero;
            Anim.ResetTrigger("PlayerAttack");
            Ratattackscript.Attackcount = 0;
            yield return new WaitForSeconds(StatsComponent.myStats.AttackCooldown);
               
        AttackReady = true;
        
    }
    
    void PlayerDetection()
    {
        //cols = Physics.OverlapSphere(this.transform.position, StatsComponent.DetectionRadius, playerLayer);
        cols = Physics2D.OverlapCircleAll(this.transform.position, StatsComponent.DetectionRadius, playerLayer);
        if (cols.Length > 0)
        {
            if (Physics2D.Raycast(this.transform.position, -this.transform.position + cols[0].transform.position, StatsComponent.DetectionRadius))
            {
                StatsComponent.Player = cols[0].gameObject;
                Playercollider = cols[0];
             
            }
            Detected = Physics2D.Raycast(this.transform.position, -this.transform.position + cols[0].transform.position, StatsComponent.DetectionRadius) ? true : false;
        }
        else
        {
            
            StatsComponent.Player = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (cols != null && StatsComponent!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, StatsComponent.DetectionRadius);
            if(cols.Length > 0)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(this.transform.position, -this.transform.position + cols[0].transform.position);
            }
        }
    }
    */
}
