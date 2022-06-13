using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunitySquabbleQuest : Quest
{
    [SerializeField] GameObject PartyAudioObject;


    [SerializeField] DialogueObject SpeakToNeighbourADO;
    [SerializeField] DialogueObjective SpeakToNeighbourA;

    [SerializeField] DialogueObject SpeakToNeighbourBDO;
    [SerializeField] DialogueObjective SpeakToNeighbourB;

    [SerializeField] DialogueObject ReturnToNeighbourDO;
    [SerializeField] DialogueObjective ReturnToNeighbour;

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.AreaEvents.LoadAreaEvent += QuestObjectLoading;
        GameEventsSystem.gameEventsSystem.AreaEvents.UnloadAreaEvent += QuestObjectUnloading;



        GameEventsSystem.gameEventsSystem.UnlockQuestEvent += UnlockQuest;

        this.QuestID = "Community Squabble";
        this.MyObjectives = new List<Objective>();


        SpeakToNeighbourA = new DialogueObjective(SpeakToNeighbourADO.ConversationName, this, "Speak to the neighbouring house!");
            MyObjectives.Add(SpeakToNeighbourA);

        SpeakToNeighbourB = new DialogueObjective(SpeakToNeighbourBDO.ConversationName, this, "Listen to what they have to say...");
            MyObjectives.Add(SpeakToNeighbourB);

        ReturnToNeighbour = new DialogueObjective(ReturnToNeighbourDO.ConversationName, this, "Return to the house and bring the good news...");
            MyObjectives.Add(ReturnToNeighbour);


        if (ObjectivesIndex <= MyObjectives.Count && !Completed && Unlocked)
        {
            MyObjectives[ObjectivesIndex].ActivateObjective();

            if(ObjectivesIndex == 0)
            {
                PartyAudioObject.SetActive(true);
            }
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
            for (int i = 0; i < 4; i++)
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

    void ObjectiveEvents() // this is the method that activates the objective complete events, such as adding items etc
    {
        switch (ObjectivesIndex)
        {
            case 1:
                GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += BeginDialogueB;
                break;
            case 2:
                PartyAudioObject.SetActive(false);
                break;
        }
    }


    void BeginDialogueB()
    {
        GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(SpeakToNeighbourBDO);
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent -= BeginDialogueB;
    }
    public override void UnlockQuest(string CheckID)
    {
        if (CheckID == this.QuestID)
        {

            if (ObjectivesIndex < MyObjectives.Count && !Completed && !Unlocked)
            {
                GameEventsSystem.gameEventsSystem.UIEvents.OnSetStaticSetStaticQuestObjectiveQuestEvent(this);

                Notification UnlockedNot = new Notification(AlertType.NewQuest, this);
                GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(UnlockedNot);

                Notification NewObjective = new Notification(AlertType.ObjectiveUpdate, this);
                GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(NewObjective);
                Debug.Log("Unlocked Quest: " + this.QuestID);
                MyObjectives[ObjectivesIndex].ActivateObjective();
                this.Unlocked = true;
            }
        }
    }
}
