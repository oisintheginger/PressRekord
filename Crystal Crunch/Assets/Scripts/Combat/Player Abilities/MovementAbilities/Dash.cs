using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Dash : BaseAbilities
{
    [Header("UI Element")]
    [SerializeField] GameObject AbilityIconFill;

    [SerializeField] int DashLayer;
    [SerializeField] int superdashlayer;
    int RegularLayer;
    //tabstuff
    [SerializeField] HitBox box;
    HeavyAttack heavy;
   public bool superdashcheck =true;
    //tabstuff
    Movement2D movement;
    public Vector2 dashvalue;
    public bool speedcheck;
    //[SerializeField] float Dashstrenght=40;
    [SerializeField] float DashSpeed = 10f;
    [SerializeField] float DashDistance = 5f;
    [SerializeField] float dashcooldown = 1;
    [SerializeField] float dashiteration;

    PlayerLeap altability;
    //Playeraudiomanager Dashsound;
    Animator Anim;
    PlayerStats PS;

    [SerializeField] GameObject dust;
    [SerializeField] LayerMask LM;



    Vector2 PreDashSavePos;
    Vector2 DashToPos = new Vector2();
    public void Start()
    {
        if(unlocked == false)
        {
            AbilityIconFill.SetActive(false);
        }
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerUnlockAbilityEvent += OnUnlock;
        RegularLayer = this.gameObject.layer;
        PS = this.GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement2D>();
        altability = GetComponent<PlayerLeap>();
        //Dashsound = GetComponent<Playeraudiomanager>();
        Anim = GetComponent<Animator>();
        setupinput();
        box = GetComponent<HitBox>();
        heavy = GetComponent<HeavyAttack>();
        PreDashSavePos = gameObject.transform.position;
    }

    void OnUnlock(string ID)
    {
        if(ID == this.GetType().ToString())
        {
            this.unlocked = true;
            if(AbilityIconFill!=null)
            {
                AbilityIconFill.SetActive(true);
            }
            else
            {
                Debug.Log("ability icon image not set in inspector");
            }

            this.PreDashSavePos = this.transform.position;
        }
    }

    void setupinput()
    {
       
        InputManager.inputManager.PInput.PlayerInputControls.Jump.performed += ctx =>
        {
            if (InputManager.GameIsPaused)
            {
                return;
            }
            StartCoroutine("Dashing");
        };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.Jump.performed += ctx =>
        {
            if(InputManager.GameIsPaused)
            {
                return;
            }
            StartCoroutine("Dashing");
        };
    }

  /*  IEnumerator Dashfunction()
    {
        if (cooldown == false && unlocked == true)
        {
            StartCoroutine(StopCollisions());
            cooldown = true;
            speedcheck = true;
            Vector2 direction = (InputManager.inputManager.attackDirection+InputManager.inputManager.LastMoveDirection).normalized;
            Anim.SetTrigger("Dash");
            Dashsound.Dashsfx();

            for (int i = 1; i < 3; i++)
            {
                dashvalue = i * Dashstrenght * direction;
                new WaitForFixedUpdate();
            }

            yield return new WaitForSeconds(0.1f);
            speedcheck = false;
            yield return new WaitForSeconds(dashcooldown);
            cooldown = false;

        }
        yield return null;
    }*/


    //fixed update for detecting fallzone
    private void FixedUpdate()
    {
        if (dashed == true )
            return;
       
        if (Physics2D.GetContacts(this.gameObject.GetComponent<Collider2D>(), FallingCollisionFilter, COLS) > 0)
        {
            this.gameObject.transform.position = PreDashSavePos;

            GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerDamage(.2f, this.gameObject);
            dashed = false;

        }
    }

    public bool dashed = false;
    RaycastHit2D rh;
    Vector2 dir = new Vector2();
    public ContactFilter2D FallingCollisionFilter;
    List<Collider2D> COLS = new List<Collider2D>();

    IEnumerator Dashing()
    {
        if(cooldown ==false && unlocked == true && PS.PlayerStateInfo.playerState != State.istalking)
        {
            PreDashSavePos = this.gameObject.transform.position;
            Anim.SetTrigger("Dash");
            if (AbilityIconFill!=null)
            { 
                StartCoroutine(DashIconImageFilling());
            }

            dashed = true;
            cooldown = true;
            InputManager.inputManager.StartVibing(0.1f, .1f, .15f);
            GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerLeap, .05f);
            
            this.gameObject.layer = DashLayer;
            
            rb.velocity = Vector2.zero;

            if (InputManager.inputManager.playerInput.magnitude > InputManager.inputManager.LastMoveDirection.magnitude)
            {
                dir = InputManager.inputManager.playerInput;
            }
            else 
            {
                dir = InputManager.inputManager.LastMoveDirection;
            }

            rh = Physics2D.Raycast(this.transform.position, dir, DashDistance, LM);

            DashToPos = (Vector2) this.transform.position + (dir * DashDistance);
            if(rh.collider!=null)
            {
                DashToPos = (Vector2)this.transform.position + (dir * (Vector2.Distance(this.transform.position, rh.point)-0.5f));
            }

            float Movedistance = Vector2.Distance((Vector2)this.transform.position, DashToPos);

            /* while (Vector2.Distance((Vector2)this.transform.position, DashToPos) > .5f)
             {
                 this.transform.position = Vector2.Lerp(this.transform.position, DashToPos, Time.deltaTime * DashSpeed);
                 GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.istalking);
                 yield return null;
             }*/


            //   GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.istalking);
            Instantiate(dust, gameObject.transform.position, Quaternion.Euler(DashToPos - (Vector2)PreDashSavePos));
            for (int i = 0; i < dashiteration; i++)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.transform.position = (Vector2)transform.position + ((DashToPos - (Vector2)PreDashSavePos) / dashiteration);
               
               
                yield return new WaitForSecondsRealtime(0.1f / (dashiteration));

            }

            Instantiate(dust, gameObject.transform.position, Quaternion.Euler(DashToPos - (Vector2)PreDashSavePos));
            this.gameObject.layer = RegularLayer;
            dashed = false;
            yield return new WaitForSeconds(0.1f);
            speedcheck = false;
            yield return new WaitForSecondsRealtime(dashcooldown);
            cooldown = false;
            
            yield break;
        }
        yield break;
    }

   /* public IEnumerator superDashing()
    {
        if (superdashcheck == true && unlocked == true && PS.PlayerStateInfo.playerState != State.istalking)
        {
            
            StopCoroutine("Dash");
            Anim.SetTrigger("superdash");
            if (AbilityIconFill != null)
            {
                StartCoroutine(DashIconImageFilling());
            }
            
            
            dashed = true;
            cooldown = true;
            InputManager.inputManager.StartVibing(0.1f, .1f, .15f);
            GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerLeap, .1f);
           
           
            // just to stop the player moving while doing dash
            
            this.gameObject.layer = DashLayer;
            PreDashSavePos = this.gameObject.transform.position;
            dir = (InputManager.inputManager.attackDirection + InputManager.inputManager.LastMoveDirection).normalized;
            rh = Physics2D.Raycast(this.transform.position, dir, DashDistance+2, LM);

            DashToPos = (Vector2)this.transform.position + (dir * (DashDistance+2));
            if (rh.collider != null)
            {
                DashToPos = (Vector2)this.transform.position + (dir * (Vector2.Distance(this.transform.position, rh.point) - 1f));
            }
            
            float Movedistance = Vector2.Distance((Vector2)this.transform.position, DashToPos);
            Instantiate(dust, gameObject.transform.position, Quaternion.Euler(DashToPos - (Vector2)PreDashSavePos));
            for (int i = 0; i < dashiteration; i++)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.transform.position = (Vector2)transform.position + ((DashToPos - (Vector2)PreDashSavePos) / dashiteration);
                Instantiate(dust, gameObject.transform.position, Quaternion.Euler(DashToPos - (Vector2)PreDashSavePos));
                foreach (Collider2D target in box.targets)
                {

                    target.gameObject.GetComponent<EnemyStats>().TakeDamage(10,heavy);

                }
                yield return new WaitForSecondsRealtime(0.6f / (dashiteration));

            }
            Instantiate(dust, gameObject.transform.position, Quaternion.Euler(DashToPos - (Vector2)PreDashSavePos));
            this.gameObject.layer = RegularLayer;
           
            yield return new WaitForSeconds(0.1f);
            speedcheck = false;
            yield return new WaitForSecondsRealtime(dashcooldown);
            superdashcheck = true;
            cooldown = false;
            
            yield break;
        }
        yield break;
    }*/

    IEnumerator DashIconImageFilling()
    {
        float timer = 0;
        while(timer<dashcooldown)
        {
            AbilityIconFill.GetComponent<Image>().fillAmount = timer / dashcooldown;
            timer += Time.deltaTime;
            yield return null;
        }
        AbilityIconFill.GetComponent<Image>().fillAmount = 1f;
        superdashcheck = true;
        yield break;
    }

    IEnumerator StopCollisions()
    {
        this.gameObject.layer = DashLayer;
        yield return new WaitForSeconds(0.2f);
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.isImmune);
        yield return new WaitForSeconds(.3f);
        this.gameObject.layer = RegularLayer;
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.isIdle);
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.DrawWireSphere(DashToPos, 1f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(PreDashSavePos, 0.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(rh.point, .5f);
            //Gizmos.DrawLine(this.transform.position, (Vector2) this.transform.position + dir * DashDistance);
            Gizmos.DrawRay(this.transform.position, dir * DashDistance);
        }
    }
}
