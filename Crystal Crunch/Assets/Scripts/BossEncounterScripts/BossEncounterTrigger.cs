using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BossEncounterTrigger : MonoBehaviour
{
    public UnityEvent TriggerFunction;
    public BossAI BossEntity;

    public bool Triggered = false;

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent += DeactivateTrigger;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Triggered == false)
        {
            TriggerFunction.Invoke();
            Triggered = true;
        }
    }

    void DeactivateTrigger()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("Reactivate", 3f);
    }
    
    void Reactivate()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
}