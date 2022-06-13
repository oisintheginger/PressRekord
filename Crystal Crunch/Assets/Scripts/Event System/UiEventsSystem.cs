using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIEventsSystem
{
    public event Action<float> UpdatePlayerHealthBar;
    public void OnUpdatePlayerHealthBar(float ValueCheck)
    {
        if(UpdatePlayerHealthBar!=null)
        {
            UpdatePlayerHealthBar.Invoke(ValueCheck);
        }
    }


    public event Action<Notification> IncomingNotificationEvent;

    public void OnIncomingNotificationEvent(Notification TypeOfNotification)
    {
        if(IncomingNotificationEvent!=null)
        {
            IncomingNotificationEvent.Invoke(TypeOfNotification);
        }
    }

    public event Action<bool> GamePauseEvent;
    public void OnPressPauseEvent(bool isPaused)
    {
        if (GamePauseEvent != null)
        {
            GamePauseEvent.Invoke(isPaused);
        }
    }


    public event Action<CommentType> IncomingCommentEvent;
    public void OnIncomingCommentEvent(CommentType CT)
    {
        if(IncomingCommentEvent!=null)
        {
            IncomingCommentEvent.Invoke(CT);
        }
    }

    public event Action<Quest> SetStaticQuestObjectiveQuestEvent;
    public void OnSetStaticSetStaticQuestObjectiveQuestEvent(Quest QuestToSet)
    {
        if(SetStaticQuestObjectiveQuestEvent!=null)
        {
            SetStaticQuestObjectiveQuestEvent.Invoke(QuestToSet);
        }
    }

}
