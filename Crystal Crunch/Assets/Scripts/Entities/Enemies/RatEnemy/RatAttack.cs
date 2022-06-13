using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RatChargeAttackState
{
    WindUp,
    Charging,
    Damage
}
public class RatAttack : BaseAttack
{
    [SerializeField] LayerMask AttackLayer;
    [SerializeField] GameObject HitEffect;
    int DashLayer;
    int RegularLayer;
    public Vector2 Playpos;
    public int Attackcount;
    Vector2 PlayerDirection;

    public RatChargeAttackState MyChargeState;

    [SerializeField] GameObject DirectionIndicator;

    [SerializeField] float HitblowbackForce;

    public GameObject PlayerReferenceObject;
    public Animator EnemyAnimator;
    public float ChargeSpeed = 15f;
    public float HitRange = 2f;
    public int HitRayLayermask = 9;

    public Vector2 LeapTargetPosition = new Vector2();
    Vector2 LeapDirection = new Vector2();

    [SerializeField] LayerMask AttackDirectionClear;

    float cutofftimer;
    [SerializeField] float attackCutoff = 1f;

    //the rat attack is now a dash. fits a rat more to leap at enemy and better for more open areas.
    [SerializeField] Collider2D[] COLSVISUALIZED;
    private void Start()
    {
        if (this.GetComponent<NewRatAI>().PlayerRef != null)
        {
            PlayerReferenceObject = this.gameObject.GetComponent<EnemyAI>().PlayerRef;
        }
        if(this.gameObject.GetComponent<Animator>()!=null)
        {
            EnemyAnimator = this.gameObject.GetComponent<Animator>();
        }
        RegularLayer = LayerMask.NameToLayer("Enemy");
        DashLayer = LayerMask.NameToLayer("RatDashLayer");
        cutofftimer = attackCutoff;

    }

    public Vector2 IndicatorDir = new Vector2();

    public bool indicatorFollowing = false;
    private void Update()
    {
        if(!indicatorFollowing)
        {
            return;
        }
        IndicatorDir = (PlayerReferenceObject.transform.position - this.transform.position).normalized;
        float angle = Mathf.Atan2(-IndicatorDir.x, IndicatorDir.y)*Mathf.Rad2Deg;
        DirectionIndicator.transform.rotation = Quaternion.AngleAxis(angle+90f, Vector3.forward);

    }

    public void SetAsKinematic()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }
    public void SetAsDynamic()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    bool playerInline()
    {
        bool clear = true;
        RaycastHit2D RH2D = Physics2D.Raycast(this.transform.position, PlayerReferenceObject.transform.position - this.transform.position,Vector2.Distance(this.transform.position, PlayerReferenceObject.transform.position), AttackDirectionClear);
        //RaycastHit2D RH2D = Physics2D.Raycast(this.transform.position, ((Vector3)LeapTargetPosition - this.transform.position).normalized, Vector2.Distance(this.transform.position, LeapTargetPosition), AttackDirectionClear);

        if (RH2D.collider!=null)
        {
            Debug.Log(RH2D.collider.gameObject.name);
            clear = false;
        }
        return clear;
    }

    

    public void RecordPlayerPosition() //records player position a few moments before attack
    {
        PlayerReferenceObject = this.gameObject.GetComponent<EnemyAI>().PlayerRef;
        Playpos = PlayerReferenceObject.transform.position;
        PlayerDirection = ((Playpos - new Vector2(transform.position.x, transform.position.y))).normalized;
        LeapTargetPosition =   (ChargeSpeed  * PlayerDirection) + (Vector2)this.transform.position; 
        LeapDirection = PlayerDirection;
        return;
    }

    public void StartChargeFunction() // exposer for the charging coroutine coroutine
    {
        this.gameObject.layer = DashLayer;
        StartCoroutine(ChargingFunction());
    }

    IEnumerator ChargingFunction()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        bool playerAttacked = false;
        this.GetComponent<Rigidbody2D>().velocity = (LeapTargetPosition - (Vector2)this.transform.position).normalized * ChargeSpeed * 1.5f;
        
        while (this.GetComponent<Rigidbody2D>().velocity.magnitude >.5f)
        {
            RaycastHit2D RH2D = Physics2D.Raycast(this.transform.position, (LeapTargetPosition - (Vector2)this.transform.position).normalized,
                1f,
                AttackDirectionClear);

            if(RH2D.collider!=null) { this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero; }
          
            Collider2D PCol = Physics2D.OverlapCircle(this.transform.position+(Vector3)(PlayerDirection/2), HitRange, AttackLayer);
            if (PCol !=null && playerAttacked == false && PCol.gameObject.layer!=6)
            {
                GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerDamage(this.DamageAmount, PlayerReferenceObject);
                playerAttacked = true;
            } 
            cutofftimer -= Time.deltaTime;
            if(cutofftimer < 0)
            {
                EnemyAnimator.SetTrigger("EnterRecovery");
                cutofftimer = attackCutoff;
                this.gameObject.layer = RegularLayer;
                yield break;
            }
            yield return null;
        }
        this.gameObject.layer = RegularLayer;
        if (Vector2.Distance(PlayerReferenceObject.transform.position, this.transform.position) < HitRange && playerAttacked == false)
        {
            GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerDamage(this.DamageAmount, PlayerReferenceObject);
        }

        EnemyAnimator.SetTrigger("EnterRecovery");

        yield break;
    }

    Vector2 hitDetectionPosition = Vector2.zero;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, HitRange);
        Gizmos.DrawWireSphere(this.transform.position + (Vector3)(PlayerDirection / 2), HitRange);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, (Vector2)this.transform.position + PlayerDirection * 10f);
        Gizmos.DrawWireSphere(LeapTargetPosition, HitRange);
    }
}
