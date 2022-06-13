using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPotion : MonoBehaviour
{
    public float amountToHeal = 10f;
 
   
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerHeal(amountToHeal);
            GameEventsSystem.gameEventsSystem.UIEvents.OnUpdatePlayerHealthBar(PlayerStats._CurrentPlayerStats.playerStats.Health / PlayerStats._CurrentPlayerStats.playerStats.MaxHealth);
            Destroy(gameObject, 0.2f);
            Debug.Log("yeeha");
        }
    }

}