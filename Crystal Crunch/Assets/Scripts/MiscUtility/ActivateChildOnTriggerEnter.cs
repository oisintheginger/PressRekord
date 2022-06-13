using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActivateChildOnTriggerEnter : MonoBehaviour
{
    [SerializeField] GameObject Child;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Child!=null)
        {
            Child.SetActive(true);
        }
    }
}
