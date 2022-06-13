using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AnimationEventExposer : MonoBehaviour
{
    public UnityEvent EventFunctions;
    public void AnimationEventExposerFunction()
    {
        if(EventFunctions!=null)
        {
            EventFunctions.Invoke();
        }
    }
}
