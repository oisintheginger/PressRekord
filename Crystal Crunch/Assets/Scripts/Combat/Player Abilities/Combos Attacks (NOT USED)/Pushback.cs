using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushback : ComboBase, IAbilityUnlock
{
    private void Awake()
    {
        this.comboID.myInputType = BaseInput.Pushback;
        
    }

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerUnlockAbilityEvent += OnUnlock;
    }

    public float AttackForce;
    bool attackcheck = false;
    public void doPushback()
    {
        AttackLogic();
    }

    public void doPushbackCameraShake()
    {
       // GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerSpecialAttack, 0.3f);
    }

    public void doPushbackControllerVibration()
    {
        InputManager.inputManager.StartVibing(1f, 1f, 0.3f);
    }

    private void AttackLogic()
    {
        Debug.Log("Pushback");
        foreach (Collider2D target in this.gameObject.GetComponent<HitBox>().targets)
        {
            //StartCoroutine("AttackEffects", target.gameObject.GetComponent<Rigidbody2D>());
            target.gameObject.GetComponent<Rigidbody2D>().velocity = (target.gameObject.GetComponent<Rigidbody2D>().position - (Vector2)this.transform.position).normalized * AttackForce;
            target.gameObject.GetComponent<EnemyStats>().TakeDamage(this.DamageAmount, null, target.gameObject);
            target.gameObject.GetComponent<EnemyStats>().StartCoroutine("Stunduration",3f); // stuns enemy after attack
        }
    }

    IEnumerator AttackEffects(Rigidbody2D Enemyrb)
    {
        if (attackcheck == false)
        {
            Vector2 playpos = new Vector2(transform.position.x, transform.position.y);
            attackcheck = true;
            for (int i = 0; i <= 3; i++)
            {
                Enemyrb.velocity = (Enemyrb.position-playpos).normalized  * AttackForce;

            }
            yield return new WaitForSeconds(0.2f);
            Enemyrb.velocity = Vector2.zero;
            attackcheck = false;
        }
        yield return null;
    }

    public void OnUnlock(string componentName)
    {
        if (componentName == this.GetType().ToString())
        {
            this.unlocked = true;
        }
    }
}
