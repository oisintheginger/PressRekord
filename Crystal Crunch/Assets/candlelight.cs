using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candlelight : EnemyStats
{
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(float damageTaken, BaseAttack incomingAttackType, GameObject gameObject = null)
    {
        myStats.Health -= damageTaken;
        

        if (myStats.Health <= 0)
        {

            anim.SetTrigger("lightsout");
            myStats.Health = myStats.MaxHealth;

        }

    }
}
