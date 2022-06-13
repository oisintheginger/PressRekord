using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AHeadStartQuest : Quest
{


    /// <summary>
    /// A Head Start Details:
    /// -> Quest is unlocked by completing the first quest (Returning Fynn to Rose)
    /// -> The Fynn NPC will be placed in the Base Scene, at the lower level of the Train Station, and will load when the sewer scene is loaded and the objective for this quest is at 0
    /// -> A Barrier will also be loaded when the objective is 0 to block the entrance to the sewers and will also disappear when the player
    /// -> The Quest will only advance when the player accepts to go into the sewer, this destroys the barrier and the Fynn NPC
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 
    /// -> Player will reach a position after 
    /// 
    /// -> The Last Objective will be "To Be Continued" and will be imposibble to complete (KILL BOSS)
    /// 
    /// 
    /// 
    /// List of Objectives:
    /// -> Talk to Fynn
    /// -> Enter Sewers (reach end of hallway)
    /// -> Escape Rats (reach end of Maze)
    /// -> Follow Dullaghan
    /// -> Kill boss(to be continued)
    /// 
    /// </summary>
    /// 

    DialogueObjective TalkToFynn; //index = 0
    ReachLocationObjective EnterTheSewers; //index = 1
    DialogueObjective UghSoBoring;
    ReachLocationObjective EscapeRats; // index = 2
    ReachLocationObjective FollowDullahan; // index = 3
    ReachLocationObjective EnterBossRoom;
    KillObjective KillBoss; // index = 4

    [Header("Objective Parameters")]
    [Space(50f)]
    [Space(3f, order = 5)] [SerializeField] [Tooltip("Dialogue Object for Talk To Fynn Objective")] DialogueObject FynnDialogueObject;
    [Space(3f, order = 5)] [SerializeField] [Tooltip("Location ID for EnterSewers Objective")] string SewerLocationID;
    [Space(3f, order = 5)] [SerializeField] [Tooltip("Location ID for EscapeRats Objective")] string RatEscapeLocationID;
    [Space(3f, order = 5)] [SerializeField] [Tooltip("Location ID for FollowDullahan Objective")] string FollowDullahanLocationID;
    [Space(3f, order = 5)] [SerializeField] [Tooltip("Location ID for FollowDullahan Objective")] string EnterTheRoomLocationID;
    [Space(3f, order = 5)] [SerializeField] [Tooltip("EnemyID for KillBoss Objective")] string BossEnemyID;

    [Header("Objective Dependent Objects")]
    [Space(50f)]
    [SerializeField] GameObject FynnNPCObject;
    [SerializeField] GameObject SewerEntranceObjectiveTrigger;
    [SerializeField] GameObject RatEscapeObjectiveTrigger;
    [SerializeField] GameObject RatEnemyParentObject;
    [SerializeField] GameObject SpeakToDullahanObjectiveTrigger;
    [SerializeField] GameObject EnterBossRoomObjectiveObjectiveTrigger;
    [SerializeField] GameObject SewerEntranceBlock;


    [Header("Rat Ambush Dialogues")]
    [Space(30f)]
    [Space(3f)] [SerializeField] [Tooltip("This is the dialogue that plays once player reaches the sewer objective")] DialogueObject RatAmbushADialogueObject;
    [Space(3f)] [SerializeField] [Tooltip("This is the dialogue that plays once the lights go off in the tunnel")] DialogueObject RatAmbushBDialogueObject;
    [Space(3f)] [SerializeField] [Tooltip("This is the dialogue that plays once the lights go turn back on in the tunnel")] DialogueObject RatAmbushCDialogueObject;

    [Header("Dullahan Dialogues")]
    [Space(30f)]
    [Space(3f)] [SerializeField] [Tooltip("This is the dialogue that plays once the player has escaped the rats")] SidebarDialogueObject GlimpseOfDullahanA;
    [Space(3f)] [SerializeField] [Tooltip("This is the dialogue that plays when the player reaches the Dullahan")] DialogueObject GlimpseOfDullahanB;

    [Header("End Credits Object")]
    [Space(30f)]
    [SerializeField] GameObject EndCreditsObject;
    
    private void Start()
    {
        GameEventsSystem.gameEventsSystem.AreaEvents.LoadAreaEvent += QuestObjectLoading;
        GameEventsSystem.gameEventsSystem.AreaEvents.UnloadAreaEvent += QuestObjectUnloading;



        GameEventsSystem.gameEventsSystem.UnlockQuestEvent += UnlockQuest;

        this.QuestID = "A Head Start";
        this.MyObjectives = new List<Objective>();

        TalkToFynn = new DialogueObjective(FynnDialogueObject.ConversationName, this, "Talk to Fynn before entering the sewers");
            MyObjectives.Add(TalkToFynn); 
        EnterTheSewers = new ReachLocationObjective(SewerLocationID, this, "Enter into the sewers to search for Krom");
            MyObjectives.Add(EnterTheSewers);
        UghSoBoring = new DialogueObjective(RatAmbushCDialogueObject.ConversationName, this, "Chat with Fynn");
            MyObjectives.Add(UghSoBoring);
        EscapeRats = new ReachLocationObjective(RatEscapeLocationID, this, "Escape the Fiends!");
            MyObjectives.Add(EscapeRats);
        FollowDullahan = new ReachLocationObjective(FollowDullahanLocationID, this, "Speak To Krom");
            MyObjectives.Add(FollowDullahan);
        EnterBossRoom = new ReachLocationObjective(EnterTheRoomLocationID, this, "Enter the Room");
            MyObjectives.Add(EnterBossRoom);
        KillBoss = new KillObjective(BossEnemyID, 1, this, "Defeat the Big Thing");
            MyObjectives.Add(KillBoss);



        if (ObjectivesIndex <= MyObjectives.Count && !Completed && Unlocked)
        {
            MyObjectives[ObjectivesIndex].ActivateObjective();
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
        }
        else
        {
            this.Completed = true;
            for(int i  = 0; i <4; i++)
            {
                GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);
            }
            Notification CompletionNotification = new Notification(AlertType.QuestComplete, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(CompletionNotification);
            ObjectiveEvents();
        }
    }

    public override void QuestObjectLoading(string SceneLoadingIn)
    {
        Debug.Log("Called The Load Area Event");
        foreach (RequiredQuestObject RQO in this.RequiredObjects)
        {
            if (this.ObjectivesIndex == RQO.RelevantObjectiveIndex && this.Unlocked == true)
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

    public override void UnlockQuest(string CheckID)
    {
        if (CheckID == this.QuestID)
        {
            GameEventsSystem.gameEventsSystem.UIEvents.OnSetStaticSetStaticQuestObjectiveQuestEvent(this);

            Notification UnlockedNot = new Notification(AlertType.NewQuest, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(UnlockedNot);

            Notification NewObjective = new Notification(AlertType.ObjectiveUpdate, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(NewObjective);

            if (ObjectivesIndex < MyObjectives.Count && !Completed && !Unlocked)
            {
                Debug.Log("Unlocked Quest: " + this.QuestID);
                MyObjectives[ObjectivesIndex].ActivateObjective();
                this.Unlocked = true;
            }
        }
    }

    void ObjectiveEvents() // this is the method that activates the objective complete events, such as adding items etc
    {
        switch (ObjectivesIndex)
        {

            case 1:
                SewerEntranceObjectiveTrigger.SetActive(true);
                SewerEntranceBlock.SetActive(false);
                FynnNPCObject.SetActive(false);
                break;
            case 2:
                SewerEntranceBlock.SetActive(true);
                GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(RatAmbushADialogueObject);
                GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += TurnOffLights;
                SewerEntranceObjectiveTrigger.SetActive(false);
                RatEscapeObjectiveTrigger.SetActive(true);
                break;
            case 4:
                GameEventsSystem.gameEventsSystem.DialEvents.OnStartSidebarDialogue(GlimpseOfDullahanA);
                SpeakToDullahanObjectiveTrigger.SetActive(true);
                break;
            case 5:
                GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(GlimpseOfDullahanB);
                SpeakToDullahanObjectiveTrigger.SetActive(false);
                EnterBossRoomObjectiveObjectiveTrigger.SetActive(true);
                break;
            case 7:
                SewerEntranceBlock.SetActive(false);
                RatEnemyParentObject.SetActive(false);
                if(EndCreditsObject!=null)
                {
                    EndCreditsObject.SetActive(true);
                }
                break;
        }
    }

    void SetEnemiesActive()
    {
        foreach(EnemyAI EAI in RatEnemyParentObject.GetComponentsInChildren<EnemyAI>())
        {
            EAI.PlayerDetected = true;
        }
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent -= SetEnemiesActive;
    }

    void MiddleFunction()
    {
        
        RatEnemyParentObject.SetActive(true);
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent -= MiddleFunction;
        GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(RatAmbushCDialogueObject);
        Notification NewObjective = new Notification(AlertType.ObjectiveUpdate, this);
        //GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(NewObjective);
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += SetEnemiesActive;
    }
    void TurnOffLights()
    {
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent -= TurnOffLights;
        GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(RatAmbushBDialogueObject);
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += MiddleFunction;
    }
}
