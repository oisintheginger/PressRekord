using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sludge : MonoBehaviour
{
    bool damagechecck=true;
    Collider2D col;
   [SerializeField] int damage = 1;
    public void Start()
    {
        col = GetComponent<Collider2D>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (col.enabled == true)
        {
            if (collision.gameObject.CompareTag("Player") && collision.gameObject.layer == 9 && damagechecck == true)
            {
                GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerDamage(damage, collision.gameObject);
                damagechecck = false;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            damagechecck = true;
        }
    }
}
