using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))][RequireComponent(typeof(EnemyStats))][RequireComponent(typeof(Animator))]
public abstract class EnemyAI : MonoBehaviour
{
    public EnemyStats MyEnemyStats;
    public float AttackRange = 10f;
    public GameObject PlayerRef;
    public bool PlayerDetected;
    public float DetectionDistance;
    public LayerMask DetectionMask;
    public float StoppingDistance;
    public float Speed;
    public float NextWayPointDistance;

    public int pathWaypointIndex;
    public bool finishedPath = false;
    public Path MyPath;
    public Seeker MySeeker;
    public Rigidbody2D MyRB;
    public Animator MyAnim;

    public Vector2 LastKnownPlayerLocation = new Vector2();

    void Awake()
    {
        PlayerRef = FindObjectOfType<PlayerStats>().gameObject;
        MySeeker = this.gameObject.GetComponent<Seeker>();
        MyRB = this.gameObject.GetComponent<Rigidbody2D>();
        MyEnemyStats = this.GetComponent<EnemyStats>();
        
        MyAnim = this.GetComponent<Animator>();


    }
    public virtual void MoveToPlayer()
    {
        if (MyPath == null || MyEnemyStats.EnemyCurrentState != EnemyState.enemyIdle || !PlayerDetected)
        {
            return;
        }
        float PATHDISTANCE = 0f;
        for (int i = 1; i < MyPath.vectorPath.Count - 1; i++)
        {
            PATHDISTANCE += Vector3.Distance(MyPath.vectorPath[i], MyPath.vectorPath[i - 1]);
        }
        if (PATHDISTANCE < StoppingDistance)
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
        MyRB.velocity = Vector3.Lerp(MyRB.velocity, AverageVector, 20f * Time.deltaTime);

        if (Vector2.Distance(MyRB.position, MyPath.vectorPath[pathWaypointIndex]) < NextWayPointDistance)
        {
            pathWaypointIndex++;
        }


    }

    public virtual void PlayerDetection()
    {
        if (PlayerRef != null && InputManager.PlayerInDialogue == false)
        {
            if (Vector2.Distance(PlayerRef.transform.position, this.gameObject.transform.position) < DetectionDistance)
            {
                RaycastHit2D RH2D = Physics2D.Raycast(this.transform.position, PlayerRef.transform.position - this.transform.position, DetectionDistance, DetectionMask);
                if (RH2D.collider != null)
                {
                    if (RH2D.collider.gameObject.GetComponent<PlayerStats>() != null)
                    {
                        PlayerDetected = true;

                        LastKnownPlayerLocation = (Vector2)PlayerRef.transform.position;
                    }

                }
            }
        }
    }

    public virtual void PathRefresh()
    {
        MySeeker.StartPath(MyRB.position, PlayerRef.transform.position, SeekPlayer);
    }

    public virtual void SeekPlayer(Path pathToPlayer)
    {
        if (!pathToPlayer.error)
        {
            MyPath = pathToPlayer;
            this.pathWaypointIndex = 0;
        }
    }

    
}
