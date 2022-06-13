using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(RatEnemyStats))]
public class NewRatAI : EnemyAI
{
    [Header("Rat Attack Stuff")]
    bool AttackReady = true;
    bool startanim;
    RatAttack ratAttack;
    public int MaxAttackCount = 1;
    public SpriteRenderer SpriteRen;

    public LayerMask RatAttackDetection;

    void Start()
    {
        this.PlayerRef = base.PlayerRef;
        ratAttack = this.GetComponent<RatAttack>();
        InvokeRepeating("PlayerDetection", 0.1f, 0.2f);
        InvokeRepeating("PathRefresh", 0.1f, 0.2f);
    }
    
    Vector2 spriteFlipCompareVector = new Vector2();

    private void FixedUpdate()
    {
        MyAnim.SetFloat("XYVelocity", MyRB.velocity.magnitude);

        base.MoveToPlayer();

        CombatLogic();

        if (PlayerDetected)
        {
            
            switch (MyEnemyStats.EnemyCurrentState)
            {
                case EnemyState.enemyIdle:
                    spriteFlipCompareVector = PlayerRef.transform.position - this.transform.position;
                    break;
                case EnemyState.enemyWindup:
                    if (ratAttack.indicatorFollowing)
                    {
                        spriteFlipCompareVector = ratAttack.IndicatorDir;
                    }
                    else
                    {
                        spriteFlipCompareVector = (Vector3)ratAttack.LeapTargetPosition - this.transform.position;
                    }
                    break;
                case EnemyState.enemyAttacking:
                        spriteFlipCompareVector = (Vector3)ratAttack.LeapTargetPosition - this.transform.position;
                    break;
            }
            if (spriteFlipCompareVector.x > 0.1f)
            {
                SpriteRen.flipX = true;
                return;
            }
            else
            {
                SpriteRen.flipX = false;
            }

        }
    }

 
    void CombatLogic()
    {
        if(PlayerStats._CurrentPlayerStats.isTalking)
        {
            return;
        }
        RaycastHit2D COL = Physics2D.Raycast(this.transform.position, ((PlayerRef.transform.position) - this.transform.position).normalized, Vector2.Distance(PlayerRef.transform.position, this.transform.position), RatAttackDetection);
        if(COL.collider!=null)
        {
            //Debug.Log("Wall/Gap obstacle Detected");
            return;
        }
        float Dist = Vector2.Distance(this.transform.position, PlayerRef.transform.position);
        if(!PlayerDetected || Dist>AttackRange || this.MyEnemyStats.EnemyCurrentState == EnemyState.enemyStunned || this.MyEnemyStats.EnemyCurrentState == EnemyState.enemyRecovery || this.MyEnemyStats.EnemyCurrentState == EnemyState.enemyWindup || this.MyEnemyStats.EnemyCurrentState == EnemyState.enemyAttacking || AttackReady == false)
        {

            return;
        }

        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        StartCoroutine(ratattack());
    }

    IEnumerator ratattack() // the cooldown for the attack
    {
        if (AttackReady)
        {
            StartCoroutine(AttackCooldownFunction(MyEnemyStats.myStats.AttackCooldown));
            MyAnim.SetTrigger("PlayerAttack");
        }
        yield break;
    }
    /*
    IEnumerator ratattack() // the cooldown for the attack
    {
        if(AttackReady)
        {
            
            AttackReady = false;
            MyAnim.SetTrigger("PlayerAttack");
        }
        yield return new WaitForSeconds(MyEnemyStats.myStats.AttackCooldown);

        AttackReady = true;
        yield break;
    }
    */

    public IEnumerator AttackCooldownFunction(float CoolDownAmount)
    {
        AttackReady = false;
        yield return new WaitForSeconds(CoolDownAmount);
        AttackReady = true;
        yield break;
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, DetectionDistance);
        if (PlayerRef != null )
        {
            if(PlayerDetected == true)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.blue;
            }
            Gizmos.DrawLine(this.transform.position, PlayerRef.transform.position);
        }
    }

    /*
    void MoveToPlayer()
    {
        if (MyPath == null ||  MyEnemyStats.EnemyCurrentState != EnemyState.enemyIdle || !PlayerDetected)
        {

            return;
        }
        float PATHDISTANCE = 0f;
        for(int i = 1; i<MyPath.vectorPath.Count-1; i++)
        {
            PATHDISTANCE += Vector3.Distance(MyPath.vectorPath[i], MyPath.vectorPath[i - 1]);
        }
        if(PATHDISTANCE < StoppingDistance)
        {
            return;
        }

        if (pathWaypointIndex >= MyPath.vectorPath.Count)
        {
            finishedPath = true;
            return;
        }
        else
        {
            finishedPath = false;
        }

        Vector2 Direction = ((Vector2)MyPath.vectorPath[pathWaypointIndex] - MyRB.position).normalized;

        Vector2 newVel = Direction * this.Speed;
        Vector2 AverageVector = new Vector2((newVel.x + MyRB.velocity.x) / 2, (newVel.y + MyRB.velocity.y) / 2);
        //MyRB.velocity = newVel;
        MyRB.velocity = Vector3.Lerp(MyRB.velocity, AverageVector, 20f*Time.deltaTime);
        
        if (Vector2.Distance(MyRB.position, MyPath.vectorPath[pathWaypointIndex]) < NextWayPointDistance)
        {
            pathWaypointIndex++;
        }


    }
    */

    /*
    void PathRefresh()
    {
        MySeeker.StartPath(MyRB.position, PlayerRef.transform.position, SeekPlayer);
    }

    void SeekPlayer(Path pathToPlayer)
    {
        if(!pathToPlayer.error)
        {
            MyPath = pathToPlayer;
            this.pathWaypointIndex = 0;
        }
    }
    */
}
