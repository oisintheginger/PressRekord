using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SidebarDialogueManager : MonoBehaviour
{
    public static SidebarDialogueManager current;

    SidebarDialogueObject SBDialogueToDisplay;

    [SerializeField] GameObject SidebarDialogueObject;
    [SerializeField] TMP_Text SentenceTextObject;
    [SerializeField] Image CharacterPortrait;

    const string OPENTRIGGER = "Open";
    const string CLOSETRIGGER = "Close";
    int _SentenceIndex = 0;

    bool CurrentlyDisplayingDialogue = false;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.DialEvents.StartSidebarDialogueEvent += StartSidebarDialogue;
        GameEventsSystem.gameEventsSystem.DialEvents.EndSidebarDialogueEvent += EndSidebarDialogue;

        GameEventsSystem.gameEventsSystem.DialEvents.StartDialogueEvent += PauseSidebarDialogue;
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += ResumeSidebarDialogue;
    }



    void StartSidebarDialogue(SidebarDialogueObject SBDO)
    {
        if(CurrentlyDisplayingDialogue == true)
        {
            return;
        }
        CurrentlyDisplayingDialogue = true;

        _SentenceIndex = 0;
        SBDialogueToDisplay = SBDO;

        DisplaySidebarDialogue();

        SidebarDialogueObject.GetComponent<Animator>().SetTrigger(OPENTRIGGER);

    }

    void DisplaySidebarDialogue()
    {
        StartCoroutine(SentenceDisplayCycleTimer());
    }

    void EndSidebarDialogue()
    {
        SentenceTextObject.text = "";
        SidebarDialogueObject.GetComponent<Animator>().SetTrigger(CLOSETRIGGER);
        CurrentlyDisplayingDialogue = false;
    }

    [SerializeField] float TextSpeed = 5f;
    //Because the Dialogue does not continue manually, it needs a Psuedo Update Loop to run so that the conversation updates through timing 
    IEnumerator SentenceDisplayCycleTimer()
    {

        while(_SentenceIndex < SBDialogueToDisplay.Sentences.Count)
        {
            CharacterPortrait.sprite = SBDialogueToDisplay.Sentences[_SentenceIndex].Portrait;
            if (SBDialogueToDisplay.Sentences[_SentenceIndex].voicesound != sideSounddecider.nothing)
            {
                FMOD.Studio.EventInstance Dialoguesound = FMODUnity.RuntimeManager.CreateInstance(Soundmethod(SBDialogueToDisplay.Sentences[_SentenceIndex].voicesound));
                Dialoguesound.start();
            }

            if (_SentenceIndex == 0)
            {
                yield return new WaitForSeconds(2f);
            }
            SentenceTextObject.text = "";
            StartCoroutine(TYPEEFFECT(SBDialogueToDisplay.Sentences[_SentenceIndex].dialogueSentence));
            

            yield return new WaitForSeconds(TextSpeed);
            _SentenceIndex += 1;
            if(_SentenceIndex == SBDialogueToDisplay.Sentences.Count)
            {
                EndSidebarDialogue();
                yield break;
            }
        }
    }

    IEnumerator TYPEEFFECT(string inputString)
    {
        float TypeSpeed = (TextSpeed - 2) / (float) inputString.Length;
        foreach (char Character in inputString)
        {
            SentenceTextObject.text += Character;
            yield return new WaitForSeconds(.015f);
        }
        yield break;
    }

    string Soundmethod(sideSounddecider Charsound)
    {
        switch (Charsound)
        {
            case sideSounddecider.Player:
                return "event:/Player talking";

            case sideSounddecider.Wolf:
                return "event:/NPC wolf";

            case sideSounddecider.rose:
                return "event:/NPC rose";

            case sideSounddecider.ticketmachine:
                return "event:/NPC ticketmachine";

            case sideSounddecider.traintransition:
                return "event:/train sfx";
        }


        return null;
    }

    void PauseSidebarDialogue(DialogueObject USELESS = null)
    {
        SidebarDialogueObject.transform.localScale = Vector3.zero;
    }

    void ResumeSidebarDialogue()
    {
        SidebarDialogueObject.transform.localScale = Vector3.one;
    }
}
