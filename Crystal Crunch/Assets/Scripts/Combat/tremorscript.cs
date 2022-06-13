using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tremorscript : BaseAttack
{
   
    public void destroyobject()
    {
        Destroy(gameObject);
    }

    public void AttackLogic()
    {
        foreach (Collider2D target in this.gameObject.GetComponent<HitBox>().targets)
        {
            Debug.Log("heya");
            target.gameObject.GetComponent<IEnemyDamageable<float, BaseAttack>>().TakeDamage(this.DamageAmount, this);


            Vector2 dir = new Vector2();
            dir = target.transform.position - this.gameObject.transform.position;
        }
    }
}
