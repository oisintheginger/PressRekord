using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public static QuestManager questManager;
    public List<Quest> GameQuests;
    public List<Quest> UnlockedQuests;
    void Awake()
    {
        if(questManager!=this)
        {
            questManager = this;
        }

        GameQuests = new List<Quest>();
        UnlockedQuests = new List<Quest>();
        foreach(Quest q in this.GetComponents<Quest>())
        {
            GameQuests.Add(q);
            if(q.Unlocked == true && q.Completed==false)
            {
                UnlockedQuests.Add(q);
            }    
        }


    }

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.UnlockQuestEvent += AddToUnlockedList;
        Invoke("DelayedNotif", 3f);
    }


    void AddToUnlockedList(string QuestToUnlock)
    {
        foreach (Quest q in this.GetComponents<Quest>())
        {
            if(q.QuestID == QuestToUnlock)
            {
                
                if(!UnlockedQuests.Contains(q))
                {
                    UnlockedQuests.Add(q);
                }
                
            }
        }
    }

    void DelayedNotif()
    {
        Notification OnWakeUpNotification = new Notification(AlertType.NewQuest, QuestManager.questManager.UnlockedQuests[0]);
        GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(OnWakeUpNotification);
        Notification OnWakeUpNotification2 = new Notification(AlertType.ObjectiveUpdate, QuestManager.questManager.UnlockedQuests[0]);
        GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingNotificationEvent(OnWakeUpNotification2);

    }
}
