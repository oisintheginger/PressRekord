using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObjective : Objective
{
    Quest QuestParent;
    string TargetDialogueID;
    public DialogueObjective(string _dialogueID, Quest _questParent, string _objectiveDescription)
    {
        TargetDialogueID = _dialogueID;
        QuestParent = _questParent;
        this.ObjectiveDescription = _objectiveDescription;
    }

    public override void ActivateObjective()
    {
        GameEventsSystem.gameEventsSystem.DialEvents.StartDialogueEvent += CheckConvoID;
    }

    void CheckConvoID(DialogueObject ConvoToCheck)
    {
        if(ConvoToCheck == null)
        {
            return;
        }
        if(ConvoToCheck.ConversationName == TargetDialogueID)
        {
            QuestParent.CurrentObjectiveCount = 0;
            QuestParent.AdvanceQuest();
            GameEventsSystem.gameEventsSystem.DialEvents.StartDialogueEvent -= CheckConvoID;
        }
    }
}
