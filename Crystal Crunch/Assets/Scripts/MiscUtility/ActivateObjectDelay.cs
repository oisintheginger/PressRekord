using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectDelay : MonoBehaviour
{
    [SerializeField] GameObject ObjectToActivate;
    [SerializeField] float Delay = 4f;
    private void Awake()
    {
        Invoke("Activate", Delay);
    }

    void Activate()
    {
        ObjectToActivate.SetActive(true);
    }
}
