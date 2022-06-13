using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHook : ComboBase, IAbilityUnlock
{
    public float AttackForce;
    bool attackcheck = false;
    
    private void Awake()
    {
        this.comboID.myInputType = BaseInput.RightHook;
    }

    void Start()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerUnlockAbilityEvent += OnUnlock;
    }

    public void doRighthook()
    {
        AttackLogic();
        
    }

    public void doRightHookCameraShake()
    {
       // GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerSpecialAttack, 0.3f);
    }

    public void doRightHookControllerVibration()
    {
        InputManager.inputManager.StartVibing(1f, 1f, 0.3f);
    }
    private void AttackLogic()
    {
        foreach (Collider2D target in this.gameObject.GetComponent<HitBox>().targets)
        {
            target.gameObject.GetComponent<Rigidbody2D>().velocity = (target.gameObject.GetComponent<Rigidbody2D>().position - (Vector2)this.transform.position).normalized * AttackForce;
            target.gameObject.GetComponent<EnemyStats>().TakeDamage(this.DamageAmount, null,target.gameObject);
            target.gameObject.GetComponent<EnemyStats>().StartCoroutine("Stunduration"); // stuns enemy after attack
            
          //  StartCoroutine(AttackEffects(target.gameObject.GetComponent<Rigidbody2D>()));
          
        }
    }


    IEnumerator AttackEffects(Rigidbody2D Enemyrb)
    {
        if (attackcheck == false)
        {
            attackcheck = true;
           
            Vector2 playpos = new Vector2(transform.position.x, transform.position.y);
            for (int i = 0; i <= 3; i++)
            {
              
                Enemyrb.velocity = Vector2.up * AttackForce;

            }
            yield return new WaitForSeconds(0.2f);
            Enemyrb.velocity = Vector2.zero;
            attackcheck = false;
        }
        yield return null;
    }

    public void OnUnlock(string componentName)
    {
        Debug.Log("Unlocked " + componentName);
        if(componentName == this.GetType().ToString())
        {
            this.unlocked = true;
        }
    }
}
