using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerConverstation : MonoBehaviour
{
    public LayerMask lm;
    public DialogueManager dm;
    public DialogueObject dialogueToStart;
    public bool isOnceOff = false;
    private void Awake()
    {
        this.GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(isOnceOff)
            {
                this.gameObject.SetActive(false);
            }
            GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(dialogueToStart);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameEventsSystem.gameEventsSystem.DialEvents.OnEndDialogue();
        }
    }


}


