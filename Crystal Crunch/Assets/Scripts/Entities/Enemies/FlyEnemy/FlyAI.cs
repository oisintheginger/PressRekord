using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class FlyAI : EnemyAI
{
    public bool isMoveable = true;
    IAstarAI AIComp;
    bool AttackReady = true;
    const string FlyAttackTrigger = "FlyAttack";
    Vector2 AIDestVector = new Vector2();

    [SerializeField] SpriteRenderer SpriteRen;


    [SerializeField] LayerMask FlyAttackClearLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        this.MyEnemyStats = this.GetComponent<FlyEnemyStats>();
        this.PlayerRef = base.PlayerRef;
        InvokeRepeating("PlayerDetection", 0.1f, 0.2f);
        InvokeRepeating("PathCheck", 0.1f, 0.2f);
        AIComp = this.GetComponent<IAstarAI>();
        AIDestVector = PlayerRef.transform.position;
        AIComp.destination = AIDestVector;
    }


    Vector2 spriteFlipCompareVector = new Vector2();

    void FixedUpdate()
    {
        FlyMove();
        SpriteFlipping();
    }

    public void FlyMove()
    {
        
        if (MyPath == null || MyEnemyStats.EnemyCurrentState != EnemyState.enemyIdle || !PlayerDetected )
        {
            isMoveable = false;
        }

        CombatLogic(); 
        AIComp.canMove = isMoveable;
        
    }

    void SpriteFlipping()
    {
        spriteFlipCompareVector = PlayerRef.transform.position - this.transform.position;
        if (spriteFlipCompareVector.x > 0.1f)
        {
            SpriteRen.flipX = false;
            return;
        }
        else
        {
            SpriteRen.flipX = true;
        }
    }

    public void PathCheck()
    {
        MySeeker.StartPath(MyRB.position, AIDestVector, SeekCheck);
    }

    public void SeekCheck(Path pathToPlayer)
    {
        if (!pathToPlayer.error && pathToPlayer.vectorPath.Count>0)
        {
            MyPath = pathToPlayer;
            Vector3 pathEndPos = MyPath.vectorPath[MyPath.vectorPath.Count - 1];

            if (Vector2.Distance(this.transform.position, PlayerRef.transform.position) < 6f)
            {
                AIDestVector = (Vector2)this.transform.position - (Vector2)((PlayerRef.transform.position - this.transform.position).normalized * 3f);
            }
            else
            {
                AIDestVector = PlayerRef.transform.position;
            }

            if (Vector3.Distance(this.transform.position, AIDestVector) > 10f)
            {

               // isMoveable = false;
               // return;
            }
            isMoveable = true;
        }
    }


    void CombatLogic()
    {
        RaycastHit2D COL = Physics2D.Raycast(this.transform.position, ((PlayerRef.transform.position) - this.transform.position).normalized, Vector2.Distance(PlayerRef.transform.position, this.transform.position), FlyAttackClearLayerMask);
        if (COL.collider != null)
        {
            return;
        }
        if (Vector3.Distance(this.transform.position, PlayerRef.transform.position) < this.AttackRange && AttackReady == true)
        {
            StartCoroutine(AttackAnimationTrigger());
        }
    }


    IEnumerator AttackAnimationTrigger()
    {
        AttackReady = false;
        MyAnim.SetTrigger(FlyAttackTrigger);
        yield return new WaitForSeconds(MyEnemyStats.myStats.AttackCooldown);
        AttackReady = true;
    }
 
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(this.transform.position, AttackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, DetectionDistance);
        if (PlayerRef != null)
        {
            if (PlayerDetected == true)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.blue;
            }
            Gizmos.DrawLine(this.transform.position, PlayerRef.transform.position);
        }
        Gizmos.color = Color.black;

        if(PlayerRef!=null)
            Gizmos.DrawLine(this.transform.position, this.transform.position - ((PlayerRef.transform.position - this.transform.position)*3));

    }


}
