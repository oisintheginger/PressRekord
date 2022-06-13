using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{

    public string _MyItemID;

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.DestroyQuestItem += SelfDestruct;
    }

    void SelfDestruct(string IncomingID)
    {
        Debug.Log("SelfDestructVCalled");
        if(IncomingID == _MyItemID)
        {
            Destroy(this.gameObject);
        }
    }
}
