using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager current;

    [HideInInspector]public int SentenceIndex = 0; 

    #region UI Stuff, CharacterImages, Dialogue Interface, DIalogueText
    [SerializeField] GameObject GUI;
    [SerializeField] GameObject DialogueBlur;
    [SerializeField] GameObject DialogueInterface;
    [SerializeField] GameObject DialogueText;
    [SerializeField] GameObject CharacterName;
    [SerializeField] GameObject LeftCharacter, LeftBehindCharImage;
    [SerializeField] GameObject RightCharacter, RightBehindCharImage;
    [SerializeField] GameObject DialogueOptions;
    [SerializeField] GameObject DialogueButtonPrefab;
    [SerializeField] GameObject ContinueButton;
    [SerializeField] GameObject EndDialogueButton;
    [SerializeField] GameObject ShopButton;
    [SerializeField] Sprite EmptyPNG;
    #endregion

    [SerializeField] GameObject fader;

    public List<GameObject> dialogueButtonsList;
    public DialogueObject dialogueToDisplay;
    private GameObject currentNPC;
    private int dialoguecheck = 0;
    [SerializeField] GameObject ShopMenu;
    
    [SerializeField] float TransSpeed = 0.5f;

    const string BackgroundAnimationTrigger = "AnimationTrigger";
    [SerializeField] Animator BackgroundAnimator;
    //soundstuff;
  
    FMOD.Studio.PARAMETER_ID paraid;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        dialogueButtonsList = new List<GameObject>();
        DialogueBlur.SetActive(false);
    }

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.DialEvents.StartDialogueEvent += StartDialogue;
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += EndDialogue;


        FMOD.Studio.EventDescription Event;
        FMOD.Studio.PARAMETER_DESCRIPTION Parameter;
        Event = FMODUnity.RuntimeManager.GetEventDescription("event:/Music");
        Event.getParameterDescriptionByName("enemy encounter", out Parameter);
        paraid = Parameter.id;
    }

    public void StartDialogue(DialogueObject DO)
    {
        GUI.SetActive(false);
        DialogueBlur.SetActive(true);
        PlayerStats._CurrentPlayerStats.isTalking = true;
        InputManager.PlayerInDialogue = true;

        StartCoroutine(StateChangeDelay(State.istalking));
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.istalking);

        SentenceIndex = 0;
        DialogueInterface.GetComponent<UITweaner>().OnOpen();


        if(DO._animatorOverrideController!=null)
        {
            BackgroundAnimator.gameObject.SetActive(true);
            BackgroundAnimator.runtimeAnimatorController = DO._animatorOverrideController;
            BackgroundAnimator.SetTrigger(BackgroundAnimationTrigger);
        }
        else
        {
            BackgroundAnimator.gameObject.SetActive(false);
            BackgroundAnimator.runtimeAnimatorController = null;
        }




        dialogueToDisplay = DO;
        DisplaySentence();
        float picked = musicmethod();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(paraid, picked);
        
    }

    public void ContinueConversation() 
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.istalking);
        InputManager.inputManager.StartVibing(.1f, .1f, 0.1f);

        if (dialogueToDisplay == null)
        {
            Debug.Log("NULL DIALOGUE"); 
            return;
        }

        if(TypingFinished == false)
        {
            StopCoroutine("TYPEEFFECT");
            DialogueText.GetComponent<TMP_Text>().text = dialogueToDisplay.Sentences[SentenceIndex].dialogueSentence;
            TypingFinished = true;
            return;
        }

        if(SentenceIndex==dialogueToDisplay.Sentences.Count-1)
        {
            if(dialogueToDisplay.Sentences[dialogueToDisplay.Sentences.Count-1].ConnectedDialogue!=null)
            {
                GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(dialogueToDisplay.Sentences[dialogueToDisplay.Sentences.Count - 1].ConnectedDialogue);
                return;
            }
            GameEventsSystem.gameEventsSystem.DialEvents.OnEndDialogue();
            EndDialogue();
            return;
        }
        SentenceIndex += 1;
        DisplaySentence();

    }

    void DisplaySentence()
    {
        //you have to set the focus of the UI events system, but first it must be cleared
        EventSystem.current.SetSelectedGameObject(null);
        Sentence CurrentSentence = dialogueToDisplay.Sentences[SentenceIndex];
        Sentence PreviousSentence = new Sentence();
        if(SentenceIndex>0)
        {
            PreviousSentence = dialogueToDisplay.Sentences[SentenceIndex - 1];
        }
        
        
        //ShopButton.SetActive(CurrentSentence.vendorEnabled); //setting the shop button active depending on whether the current sentence has it enabled
        
        if(SentenceIndex == dialogueToDisplay.Sentences.Count-1)
        {

            if(dialogueToDisplay.Sentences[SentenceIndex].ConnectedDialogue != null)
            {
                ContinueButton.SetActive(true);
                EndDialogueButton.SetActive(false);
                EventSystem.current.SetSelectedGameObject(ContinueButton);
            }
            else
            {
                ContinueButton.SetActive(false);
                EndDialogueButton.SetActive(true);
                EventSystem.current.SetSelectedGameObject(EndDialogueButton); //setting UI Menu Selection Focus
            }

        }

        if(CurrentSentence.myDialogueOptions.Count>0)
        {
            ContinueButton.SetActive(false);
            EndDialogueButton.SetActive(false);
        }

        if(SentenceIndex < dialogueToDisplay.Sentences.Count-1)
        {
            ContinueButton.SetActive(true);
            EndDialogueButton.SetActive(false);
            EventSystem.current.SetSelectedGameObject(ContinueButton);
        }
        
        if(dialogueToDisplay.isEntryPoint == true)
        {
            ContinueButton.SetActive(false);
            EndDialogueButton.SetActive(true);

        }

        ShowDialogueOptions();

        CharacterName.GetComponent<TMP_Text>().text = CurrentSentence.CharacterNameDisplay;

        switch (CurrentSentence.myEvent)
        {
            case EventType.UnlockAbility:
                    GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerUnlock(CurrentSentence.AbilityToUnlock);
                break;
            case EventType.UnlockQuest:
                    GameEventsSystem.gameEventsSystem.OnUnlockQuest(CurrentSentence.QuestIdToUnlock);
                break;

            case EventType.TravelToArea:
                GameEventsSystem.gameEventsSystem.DialEvents.OnEndDialogue();
                StartCoroutine(travelToAreaDelay(CurrentSentence));
                break;
                
        }

         //DialogueText.GetComponent<TMP_Text>().text = CurrentSentence.dialogueSentence;
        if(!TypingFinished)
        {
            StopCoroutine("TYPEEFFECT");
        }
        StartCoroutine("TYPEEFFECT", CurrentSentence.dialogueSentence);

        switch (CurrentSentence.ActiveSprite)
        {
            case LeftOrRight.Left:
                if(PreviousSentence.ActiveSprite==CurrentSentence.ActiveSprite && SentenceIndex>0 && CurrentSentence.LerpBackCharacter == false)
                {
                    
                    break;
                }
                LeftCharacter.GetComponent<UITweaner>().UnShrink();
                LeftBehindCharImage.GetComponent<UITweaner>().UnShrink();
                RightBehindCharImage.GetComponent<UITweaner>().Shrink();
                RightCharacter.GetComponent<UITweaner>().Shrink();
                if (dialoguecheck <2 && dialogueToDisplay.Sentences[SentenceIndex].leftsound != Sounddecider.nothing)
                {
                    FMOD.Studio.EventInstance Dialoguesound = FMODUnity.RuntimeManager.CreateInstance(Soundmethod(dialogueToDisplay.Sentences[SentenceIndex].leftsound));
                    Dialoguesound.start();
                    dialoguecheck++;
                }
                break;
            case LeftOrRight.Right:
                if (PreviousSentence.ActiveSprite == CurrentSentence.ActiveSprite && SentenceIndex > 0 && CurrentSentence.LerpBackCharacter == false)
                {
                    break;
                }
                LeftCharacter.GetComponent<UITweaner>().Shrink();
                RightBehindCharImage.GetComponent<UITweaner>().UnShrink();
                LeftBehindCharImage.GetComponent<UITweaner>().Shrink();
                RightCharacter.GetComponent<UITweaner>().UnShrink();
                if (dialoguecheck <2 && dialogueToDisplay.Sentences[SentenceIndex].rightsound != Sounddecider.nothing)
                {
                    FMOD.Studio.EventInstance Dialoguesound = FMODUnity.RuntimeManager.CreateInstance(Soundmethod(dialogueToDisplay.Sentences[SentenceIndex].rightsound));
                    Dialoguesound.start();
                    dialoguecheck++;
                }
                break;
        }
       
        LeftCharacter.GetComponent<Image>().sprite = CurrentSentence.LeftSprite;
        RightCharacter.GetComponent<Image>().sprite = CurrentSentence.RightSprite;

        if(CurrentSentence.LeftBehindSprite!=null)
        {
            LeftBehindCharImage.GetComponent<Image>().sprite = CurrentSentence.LeftBehindSprite;
        }
        else if(EmptyPNG != null)
        {
            LeftBehindCharImage.GetComponent<Image>().sprite = EmptyPNG;
        }
        if (CurrentSentence.RightBehindSprite != null)
        {
            RightBehindCharImage.GetComponent<Image>().sprite = CurrentSentence.RightBehindSprite;
        }
        else if(EmptyPNG!=null)
        {
            RightBehindCharImage.GetComponent<Image>().sprite = EmptyPNG;
        }
    }

    void ShowDialogueOptions()
    {
        ClearButtonOptions();
        if(dialogueToDisplay.Sentences[SentenceIndex].myDialogueOptions.Count>0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            DialogueOptions.GetComponent<UITweaner>().OnOpen();
            DialogueOptions.SetActive(true);
            foreach (DialogueOption DO in dialogueToDisplay.Sentences[SentenceIndex].myDialogueOptions)
            {
                GameObject newButton = Instantiate(DialogueButtonPrefab, DialogueOptions.transform);
                dialogueButtonsList.Add(newButton);
                newButton.gameObject.GetComponent<dialogueButton>().myIndex = dialogueToDisplay.Sentences[SentenceIndex].myDialogueOptions.IndexOf(DO);
                string buttonTitle = DO.LinkedDialogueName;
                newButton.GetComponent<dialogueButton>().SetButtonText(buttonTitle);
            }
            EventSystem.current.SetSelectedGameObject(dialogueButtonsList[0]);//setting UI Menu Selection Focus
            dialogueButtonsList[0].GetComponent<Button>().Select();
        }
    }
    
    public void EndDialogue()
    {
        PlayerStats._CurrentPlayerStats.isTalking = false;
        InputManager.PlayerInDialogue = false;
        StartCoroutine(StateChangeDelay(State.isIdle));

        LeftCharacter.GetComponent<UITweaner>().Shrink();
        RightCharacter.GetComponent<UITweaner>().Shrink();

        PlayerStats._CurrentPlayerStats.isTalking = false;

        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(paraid, 0);
        SentenceIndex = 0;
        DialogueInterface.GetComponent<UITweaner>().OnClose();
        GUI.SetActive(true);
        DialogueBlur.SetActive(false);
        BackgroundAnimator.gameObject.SetActive(false);
        dialoguecheck = 0;
    }

    public void EndButton()
    {
        InputManager.inputManager.StartVibing(.1f, .1f, 0.1f);
        if (TypingFinished == false)
        {
            StopCoroutine("TYPEEFFECT");
            DialogueText.GetComponent<TMP_Text>().text = dialogueToDisplay.Sentences[SentenceIndex].dialogueSentence;
            TypingFinished = true;
            return;
        }
        PlayerStats._CurrentPlayerStats.isTalking = false;
        InputManager.PlayerInDialogue = false;
        StartCoroutine(StateChangeDelay(State.isIdle));

        LeftCharacter.GetComponent<UITweaner>().Shrink();
        RightCharacter.GetComponent<UITweaner>().Shrink();

        PlayerStats._CurrentPlayerStats.isTalking = false;

        FMODUnity.RuntimeManager.StudioSystem.setParameterByID(paraid, 0);
        SentenceIndex = 0;
        DialogueInterface.GetComponent<UITweaner>().OnClose();
        GUI.SetActive(true);
        DialogueBlur.SetActive(false);
        BackgroundAnimator.gameObject.SetActive(false);
        dialoguecheck = 0;

        GameEventsSystem.gameEventsSystem.DialEvents.OnEndDialogue();
    }
    
    IEnumerator travelToAreaDelay(Sentence sentence)
    {
        
        fader.GetComponent<Image>().enabled = true;
        fader.GetComponent<Animator>().SetBool("fade", true);
        yield return new WaitForSecondsRealtime(4.5f);
       
        GameEventsSystem.gameEventsSystem.AreaEvents.OnLoadArea(sentence.AreaToTravelTo);
        yield return new WaitForSecondsRealtime(1f);
        fader.GetComponent<Animator>().SetBool("fade", false);

        yield return new WaitForSecondsRealtime(3f);
        fader.GetComponent<Image>().enabled = false;
        yield break;
    }

    IEnumerator StateChangeDelay(State StateToTransitionTo)
    {
        yield return new WaitForSeconds(.8f);
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(StateToTransitionTo);
        yield break;
    }

    void ClearButtonOptions()
    {
        if (dialogueButtonsList == null)
            return;
        if (dialogueButtonsList.Count == 0)
            return;
        foreach(GameObject DB in dialogueButtonsList)
        {
            Destroy(DB);
        }
        dialogueButtonsList.Clear();
        DialogueOptions.GetComponent<UITweaner>().OnClose();
    }

    public void SetCurrentNPC(GameObject NPC)
    {
        currentNPC = NPC;
    }

    public GameObject GetCurrentNPC()
    {
        return currentNPC;
    }

    public void SetDialogueToDisplay(DialogueObject displayDialogue)
    {
        dialogueToDisplay = displayDialogue;
    }

    float TextSpeed;
    bool TypingFinished;
    IEnumerator TYPEEFFECT(string inputString)
    {
        TypingFinished = false;
        float TypeSpeed = (TextSpeed - 2) / (float)inputString.Length;
        DialogueText.GetComponent<TMP_Text>().text = null;
        foreach (char Character in inputString)
        {
            DialogueText.GetComponent<TMP_Text>().text += Character;
            yield return new WaitForSeconds(.02f);
        }
        TypingFinished = true;
        

        yield break;
    }

    float musicmethod()
    {
        switch (dialogueToDisplay.music)
        {
            case musicdecider.nothing:
                return 0;
            case musicdecider.Wolf:
                return 2;
            case musicdecider.rose:
                return 3;
            case musicdecider.train:
                return 4;
        }
        return 0;
    }
   
    string Soundmethod(Sounddecider Charsound)
    {
        switch (Charsound)
        {
            case Sounddecider.Player:
                return "event:/Player talking";
                
            case Sounddecider.Wolf:
                return "event:/NPC wolf";

            case Sounddecider.rose:
                return "event:/NPC rose";

            case Sounddecider.ticketmachine:
                return "event:/NPC ticketmachine";

            case Sounddecider.traintransition:
                return "event:/train sfx";
        }


        return null;
    }
}

