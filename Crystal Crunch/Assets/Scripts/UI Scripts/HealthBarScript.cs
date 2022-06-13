using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    Animator anim;
    private void Start()
    {
        GameEventsSystem.gameEventsSystem.UIEvents.UpdatePlayerHealthBar += SetFill;
        anim = GetComponent<Animator>();
    }

    void SetFill(float FILLVALUE)
    {

        this.GetComponent<Image>().fillAmount = FILLVALUE;
        anim.SetBool("hit", true);
        Invoke("healthcheck", 1f);
    }

    void healthcheck()
    {
        if (this.GetComponent<Image>().fillAmount > 0.25)
        {
            anim.SetBool("hit",false);
        } 
    }
}
