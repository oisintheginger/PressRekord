using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSliceQuest : Quest
{
    [SerializeField] string FIRSTAREANAME;
    [SerializeField] string EXITAREANAME;
    public ReachLocationObjective ReachFirstCombatArea;
    public KillObjective KillFirstRats;
    public DialogueObjective TalkToFynn;
    public ReachLocationObjective GetToExitLocation;

    /// <summary>
    /// Quest Summary:
    /// Player talks to rose -> unlocks Quest; (NON OBJECIVE)
    /// 1. Player Travels to Combat Area
    /// 2. Player Kills first Rats -> "Those things are disgusting, I better hurry up and find Fynn"
    /// 3. Player Talks to Fynn;
    /// 4. Player gets to Exit Location -> Start Dialogue between fynn and the player;
    /// </summary>
    /// 
    public DialogueObject DOKilledFirstRat;
    public DialogueObject DOReachedFinalArea;
    //public DialogueObject 
    //public DialogueObject 


    void Start()
    {
        GameEventsSystem.gameEventsSystem.UnlockQuestEvent += UnlockQuest;

        this.QuestID = this.GetType().ToString();
        this.MyObjectives = new List<Objective>();
        
        ReachFirstCombatArea = new ReachLocationObjective(FIRSTAREANAME, this, "el dorado");
            MyObjectives.Add(ReachFirstCombatArea);

        KillFirstRats = new KillObjective(RatEnemyStats.EnemyID, 2, this,"gdfgdghhfge");
            MyObjectives.Add(KillFirstRats);

        TalkToFynn = new DialogueObjective("WolfMeetDialogue", this, "Talk to Fynn");
            MyObjectives.Add(TalkToFynn);

        GetToExitLocation = new ReachLocationObjective(EXITAREANAME, this,"");
            MyObjectives.Add(GetToExitLocation);


        if (ObjectivesIndex <= MyObjectives.Count && !Completed && Unlocked)
        {
            MyObjectives[ObjectivesIndex].ActivateObjective();
        }
    }

    public override void UnlockQuest(string CheckID)
    {
        if (CheckID == this.QuestID)
        {
            //this prevents the quest from being reset if the unlock event is called again unintentionally
            if (ObjectivesIndex < MyObjectives.Count && !Completed && !Unlocked)
            {
                Debug.Log("UnlockedQuest: " + this.QuestID);
                MyObjectives[ObjectivesIndex].ActivateObjective();
                this.Unlocked = true;


                Notification QuestUnlockedNotif = new Notification(AlertType.NewQuest, this);
                GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(QuestUnlockedNotif);
            }

        }
    }

    public override void AdvanceQuest()
    {
        this.ObjectivesIndex += 1;
        if (ObjectivesIndex < MyObjectives.Count)
        {
            ObjectiveEvents();
            this.CurrentObjectiveCount = 0;
            MyObjectives[ObjectivesIndex].ActivateObjective();

        }
        else
        {
            this.Completed = true;


            Debug.Log("Completed Quest " + this.QuestID);
        }
        ObjectiveEvents();
    }




    // Start is called before the first frame update

    void ObjectiveEvents() // this is the method that activates the objective complete events, such as adding items etc
    {
        switch (ObjectivesIndex)
        {

            case 1:
                GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerUnlock(typeof(Pushback).ToString());
                break;
            case 2:
                if(DOKilledFirstRat!=null)
                {
                    GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.istalking);
                    GameEventsSystem.gameEventsSystem.PlayerReferenceObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(DOKilledFirstRat);

                }
                break;
            case 4:
                if (DOReachedFinalArea != null)
                {
                    GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.istalking);
                    GameEventsSystem.gameEventsSystem.PlayerReferenceObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(DOReachedFinalArea);
                }
                break;


        }
    }

    public override void QuestObjectLoading(string SceneLoadingIn)
    {
        throw new System.NotImplementedException();
    }

    public override void QuestObjectUnloading(string SceneUnloadingOut)
    {
        throw new System.NotImplementedException();
    }
}
