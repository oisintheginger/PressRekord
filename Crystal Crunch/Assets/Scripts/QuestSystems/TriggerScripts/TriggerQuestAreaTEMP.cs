using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerQuestAreaTEMP : MonoBehaviour
{
    [SerializeField] string AREAID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameEventsSystem.gameEventsSystem.AreaEvents.OnAreaReached(AREAID);
    }
}
