using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum AlertType
{
    NewQuest,
    QuestComplete,
    ObjectiveUpdate
}
public struct Notification
{
    public AlertType IncomingAlertType;
    public Quest NotificationQuest;

    public Notification(AlertType _AlertType, Quest _ReleventQuest)
    {
        IncomingAlertType = _AlertType;
        NotificationQuest = _ReleventQuest; 
    }
}
public class QuestAlertUIManager : MonoBehaviour
{
    public Queue<Notification> NotificationQueue;

    public TMP_Text Header;
    public TMP_Text SubHeader;
    public GameObject NotificationIconGameObject;


    public RectTransform LerpToPosition;
    public RectTransform startPosition;


    bool CoroutineIsRunning = false;

    private void Awake()
    {
        NotificationQueue = new Queue<Notification>();


    }

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.UIEvents.IncomingNotificationEvent += AddToNotificationQueue;
    }

    
    void AddToNotificationQueue(Notification IncomingNotification)
    {
        NotificationQueue.Enqueue(IncomingNotification);
        StartCoroutine(ProcessingCoroutine());
    }
    
    IEnumerator ProcessingCoroutine()
    {
        if(CoroutineIsRunning==true)
        {
            yield break;
        }
        CoroutineIsRunning = true;
        while(NotificationQueue.Count >=1)
        {
            Notification NotifToProcess = NotificationQueue.Peek();
            ProcessAlert(NotifToProcess);
            while(Vector3.Distance(NotificationIconGameObject.GetComponent<RectTransform>().position, LerpToPosition.position) > .5f)
            {
                NotificationIconGameObject.GetComponent<RectTransform>().position = Vector3.Lerp(NotificationIconGameObject.GetComponent<RectTransform>().position, LerpToPosition.position, 4f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1.5f);
            while (Vector3.Distance(NotificationIconGameObject.GetComponent<RectTransform>().position, startPosition.position) > .5f)
            {
                NotificationIconGameObject.GetComponent<RectTransform>().position = Vector3.Lerp(NotificationIconGameObject.GetComponent<RectTransform>().position, startPosition.position, 4f * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
        CoroutineIsRunning = false;
        yield break;
    }

    void ProcessAlert(Notification IncomingNotif)
    {
        switch (IncomingNotif.IncomingAlertType)
        {
            case AlertType.NewQuest:
                    Header.text = "New Quest";
                    SubHeader.text = IncomingNotif.NotificationQuest.QuestID;
                break;
                

            case AlertType.ObjectiveUpdate:
                    Header.text = "New Objective";
                    SubHeader.text = IncomingNotif.NotificationQuest.MyObjectives[IncomingNotif.NotificationQuest.ObjectivesIndex].ObjectiveDescription;
                    
                break;

            case AlertType.QuestComplete:
                    Header.text = "Quest Complete";
                    SubHeader.text = IncomingNotif.NotificationQuest.QuestID;
                break;

        }
        if(NotificationQueue.Count>0)
        { 
            NotificationQueue.Dequeue();
        }
    }

}
