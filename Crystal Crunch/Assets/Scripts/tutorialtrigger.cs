using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tutorialtrigger : MonoBehaviour
{
   [SerializeField] Image spritetutorial;
    FMOD.Studio.EventInstance tutorialssound;
    bool iconcheck = true;

     void Start()
    {
        tutorialssound = FMODUnity.RuntimeManager.CreateInstance("event:/triggernotice");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            iconcheck = true;
            if (iconcheck == true)
            {
                GetComponent<SpriteRenderer>().enabled = false;
            }
            tutorialssound.start();
            spritetutorial.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spritetutorial.enabled = false;
        }
    }
}
