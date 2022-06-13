using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLeap : BaseInteractable
{
    //is the leap object itself
   [SerializeField] Transform destination;
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player = other.gameObject;
        }
        if (other.gameObject == Player && Player.GetComponent<PlayerStats>().canleap == true)
        {
            Caninteract = true;
            if (interact == true)//player has made the input to interact, (in this case jump)
            {
                Player.GetComponent<PlayerLeap>().StartCoroutine("Leap", destination);//activates co routine from player leap script

            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Caninteract = false;
    }

}
