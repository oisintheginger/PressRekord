using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaObjectiveTrigger : MonoBehaviour
{

    public string AreaName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("RaisedEvent");
        GameEventsSystem.gameEventsSystem.AreaEvents.OnAreaReached(AreaName);
    }
}
