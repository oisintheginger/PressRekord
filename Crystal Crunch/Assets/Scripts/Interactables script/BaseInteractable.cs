using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    public GameObject Player;
    public Controls Interactableinput;
    public bool interact;
    public bool Caninteract=false;
    public void Awake()
    {
        registerinput();
    }
    public void OnEnable()
    {
        Interactableinput.Enable();
    }
    public void OnDisable()
    {
        Interactableinput.Disable();
    }

    void registerinput()//to interact with object
    {
        Interactableinput = new Controls();
        Interactableinput.PlayerInputControls.Jump.performed += ctx => interact = Caninteract==true?true:false;//checks wether player is on a interactable object
        Interactableinput.PlayerInputControls.Jump.canceled += ctx => interact = false;
    }

}
