using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitCounterEventSystem : MonoBehaviour
{
    public event Action<float,float, int> actionparticle;

    public void particleemit(float minsp,float maxsp, int emmissionsum)
    {
        if(actionparticle != null)
        {
            actionparticle.Invoke(minsp,maxsp, emmissionsum);
        }
    }
  
}
