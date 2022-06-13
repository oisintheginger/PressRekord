using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    Rigidbody2D myRb;
    public Controls playerController;
    public Vector2 vel;
    private Vector2  dVel;
    //[SerializeField] bool Grounded;
    
    public float maxSpeed, maxAcc;
    
    //Dictionary<Vector2, int> angleRotation = new Dictionary<Vector2, int>();
    //Dash dashcomp;
    //Quaternion newRot;
    //int currentRotation = 0;
    //bool dash;
    //public bool dashUnlocked;

    [SerializeField] int GapLayerIndex = 18;

    PlayerStats PS;
    Animator spriteAnim;

    void Awake()
    {
        myRb = GetComponent<Rigidbody2D>();
        spriteAnim = this.GetComponent<Animator>();
        //dashcomp = GetComponent<Dash>();
        /*dash = true;
        angleRotation.Add(new Vector2(0, 1), 0);
        angleRotation.Add(new Vector2(1, 1), 0);
        angleRotation.Add(new Vector2(-1, 1), 0);
        angleRotation.Add(new Vector2(0, -1), 180);
        angleRotation.Add(new Vector2(1, -1), 180);
        angleRotation.Add(new Vector2(-1, -1), 180);
        angleRotation.Add(new Vector2(1, 0), 90);
        angleRotation.Add(new Vector2(-1, 0), -90);
        angleRotation.Add(new Vector2(0, 0), currentRotation);*/
        PS = this.GetComponent<PlayerStats>();
    }
   


    [SerializeField] Collider2D[] COLS;
    void Update()
    {
        if (InputManager.PlayerInDialogue == true)
        {
            spriteAnim.SetFloat("AnimXMove", 0f);
            spriteAnim.SetFloat("AnimYMove", 0f);
            spriteAnim.SetFloat("LastAnimXMove", 0f);
            spriteAnim.SetFloat("LastAnimYMove", 0f);
            spriteAnim.SetFloat("AnimMoveSpeed", 0f);
            vel = Vector2.zero;
            dVel = Vector2.zero;
        }
        if (PS.PlayerStateInfo.playerState!=State.istalking)
        {
            dVel = new Vector2(InputManager.inputManager.playerInput.x, InputManager.inputManager.playerInput.y) * maxSpeed;
            //playerRotationCalculate();
           
        }

    }

    void FixedUpdate()
    {
        if(InputManager.GameIsPaused)
        {
            return;
        }
        CalculateNewVelocity();
        if (this.GetComponent<Dash>().dashed == true)
        {
            vel = Vector2.zero;
            return;
        }
        AnimateSprite();
        if (PS.PlayerStateInfo.playerState != State.istalking)
        {
            myRb.velocity = vel;
        }
        else 
        {
            myRb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == GapLayerIndex)
        {
           // GameEventsSystem.gameEventsSystem.AreaEvents.OnRestartScene(ActiveSceneManager._Current.CurrentlyLoadedScene);
        }
    }

    
    Vector2 VectorDirectionCorrecter(Vector2 InputToCorrect)
    {
        Vector2 returnVector = InputToCorrect;
        float x = Mathf.Abs(returnVector.x) * 500f;
        float y = Mathf.Abs(returnVector.y) * 500f;

        if (InputToCorrect.magnitude < 0.3f)
        {
            return returnVector;
        }
        if (x > y)
        {
            returnVector.y = 0;
            returnVector.x = returnVector.x / Mathf.Abs(returnVector.x);
        }
        else if (y >= x + Mathf.Epsilon)
        {
            returnVector.x = 0;
            returnVector.y = returnVector.y / Mathf.Abs(returnVector.y);
        }

        return returnVector;
    }

    void AnimateSprite()
    {
        Vector2 SpriteAnimVector = new Vector2();

        if(Mathf.Abs(InputManager.inputManager.playerInput.x)>= Mathf.Abs(InputManager.inputManager.playerInput.y) )
        {
            SpriteAnimVector.x = InputManager.inputManager.playerInput.x;
            SpriteAnimVector.y = 0;
        }
        else
        {
            SpriteAnimVector.y = InputManager.inputManager.playerInput.y;
            SpriteAnimVector.x = 0;
        }


        spriteAnim.SetFloat("AnimXMove", SpriteAnimVector.x);
        spriteAnim.SetFloat("AnimYMove", SpriteAnimVector.y);

        if ((InputManager.inputManager.playerInput.x != 0 ||  InputManager.inputManager.playerInput.y != 0) && (Mathf.Abs(vel.x) > 0 || Mathf.Abs(vel.y) > 0))
        {
            InputManager.inputManager.LastMoveDirection.x = vel.x;
            InputManager.inputManager.LastMoveDirection.y = vel.y;
        }
        InputManager.inputManager.LastMoveDirection.Normalize();

        Vector2 lastMoveSpriteDir = VectorDirectionCorrecter(InputManager.inputManager.LastMoveDirection);

        InputManager.inputManager.attackDirection = lastMoveSpriteDir;
        spriteAnim.SetFloat("LastAnimXMove", lastMoveSpriteDir.x);
        spriteAnim.SetFloat("LastAnimYMove", lastMoveSpriteDir.y);

        spriteAnim.SetFloat("AnimMoveSpeed", vel.magnitude);
    }

    private void CalculateNewVelocity()
    {
        float currentVelX = myRb.velocity.x;
        float currentVelY = myRb.velocity.y;
        
        float maximumSpeedIncrease = maxAcc * Time.fixedDeltaTime;
        float newVelX = Mathf.MoveTowards(currentVelX, dVel.x, maximumSpeedIncrease);
        float newVelY = Mathf.MoveTowards(currentVelY, dVel.y, maximumSpeedIncrease);
        
        vel = Vector2.right * (newVelX) + Vector2.up * (newVelY);

    }

    /* IEnumerator Dash()
    {
        if (dashUnlocked == true && dash == true && GetComponent<PlayerStats>().interactable == false)
        {
            dash = false;

            spriteAnim.SetTrigger("Leap");

            for (int i = 1; i < 3; i++)
            {
                myRb.AddForce((Vector2.right * InputManager.inputManager.playerInput.x + Vector2.up * InputManager.inputManager.playerInput.y).normalized * (jumpForce / i), ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(0.75f);

            spriteAnim.SetTrigger("Land");

            dash = true;
        }
    }*/


   /* public void StartInput()
    {
        InputManager.inputManager.PInput.PlayerInputControls.Jump.performed += context => StartCoroutine("Dash");
    }*/

   /* private void playerRotationCalculate()
    {
        newRot = Quaternion.Euler(new Vector3(0, angleRotation[new Vector2(Mathf.RoundToInt(InputManager.inputManager.playerInput.x), Mathf.RoundToInt(InputManager.inputManager.playerInput.y))], 0));//calculatues the angle and convertes to a quaternion
        currentRotation = angleRotation[new Vector2(Mathf.RoundToInt(InputManager.inputManager.playerInput.x), Mathf.RoundToInt(InputManager.inputManager.playerInput.y))];//updates last used angle as the default value for when the player stops moving
        angleRotation[new Vector2(0, 0)] = currentRotation;
    }*/
}
