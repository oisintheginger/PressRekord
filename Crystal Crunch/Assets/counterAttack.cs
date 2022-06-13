using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counterAttack : BaseAttack
{
    Collider2D col;
    public bool counter;
    [SerializeField]GameObject player;
    LightAttack attacktype;
    [SerializeField] int RatDashLayer = 21, FlyProjectileLayer = 3;
    public void Start()
    {
        col = GetComponent<Collider2D>();
        //player = GetComponentInParent<GameObject>();
        player = this.transform.parent.gameObject;
        attacktype = player.GetComponent<LightAttack>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (col.isActiveAndEnabled )
        {
           if( collision.gameObject.layer == RatDashLayer)
            {
                if (counter == true)
                {
                    collision.gameObject.GetComponent<IEnemyDamageable<float, BaseAttack>>().TakeDamage(0, this);
                }

                if (counter == false)
                {
                   
                    collision.gameObject.GetComponent<IEnemyDamageable<float, BaseAttack>>().TakeDamage(0, attacktype);
                }
                return;
            }

            if (collision.gameObject.layer == FlyProjectileLayer)
            {
                if (counter == true)
                {
                    collision.GetComponent<FlyEnemyProjectile>().countered = true;
                    collision.GetComponent<FlyEnemyProjectile>().pushback(20, transform.position);
                }

                if (counter == false)
                {
                    collision.GetComponent<FlyEnemyProjectile>().countered = true;
                    collision.GetComponent<FlyEnemyProjectile>().pushback(1, transform.position);
                    collision.GetComponent<FlyEnemyProjectile>().splash();
                }
            }
        }
    }
}
