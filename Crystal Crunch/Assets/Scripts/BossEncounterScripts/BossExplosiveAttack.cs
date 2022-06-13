using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExplosiveAttack : MonoBehaviour
{
    [SerializeField] LayerMask PlayerLayer;
    [SerializeField] float AttackRadius = 1f;
    [SerializeField] float Damage = .6f;
    [SerializeField] float PushbackForce = 1f;

    bool attacked = false;
    public void AttackLogic()
    {
        Collider2D Cols = Physics2D.OverlapCircle(this.transform.position, AttackRadius, PlayerLayer);
           
        if(Cols != null && attacked ==false)
        {
            Debug.Log("CountedPlayer");
            GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerDamage(Damage, Cols.gameObject);

            GameObject PLRef = Cols.gameObject;

            Vector2 Force = (PLRef.transform.position - this.transform.position).normalized * PushbackForce;
            PLRef.GetComponent<Rigidbody2D>().velocity = Force;
            attacked = true;
        }
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
