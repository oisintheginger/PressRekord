using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillObjective : Objective
{
    Quest QuestParent;
    public string TargetID;
    public int Count;
    public KillObjective(string _target, int _count,  Quest _parent, string _objectiveDescription)
    {
        TargetID = _target;
        Count = _count;
        QuestParent = _parent;
        this.ObjectiveDescription = _objectiveDescription;
    }

    public override void ActivateObjective()
    {
        GameEventsSystem.gameEventsSystem.EnemyEvents.EnemyKilled += AdvanceCount;
    }

    void AdvanceCount(string EnemyID, GameObject enemykilledobject)
    {

        if(TargetID == EnemyID && QuestParent.CurrentObjectiveCount < Count)
        {
            QuestParent.CurrentObjectiveCount += 1;
            if (QuestParent.CurrentObjectiveCount == Count)
            {
                QuestParent.CurrentObjectiveCount = 0;
                QuestParent.AdvanceQuest();
                GameEventsSystem.gameEventsSystem.EnemyEvents.EnemyKilled -= AdvanceCount;
            }
        }
    }
}
