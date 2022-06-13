using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StaticQuestObjective : MonoBehaviour
{
    public TMP_Text CurrentObjectiveString;
    public Quest CurrentlyTrackedQuest;
    private void Start()
    {
        GameEventsSystem.gameEventsSystem.UIEvents.IncomingNotificationEvent += UpdateStringOnQuestProgress;
        GameEventsSystem.gameEventsSystem.UIEvents.SetStaticQuestObjectiveQuestEvent += SetQuestRef;
        
    }
    void UpdateStringOnQuestProgress(Notification Notif)
    {
        if(CurrentlyTrackedQuest!=null)
        {
            if(Notif.NotificationQuest == CurrentlyTrackedQuest)
            {
                if(Notif.IncomingAlertType== AlertType.QuestComplete)
                {
                    Debug.Log("Got the CompleteNotif");
                    List<Quest> QList = new List<Quest>();
                    foreach(Quest q in QuestManager.questManager.UnlockedQuests)
                    {
                        if(q.Completed == false)
                        {
                            QList.Add(q);
                        }
                    }
                    if(QList.Count>0)
                    {
                        CurrentlyTrackedQuest = QList[0];
                        CurrentObjectiveString.text = QList[0].MyObjectives[QList[0].ObjectivesIndex].ObjectiveDescription;
                    }
                    else
                    {
                        CurrentObjectiveString.text = "Find something to do!";
                    }
                }
                else
                {
                    CurrentObjectiveString.text = CurrentlyTrackedQuest.MyObjectives[CurrentlyTrackedQuest.ObjectivesIndex].ObjectiveDescription;
                }
            }
        }
    }

    void SetQuestRef(Quest Q)
    {
        CurrentlyTrackedQuest = Q;
        if (CurrentlyTrackedQuest != null)
        {
                if (CurrentlyTrackedQuest.Completed)
                {
                    CurrentObjectiveString.text = "Select new mission from phone";
                }
                else
                {
                    CurrentObjectiveString.text = CurrentlyTrackedQuest.MyObjectives[CurrentlyTrackedQuest.ObjectivesIndex].ObjectiveDescription;
                }
            
        }
    }
}
