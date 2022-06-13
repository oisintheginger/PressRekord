using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.ParticleSystemJobs;

public enum BaseInput
{
    Null,
    Light,
    Heavy,
    RightHook,
    Pushback
}

[System.Serializable]
public struct ComboInput
{
    public BaseInput cInput;
    public float inTime;
    public float gapFromPrevious;
}
public class AttackManager2D : MonoBehaviour
{
    public static AttackManager2D attackManager;

    PlayerStats PS;

    [Header("Combo Timing")]
    public float attackCooldown;
    public float ComboDeadTimer;
    public float InputDeadTimer;
    float InputQueueTimer; // this is the timer float. When the player gives an input, this is reset to the resetTime value. The clearInputQueue (run through Update()) checks this value, if it is less than 0 it clears the input queue 
    [SerializeField] float resetTime; // this value is set in the editor.
    [SerializeField] float timecheck;
    [SerializeField] Queue<ComboInput> ComboInputs;
    [Header("Attack Ready")]
    [SerializeField] bool Ready = true;
    bool nextattack = true;
    ParticleSystem part;

    #region Lists for inputs
    #endregion

    public Animator PlayerAnim;

    [SerializeField] float LightAttackCooldown = .3f;
    private bool LightAttackReady = true;
    bool altLightAttackReady = true;
    bool finalattackready = true;
    bool heavystart = true;
    
    private void Awake()
    {
        if (attackManager != this)
        {
            attackManager = this;
        }
        PlayerAnim = this.gameObject.GetComponent<Animator>();

        AttackManager2D.attackManager.Ready = true;

        PS = this.gameObject.GetComponent<PlayerStats>();
        part = GetComponentInChildren<ParticleSystem>();
        //ComboInputs = new Queue<ComboInput>();
        //inputTests = new List<ComboInput>();
    }

    private void Start()
    {
        SetupInput();
    }

    //[SerializeField] int TESTQUEUECOUNT;
    //[SerializeField] List<ComboInput> inputTests;
    public void SetupInput()
    {
        LightAttackSetup();
        HeavyAttackSetup();
        
        
    }

    void heavyinvoke()
    {
        PlayerAnim.SetTrigger("WindUp");
        PlayerAnim.SetBool("WindingUp", true);
    }

    void HeavyAttackSetup()
    {
        InputManager.inputManager.PInput.PlayerInputControls.HeavyAttack.started +=
             ctx =>
             {

                 if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting || heavystart == false)
                 {
                     return;
                 }

                //PlayerAnim.SetTrigger("WindUp");
                //PlayerAnim.SetBool("WindingUp", true); //enter the windup state
                Invoke("heavyinvoke", 0.3f);
             };

        InputManager.inputManager.PInput.PlayerInputControls.HeavyAttack.performed +=
            ctx =>
            {
                if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting)
                {
                    return;
                }

