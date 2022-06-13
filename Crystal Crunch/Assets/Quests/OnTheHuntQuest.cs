using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTheHuntQuest : Quest
{

    /// <Alpha_Quest_Overview>
    ///     Detailed Description:
    ///     ->  Begin in Hub Area, 3 seconds after loading we will show the instructions through sidebar dialogue sequence
    ///     ->  We get a new Quest notification and then a Objective notification to talk to rose at 5 seconds in (use invoke to send quest update)
    ///     ->  The road to the combat area will be blocked by a road block, this is spawned by the scene loader 
    ///     ->  Rose will also be spawned by the scene loader 
    ///     ->  The player will talk to Rose, this sends a new objective notification 'look for Fynn'
    ///     ->  After speaking to Rose, road block will disappear and the ,(optional)the Rose NPC will have her dialogue object swapped to keep asking if the player has found Fynn yet
    ///     ->  The player then will go to the combat area
    ///     ->  Entering a trigger in the hub area will load the combat area, with the scene loader also loading in the Fynn object
    ///     ->  Speaking to Fynn unlocks the Leap ability, destroys the Fynn NPC object, sends the new Objective to return to speak to Rose
    ///     ->  Loading into the hub area will spawn a new version of Rose which will thank you for rescuing Fynn, speaking to here will complete the quest
    ///     
    ///     Objectives:
    ///     1 -> Talk to Rose
    ///     2 -> Talk To Fynn
    ///     3 -> Talk to Rose
    ///     
    ///     Required Objects: 
    ///         Objective 1 ->  Rose NPC telling the player to rescue Fynn
    ///                         Road block object
    ///         Objective 2 ->  Fynn Object that Teaches You the Leap
    ///         Objective 3 ->  Rose with Thank you For rescuing Fynn
    ///     
    ///     Optional Details(Do if enough Time):
    ///         ->  Speaking to Fynn will unlock a side quest with the objective of Reaching a certain area and fighting a few rats
    ///         
    /// </Alpha_Quest_Overview>

    DialogueObjective TalkToRose_RescueFynn;
    DialogueObjective TalkToFynn_UnlockLeap;
    DialogueObjective TalkToRose_ThankYou;

    [Tooltip("Kat Responds to Entering Hub Area")] [SerializeField] SidebarDialogueObject KatEntersHubArea;

    [Tooltip("Rose tells player to Find Fynn")] [SerializeField] DialogueObject TalkToRose_RescueFynn_DialogueID;
    [Tooltip("Player Talks to Fynn")] [SerializeField] DialogueObject TalkToFynn_UnlockLeap_DialogueID;
    [Tooltip("Rose thanks Player For Finding Fynn")] [SerializeField] DialogueObject TalkToRose_ThankYou_DialogueID;

    [Tooltip("Instructions for player to use dash")] [SerializeField] SidebarDialogueObject DashInstructions;
    
    [Tooltip("Quest Complete Dialogue For Rose")] [SerializeField] DialogueObject PostCompleteQuestRoseConversation;

    [Tooltip("Post Receive Quest Rose Dialogue")] [SerializeField] DialogueObject PostReceiveQuestRoseConversation;

    [SerializeField] string startingHubSceneName;

    [SerializeField] GameObject FYNNNPCOBJECT;
    [SerializeField] GameObject Roadblock;


    [SerializeField] bool DebugBool;
    private void Update()
    {
        if(DebugBool)
        {
            AdvanceQuest();
            DebugBool = false;
        }
    }
    private void Start()
    {
        QuestObjectLoading(startingHubSceneName);


        GameEventsSystem.gameEventsSystem.AreaEvents.LoadAreaEvent += QuestObjectLoading;
        GameEventsSystem.gameEventsSystem.AreaEvents.UnloadAreaEvent += QuestObjectUnloading;


        GameEventsSystem.gameEventsSystem.UnlockQuestEvent += UnlockQuest;

        this.QuestID = "On the Hunt";
        this.MyObjectives = new List<Objective>();


        TalkToRose_RescueFynn = new DialogueObjective(TalkToRose_RescueFynn_DialogueID.ConversationName, this, "Talk to Rose"); //Defining first objective
            MyObjectives.Add(TalkToRose_RescueFynn); // Adding to list its index will be 0

        TalkToFynn_UnlockLeap = new DialogueObjective(TalkToFynn_UnlockLeap_DialogueID.ConversationName, this, "Find Fynn in the Caves"); //Degfine second objective
            MyObjectives.Add(TalkToFynn_UnlockLeap);    //index will be 1

        TalkToRose_ThankYou = new DialogueObjective(TalkToRose_ThankYou_DialogueID.ConversationName, this, "Return to Rose with Fynn"); //Define third objective
            MyObjectives.Add(TalkToRose_ThankYou);  //index will be 2;

        GameEventsSystem.gameEventsSystem.OnUnlockQuest(this.QuestID);

        if (ObjectivesIndex <= MyObjectives.Count && !Completed && Unlocked)
        {
            Roadblock.SetActive(true);
            MyObjectives[ObjectivesIndex].ActivateObjective();
        }
        Notification UnlockedNot = new Notification(AlertType.NewQuest, this);
        GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(UnlockedNot);

        for(int i = 0; i<8;i++)
        {
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);
        }
    }

    public override void AdvanceQuest()
    {
        this.ObjectivesIndex += 1;
        if (ObjectivesIndex < MyObjectives.Count)
        {
            Notification NewObjective = new Notification(AlertType.ObjectiveUpdate, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(NewObjective);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);
            ObjectiveEvents();
            this.CurrentObjectiveCount = 0;
            MyObjectives[ObjectivesIndex].ActivateObjective();
            Debug.Log(MyObjectives[ObjectivesIndex].ObjectiveDescription);
        }
        else
        {
            Debug.Log("Completed");
            this.Completed = true;
            GameEventsSystem.gameEventsSystem.OnQuestCompletedEvent();
            for (int i = 0; i < 4; i++)
            {
                GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);
            }
            GameEventsSystem.gameEventsSystem.OnUnlockQuest("A Head Start");
            Notification CompletionNotification = new Notification(AlertType.QuestComplete, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(CompletionNotification);
            ObjectiveEvents();
        }

    }

    

    public override void UnlockQuest(string CheckID)
    {
        if (CheckID == this.QuestID)
        {
            Notification UnlockedNot = new Notification(AlertType.NewQuest, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(UnlockedNot);

            Notification NewObjective = new Notification(AlertType.ObjectiveUpdate, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(NewObjective);

            if(KatEntersHubArea!=null)
            {
                Invoke("DelayEnteringHubArea", 2f);
            }
            if (ObjectivesIndex < MyObjectives.Count && !Completed && !Unlocked)
            {
                Debug.Log("Unlocked Quest: " + this.QuestID);
                MyObjectives[ObjectivesIndex].ActivateObjective();
                this.Unlocked = true;
            }

        }
    }

    void DelayEnteringHubArea()
    {
        GameEventsSystem.gameEventsSystem.DialEvents.OnStartSidebarDialogue(KatEntersHubArea);
    }

    void ObjectiveEvents() // this is the method that activates the objective complete events, such as adding items etc
    {
        switch (ObjectivesIndex)
        {

            case 1:
                Roadblock.SetActive(false);
                break;
            case 2:
                StartCoroutine(DeactivationDelay(FYNNNPCOBJECT));
                GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += StartDashInstructionsAfter;
                GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerUnlock("Dash");
                break;
        }
    }

    void StartDashInstructionsAfter()
    {
        if(DashInstructions!=null)
        { 
            GameEventsSystem.gameEventsSystem.DialEvents.OnStartSidebarDialogue(DashInstructions);
        }
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent -= StartDashInstructionsAfter;
    }


    public override void QuestObjectLoading(string SceneLoadingIn)
    {

        foreach (RequiredQuestObject RQO in this.RequiredObjects)
        {
            if (this.ObjectivesIndex == RQO.RelevantObjectiveIndex)
            {
                if (RQO.SceneID == SceneLoadingIn)
                {
                    RQO.RequiredObject.SetActive(true);
                }
            }
        }
    }

    public override void QuestObjectUnloading(string SceneUnloadingOut)
    {

        foreach (RequiredQuestObject RQO in this.RequiredObjects)
        {
            if (RQO.SceneID == SceneUnloadingOut)
            {
                RQO.RequiredObject.SetActive(false);
            }
            
        }
    }

    IEnumerator DeactivationDelay(GameObject ObjectToDeactivate)
    {
        yield return new WaitForSeconds(1f);
        ObjectToDeactivate.SetActive(false);
        yield break;
    }
}
