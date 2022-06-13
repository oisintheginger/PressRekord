using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class dialogueButton : MonoBehaviour
{
    DialogueManager dm;
    public int myIndex = 0;
    private void Awake()
    {
        if(dm ==null)
        {
            dm = FindObjectOfType<DialogueManager>();
        }
        this.GetComponent<Button>().onClick.AddListener(() => Clicked());
    }
    void Clicked()
    {
        if(dm.dialogueToDisplay.Sentences[dm.SentenceIndex].myDialogueOptions[myIndex].LinkedDialogue!=null)
        {
            GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(dm.dialogueToDisplay.Sentences[dm.SentenceIndex].myDialogueOptions[myIndex].LinkedDialogue);
        }
    }

    public void SetButtonText(string Text)
    {
        this.gameObject.GetComponentInChildren<TMP_Text>().text = Text;
    }
}
