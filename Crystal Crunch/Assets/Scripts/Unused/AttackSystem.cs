using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class AttackSystem : MonoBehaviour
{
    struct Combos
    {
        public int inp1;
        public int inp2;
        public int inp3;
        public String ComboName;

        public Combos(int inp1, int inp2, int inp3, string ComboName)
        {
            this.inp1 = inp1;
            this.inp2 = inp2;
            this.inp3 = inp3;
            this.ComboName = ComboName;
        }
    }

    [SerializeField] Animator spriteAnim;
    Animator PlayerAnim;
    public int Input, InputCounter;// the input itself and counts which input
    public float DeadTimeTimer = 0;
    public string AttackName; // names the attacks that are being done
    bool AttackReady; // checks wether another attack can be done
    Collider Hitbox;
    [SerializeField] int[] Combo = new int[3] { 0, 0, 0 }; // the players input
    List<Combos> ComboList = new List<Combos>();

    // were the inputs will be placed for the combolist
    void Awake()
    {
       
        ComboList.Add(new Combos(1, 1, 2, "Righthook"));
        ComboList.Add(new Combos(1, 1, 1, "Pushback"));
        PlayerAnim = this.GetComponent<Animator>();
    }

    void Start()
    {
        StartInput();
        Input = 0;
        InputCounter = 0;
        AttackReady = true;

    }
    
    public void StartInput()
    {
        InputManager.inputManager.PInput.PlayerInputControls.LightAttack.performed += ctx => StartCoroutine("buttonInput");
        InputManager.inputManager.PInput.PlayerInputControls.HeavyAttack.performed += ctx => StartCoroutine("buttonInput");
    }

    void TriggerAttack(string Trigger)
    {
        //PlayerAnim.SetTrigger(Trigger);
        spriteAnim.SetTrigger(Trigger);
    }


    IEnumerator buttonInput()
    {
        if (this.gameObject.GetComponent<PlayerStats>().isTalking)
        {
            yield break;
        }
        //Skips coroutine if attack is on cooldown
        if (AttackReady == true)
        {
            AttackReady = false;

            //acts as input delay
            //yield return new WaitForSecondsRealtime(4 / 60f);

            //decides wether input is a light attack or heavy attack
            AttackName = Input == 1 ? "LightAttack" : "HeavyAttack";

            //rests the inputs after doing too many (in this case going over 3)
            if (InputCounter >= 3)
            {
                Combo = new int[3] { 0, 0, 0 };
                InputCounter = 0;
            }

            //adds inputs to the combo array
            Combo[InputCounter] = Input;

            //tells what number input we are on
            InputCounter++;

            // checks if the player has input a combo thats in the combolist
            for (int i = 0; i < ComboList.Count; i++)
            {
                int[] testcombo = new int[3] { ComboList[i].inp1, ComboList[i].inp2, ComboList[i].inp3 };

                if (testcombo.SequenceEqual(Combo)) //compares combo to the combos in the combo list (wether they are the same or not)
                {
                    AttackName = ComboList[i].ComboName;
                    break;
                }
            }

            
            TriggerAttack(AttackName); // IMPORTANT This is where the trigger is set
            yield return new WaitForSecondsRealtime(2/ 60);
            Debug.Log(AttackName);
           // Hitbox.enabled = false;
            DeadTimeTimer = 0.8f;
            AttackReady = true;

            //this while loops acts as a deadtime counter for when inputs are not being put in
            while (DeadTimeTimer > 0 && DeadTimeTimer < 0.9f)
            {
                DeadTimeTimer -= Time.deltaTime;
                yield return null;
            }

            //resets the inputs after the deadtime counter goes over 0.8 seconds
            if (DeadTimeTimer <= 0)
            {
                DeadTimeTimer = 0;
                Combo = new int[3] { 0, 0, 0 };
                InputCounter = 0;

            }

        }

    }
}
