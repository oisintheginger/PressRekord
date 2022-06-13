using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogueEventSystem : MonoBehaviour
{
    public event Action<DialogueObject> StartDialogueEvent;
    public void OnStartDialogue(DialogueObject DO)
    {
        if (StartDialogueEvent != null)
        {
            StartDialogueEvent.Invoke(DO);
        }
    }

    public event Action EndDialogueEvent;
    public void OnEndDialogue()
    {
        if (EndDialogueEvent != null)
        {
            EndDialogueEvent.Invoke();
        }
    }

    public event Action<SidebarDialogueObject> StartSidebarDialogueEvent;
    public void OnStartSidebarDialogue(SidebarDialogueObject SBDO)
    {
        if(StartSidebarDialogueEvent!=null)
        {
            StartSidebarDialogueEvent.Invoke(SBDO);
        }
    }

    public event Action EndSidebarDialogueEvent;
    public void OnEndSidebarDialogue()
    {
        if (EndSidebarDialogueEvent != null)
        {
            EndSidebarDialogueEvent.Invoke();
        }
    }
}
