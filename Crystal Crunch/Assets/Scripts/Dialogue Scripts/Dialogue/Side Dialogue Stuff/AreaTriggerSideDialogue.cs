using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTriggerSideDialogue : MonoBehaviour
{

    [SerializeField] bool isOnceOff = true;
    [SerializeField] SidebarDialogueObject SBDO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(SBDO==null)
            {
                Debug.Log("NO SIDEBAR DIALOGUE ATTACHED TO " + this.gameObject.name);
                return;
            }
            if(isOnceOff)
            {
                Destroy(this.gameObject);
            }
            GameEventsSystem.gameEventsSystem.DialEvents.OnStartSidebarDialogue(SBDO);
            
        }
        
    }
}