                //GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerHeavyAttack, 0.1f);
                PlayerAnim.SetTrigger("HeavyAttack"); //trigger attack state
                PlayerAnim.SetBool("WindingUp", false);

            };

        InputManager.inputManager.PInput.PlayerInputControls.HeavyAttack.canceled +=
            ctx =>
            {
                if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting)
                {
                    return;
                }
                CancelInvoke("heavyinvoke");
                PlayerAnim.ResetTrigger("HeavyAttack");
                PlayerAnim.SetBool("WindingUp", false);
                part.Stop();
                //PlayerAnim.SetTrigger("CancelAttack"); //trigger attack state

            };
        ///////////////////////////////////////////////////////////////////////////////
        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.HeavyAttack.started +=
            ctx =>
            {

                if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting || heavystart==false)
                {
                    return;
                }

                //PlayerAnim.SetTrigger("WindUp");
                //PlayerAnim.SetBool("WindingUp", true); //enter the windup state
                Invoke("heavyinvoke", 0.3f);
            };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.HeavyAttack.performed +=
            ctx =>
            {
                if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting)
                {
                    return;
                }

                //GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerHeavyAttack, 0.1f);
                PlayerAnim.SetTrigger("HeavyAttack"); //trigger attack state
                PlayerAnim.SetBool("WindingUp", false);
                
            };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.HeavyAttack.canceled +=
            ctx =>
            {
                if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting)
                {
                    return;
                }
                CancelInvoke("heavyinvoke");
                PlayerAnim.ResetTrigger("HeavyAttack");
                PlayerAnim.SetBool("WindingUp", false);
                part.Stop();
                //PlayerAnim.SetTrigger("CancelAttack"); //trigger attack state

            };
    }



    void LightAttackSetup()
    {
        InputManager.inputManager.PInput.PlayerInputControls.LightAttack.performed +=
      ctx =>
      {

          if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting || LightAttackReady == false)
          {
              return;
          }



          PlayerAnim.SetTrigger("LightAttack");

          StartCoroutine(LightCooldown());
      };



        InputManager.inputManager.PInput.PlayerInputControls.LightAttackAlt.performed +=
         ctx =>
         {
             if ( PS.PlayerStateInfo.playerState == State.isStunned ||  altLightAttackReady == false || LightAttackReady == true)
             {
                 return;
             }
            //GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerLightAttack, 0.1f);
            //StartCoroutine(LightCooldown());
            //altLightAttackReady = false;
            PlayerAnim.ResetTrigger("LightAttack");
             PlayerAnim.SetBool("inchain", true);

             PlayerAnim.SetTrigger("LightAttackAlt");
         };

        InputManager.inputManager.PInput.PlayerInputControls.LightAttackAlt.canceled +=
        ctx =>
        {
            altLightAttackReady = false;
            StartCoroutine(LightCooldown());

        };

        InputManager.inputManager.PInput.PlayerInputControls.finallightattack.performed +=
    ctx =>
    {
        if ( PS.PlayerStateInfo.playerState == State.isStunned ||  finalattackready == false || altLightAttackReady == true)
        {
            return;
        }
            //GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerLightAttack, 0.1f);
            //StartCoroutine(LightCooldown());
            //finalattackready = false;
            PlayerAnim.ResetTrigger("LightAttack");
        PlayerAnim.SetBool("inchain", true);

        PlayerAnim.SetTrigger("FinalLightAttack");
    };

        InputManager.inputManager.PInput.PlayerInputControls.finallightattack.canceled +=
        ctx =>
        {
            finalattackready = false;
            StartCoroutine(LightCooldown());

        };


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.LightAttack.performed +=
        ctx =>
        {
            
            if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting || LightAttackReady == false)
            {
                return;
            }


          
            PlayerAnim.SetTrigger("LightAttack");
           
            StartCoroutine(LightCooldown());
        };

     

       InputManager.inputManager.PInput.PlayerInputControlsKeyboard.LightAttackAlt.performed +=
        ctx =>
        {
            if ( PS.PlayerStateInfo.playerState == State.isStunned ||  altLightAttackReady == false || LightAttackReady==true)
            {
                return;
            }
            //GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerLightAttack, 0.1f);
            //StartCoroutine(LightCooldown());
            //altLightAttackReady = false;
           
            PlayerAnim.ResetTrigger("LightAttack");
            PlayerAnim.SetBool("inchain", true);
           
            PlayerAnim.SetTrigger("LightAttackAlt");
        };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.LightAttackAlt.canceled +=
        ctx =>
        {
            
            if (timecheck != 0)
            {
                altLightAttackReady = false;
                StartCoroutine(LightCooldown());
            }

        };

            InputManager.inputManager.PInput.PlayerInputControlsKeyboard.finallightattack.performed +=
        ctx =>
        {
            if ( PS.PlayerStateInfo.playerState == State.isStunned ||  finalattackready == false ||  altLightAttackReady==true)
            {
                return;
            }
            //GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerLightAttack, 0.1f);
            //StartCoroutine(LightCooldown());
            //finalattackready = false;
            
            PlayerAnim.ResetTrigger("LightAttack");
            PlayerAnim.SetBool("inchain", true);
           
            PlayerAnim.SetTrigger("FinalLightAttack");
        };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.finallightattack.canceled +=
        ctx =>
        {
            
            if (timecheck != 0)
            {
                finalattackready = false;
                StartCoroutine(LightCooldown());
            }
           

        };

    }

   public void triggerreset()
    {
        timecheck = -.3f;
        
    }

   

    public void lightcheck()
    {
        LightAttackReady = false;
    }

    public void lightaltcheck()
    {
       altLightAttackReady = false;
    }

    public void finallightcheck()
    {
        finalattackready = false;
        heavystart = false;
    }

    public void statusreset()
    {
        PlayerAnim.ResetTrigger("LightAttackAlt");
        PlayerAnim.ResetTrigger("FinalLightAttack");
        PlayerAnim.ResetTrigger("LightAttack");
        part.Stop();
    }

    

 IEnumerator LightCooldown()
 {
        if (Ready == true)
        {
            Ready = false;
            while (timecheck <= LightAttackCooldown)
            {
                timecheck = timecheck + Time.deltaTime;
                //yield return new WaitForEndOfFrame();
                yield return null;
            }
            

            PlayerAnim.ResetTrigger("LightAttackAlt");
            PlayerAnim.ResetTrigger("FinalLightAttack");
            PlayerAnim.ResetTrigger("LightAttack");
            PlayerAnim.SetBool("inchain", false);
            heavystart = true;
            altLightAttackReady = true;
            finalattackready = true;
            LightAttackReady = true;
            Ready = true;
            heavystart = true;
            timecheck = 0;
            yield break;
        }
        yield break;
 }

    #region Old Combo System Stuff
    private void Update()
    {
       // clearInputQueue();
       // TESTQUEUECOUNT = ComboInputs.Count;
      //  inputTests = new List<ComboInput>(ComboInputs);
        

    }
    void TriggerAttack(BaseInput inter)
    {
        switch (inter)
        {
            case BaseInput.Heavy:
                    PlayerAnim.SetTrigger("HeavyAttack");
                break;
            case BaseInput.Light:
                    PlayerAnim.SetTrigger("LightAttack");
                break;
        }
    }

    #region
    /*
    bool checkComboMatch(List<BaseInput> userInput, List<BaseInput> comboList)
    {
        if (userInput.Count > comboList.Count)
        {
            return false;
        }
        for(int i = 0; i<comboList.Count; i++)
        {
            if (userInput[i] != comboList[i])
                return false;
        }
        return true;
    } //Checking whether the input queue sequence matches any combo input sequence, returns true for first sequence found

    bool CheckTime(ComboInput[] CIArray)
    {

        for (int i = 0; i < CIArray.Length; i++)
        {
            if (i > 0)
            {
                CIArray[i].gapFromPrevious = (CIArray[i].inTime - CIArray[i - 1].inTime);

                if(CIArray[i].gapFromPrevious>ComboDeadTimer)
                {
                    return false;
                }
            }

        }
        return true;
    } //checking if the time between inputs within the input queue are within the dead time, if they are, returns false

    void clearInputQueue() //clears the input queue if the timer goes over.
    {
        if(InputQueueTimer >0)
        {
            InputQueueTimer -= Time.deltaTime;
        }
        
        if(InputQueueTimer <= 0)
        {
            ComboInputs.Clear();
        }
    }
    */
    #endregion

    float previousInputTime = 0f;
    
    void ManageInput(BaseInput playerIn)
    {
        if(PS.PlayerStateInfo.playerState== State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting)
        {
            return;
        }

        if(Time.time - previousInputTime < InputDeadTimer)
        {
            return;
        }

        previousInputTime = Time.time;
        #region
        /*
        InputQueueTimer = resetTime;
        ComboInput CI = new ComboInput();
        CI.cInput = playerIn;
        CI.inTime = Time.time;
        ComboInputs.Enqueue(CI);

        if (ComboInputs.Count > 3)
        {
            ComboInputs.Dequeue();
        }

        ComboInput[] TempArray = ComboInputs.ToArray();
        if(TempArray.Length>2)
        {
            foreach(ComboBase COMBO in this.gameObject.GetComponents<ComboBase>())
            {
                if (COMBO.unlocked == true)
                {
                    List<BaseInput> conversionToInputs = new List<BaseInput>();
                    foreach (ComboInput ci in TempArray)
                    {
                        conversionToInputs.Add(ci.cInput);
                    }

                    bool sequenceMatch = checkComboMatch(conversionToInputs, COMBO.comboID.myInputCombo);
                    if (sequenceMatch)
                    {
                        bool timeMatch = CheckTime(TempArray);
                        if (timeMatch)
                        {

                            playerIn = COMBO.comboID.myInputType;
                            ComboInputs.Clear(); //clear after succesfull combo

                        }
                        else
                        {
                            Debug.Log("Failed time: " + (Time.time - CI.inTime));
                            ComboInputs.Clear(); //clear for reset
                        }

                    }
                }
            }
        }*/
        #endregion
        TriggerAttack(playerIn);
    }
    #endregion

}
