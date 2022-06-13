using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIsfxsystem : MonoBehaviour
{
    FMOD.Studio.EventInstance MENU;
    // Start is called before the first frame update
    void Start()
    {
        MENU = FMODUnity.RuntimeManager.CreateInstance("event:/Menu general");
    }

   
   public void Menusfx()
    {
        MENU.start();
    }
}
