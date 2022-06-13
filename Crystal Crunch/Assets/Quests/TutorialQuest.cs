using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialQuest : Quest
{
    /*
     * Quest Starts with Kat at Cave Entrance
     * -> Explore the cave objective is completed by getting to just before the block
     * -> The Next objective is to reach the area behind the light attack block
     * -> The next objective is to explore deeper, which is completed just before the heavy attack block
     * -> Following this the player is told to go even deeper
     * 
     */

    const string QuestName = "In Search of Fame";

    ReachLocationObjective ExploreTheCave;
    KillObjective DestroyLightAttackBox;
    ReachLocationObjective ExploreTheCaveFurther;
    KillObjective DestroyHeavyAttackBox;
    ReachLocationObjective GoDeeper;
    ReachLocationObjective BoardTheTrain;
    ReachLocationObjective End;

    [SerializeField] SidebarDialogueObject LivestreamIntroContinuation, KatSeesTrainDialogue;

    [SerializeField] string ExploreTheCaveLocation, AfterLightAttackLocation, ExploreTheCaveFurtherLocation, AfterHeavyAttackLocation, GoDeeperLocation;
    private void Start()
    {
        this.QuestID = QuestName;
        this.MyObjectives = new List<Objective>();

        GameEventsSystem.gameEventsSystem.UnlockQuestEvent += UnlockQuest;

        Debug.Log("Starting Tutorial Quest");
        

        ExploreTheCave = new ReachLocationObjective(ExploreTheCaveLocation, this, "Explore the Cave...");
            MyObjectives.Add(ExploreTheCave);
        DestroyLightAttackBox = new KillObjective("LightAttackBox", 1, this, "Tap X or use Left Click to perform a Light Attack!");
            MyObjectives.Add(DestroyLightAttackBox);
        ExploreTheCaveFurther = new ReachLocationObjective(ExploreTheCaveFurtherLocation, this, "Go Even Deeper...");
            MyObjectives.Add(ExploreTheCaveFurther);
        DestroyHeavyAttackBox = new KillObjective("HeavyAttackBox", 1, this, "Hold X or Hold Left Click to perform a Heavy Attack!");
            MyObjectives.Add(DestroyHeavyAttackBox);

        GoDeeper = new ReachLocationObjective(GoDeeperLocation, this, "Into the Depths...");
            MyObjectives.Add(GoDeeper);
        BoardTheTrain = new ReachLocationObjective("ANewPlace", this, "Board the Mysterious Train");
            MyObjectives.Add(BoardTheTrain);
        End = new ReachLocationObjective("asdsddsdsds", this, "Let's see where this leads...");
            MyObjectives.Add(End);

        GameEventsSystem.gameEventsSystem.OnUnlockQuest(this.QuestID);
    }
    void DelayUnlock()
    {
        GameEventsSystem.gameEventsSystem.OnUnlockQuest(this.QuestID);
        
    }
    public override void AdvanceQuest()
    {
        this.ObjectivesIndex += 1;
        QuestEvents();
        if (ObjectivesIndex < MyObjectives.Count)
        {
            Notification NewObjective = new Notification(AlertType.ObjectiveUpdate, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(NewObjective);

            this.CurrentObjectiveCount = 0;
            MyObjectives[ObjectivesIndex].ActivateObjective();
        }
        else
        {
            this.Completed = true;
            GameEventsSystem.gameEventsSystem.OnUnlockQuest("A Head Start");
            Notification CompletionNotification = new Notification(AlertType.QuestComplete, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(CompletionNotification);
        }

    }

    public override void QuestObjectLoading(string SceneLoadingIn)
    {
    }

    public override void QuestObjectUnloading(string SceneUnloadingOut)
    {
    }

    void QuestEvents()
    {
        switch(ObjectivesIndex)
        {
            case 2:
                if(LivestreamIntroContinuation!=null)
                {
                    GameEventsSystem.gameEventsSystem.DialEvents.OnStartSidebarDialogue(LivestreamIntroContinuation);
                }
                break;
            case 5:
                if (KatSeesTrainDialogue != null)
                {
                    GameEventsSystem.gameEventsSystem.DialEvents.OnStartSidebarDialogue(KatSeesTrainDialogue);
                }
                break;
        }
    }

    public override void UnlockQuest(string CheckID)
    {
        if (CheckID == this.QuestID)
        {

            for(int i = 0; i< MyObjectives.Count; i++)
            {
                Debug.Log(MyObjectives[i].ObjectiveDescription);
            }
            Debug.Log("Unlocked Quest: " + this.QuestID);
            Notification UnlockedNot = new Notification(AlertType.NewQuest, this);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(UnlockedNot);

            if (ObjectivesIndex < MyObjectives.Count && !Completed)
            {
                Debug.Log("Unlocked Quest: " + this.QuestID);
                MyObjectives[ObjectivesIndex].ActivateObjective();
                this.Unlocked = true;
            }

        }
    }

}
