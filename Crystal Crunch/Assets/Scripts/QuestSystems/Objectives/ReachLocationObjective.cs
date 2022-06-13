using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachLocationObjective : Objective
{
    Quest QuestParent;
    string TargetArea;

    public ReachLocationObjective(string _targetArea, Quest _questParent, string _objectiveDescription)
    {
        TargetArea = _targetArea;
        QuestParent = _questParent;
        this.ObjectiveDescription = _objectiveDescription;
    }

    public override void ActivateObjective()
    {
        GameEventsSystem.gameEventsSystem.AreaEvents.AreaReached += CheckArea;
    }

    public void CheckArea(string AreaToCheck)
    {
        if(AreaToCheck == TargetArea)
        {
            Debug.Log("Reached Target Area " + TargetArea + " Quest Parent : " + QuestParent);
            QuestParent.CurrentObjectiveCount = 0;
            QuestParent.AdvanceQuest();
            GameEventsSystem.gameEventsSystem.AreaEvents.AreaReached -= CheckArea;
        }
    }
}
