using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] string trigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Anim!=null)
        { 
            Anim.SetTrigger(trigger);
        }
    }
}
