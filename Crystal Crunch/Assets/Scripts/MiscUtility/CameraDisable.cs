using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisable : MonoBehaviour
{
    Camera camComp;
    private void Start()
    {
        GameEventsSystem.gameEventsSystem.UIEvents.GamePauseEvent += Reaction;
        camComp = this.gameObject.GetComponent<Camera>();
    }

    private void Reaction(bool gamePause)
    {
        if(gamePause)
        {
            Debug.Log("Little Green Goblin");
            camComp.enabled = true;
        }
        else
        {
            camComp.enabled = false;
        }
    }

    private void OnDestroy()
    {
        GameEventsSystem.gameEventsSystem.UIEvents.GamePauseEvent -= Reaction;
    }
}