// this extension method was found here: https://stackoverflow.com/questions/42347327/unity-how-to-dynamically-attach-an-unknown-script-to-a-gameobject-custom-edito
public static class ExtensionMethod
{
    public static Component AddComponentExt(this GameObject obj, string scriptName)
    {
        Component cmpnt = null;


        for (int i = 0; i < 10; i++)
        {
            //If call is null, make another call
            cmpnt = _AddComponentExt(obj, scriptName, i);

            //Exit if we are successful
            if (cmpnt != null)
            {
                break;
            }
        }


        //If still null then let user know an exception
        if (cmpnt == null)
        {
            Debug.LogError("Failed to Add Component");
            return null;
        }
        return cmpnt;
    }

    private static Component _AddComponentExt(GameObject obj, string className, int trials)
    {
        //Any script created by user(you)
        const string userMadeScript = "Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";
        //Any script/component that comes with Unity such as "Rigidbody"
        const string builtInScript = "UnityEngine, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

        //Any script/component that comes with Unity such as "Image"
        const string builtInScriptUI = "UnityEngine.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        //Any script/component that comes with Unity such as "Networking"
        const string builtInScriptNetwork = "UnityEngine.Networking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        //Any script/component that comes with Unity such as "AnalyticsTracker"
        const string builtInScriptAnalytics = "UnityEngine.Analytics, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

        //Any script/component that comes with Unity such as "AnalyticsTracker"
        const string builtInScriptHoloLens = "UnityEngine.HoloLens, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

        Assembly asm = null;

        try
        {
            //Decide if to get user script or built-in component
            switch (trials)
            {
                case 0:

                    asm = Assembly.Load(userMadeScript);
                    break;

                case 1:
                    //Get UnityEngine.Component Typical component format
                    className = "UnityEngine." + className;
                    asm = Assembly.Load(builtInScript);
                    break;
                case 2:
                    //Get UnityEngine.Component UI format
                    className = "UnityEngine.UI." + className;
                    asm = Assembly.Load(builtInScriptUI);
                    break;

                case 3:
                    //Get UnityEngine.Component Video format
                    className = "UnityEngine.Video." + className;
                    asm = Assembly.Load(builtInScript);
                    break;

                case 4:
                    //Get UnityEngine.Component Networking format
                    className = "UnityEngine.Networking." + className;
                    asm = Assembly.Load(builtInScriptNetwork);
                    break;
                case 5:
                    //Get UnityEngine.Component Analytics format
                    className = "UnityEngine.Analytics." + className;
                    asm = Assembly.Load(builtInScriptAnalytics);
                    break;

                case 6:
                    //Get UnityEngine.Component EventSystems format
                    className = "UnityEngine.EventSystems." + className;
                    asm = Assembly.Load(builtInScriptUI);
                    break;

                case 7:
                    //Get UnityEngine.Component Audio format
                    className = "UnityEngine.Audio." + className;
                    asm = Assembly.Load(builtInScriptHoloLens);
                    break;

                case 8:
                    //Get UnityEngine.Component SpatialMapping format
                    className = "UnityEngine.VR.WSA." + className;
                    asm = Assembly.Load(builtInScriptHoloLens);
                    break;

                case 9:
                    //Get UnityEngine.Component AI format
                    className = "UnityEngine.AI." + className;
                    asm = Assembly.Load(builtInScript);
                    break;
            }
        }
        catch (Exception e)
        {
            //Debug.Log("Failed to Load Assembly" + e.Message);
        }

        //Return if Assembly is null
        if (asm == null)
        {
            return null;
        }

        //Get type then return if it is null
        Type type = asm.GetType(className);
        if (type == null)
            return null;

        //Finally Add component since nothing is null
        Component cmpnt = obj.AddComponent(type);
        return cmpnt;
    }
}
