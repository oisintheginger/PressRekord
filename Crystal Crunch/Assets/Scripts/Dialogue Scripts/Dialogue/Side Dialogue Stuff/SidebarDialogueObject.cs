using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum sideSounddecider
{

    nothing,
    Player,
    rose,
    ticketmachine,
    Wolf,
    traintransition,
}

[System.Serializable]
public struct SideBarSentence
{
    [Header("Character Speaking Name")]
    public string CharacterNameDisplay;

    [Header("Sentence")]
    [TextArea]
    public string dialogueSentence;

    [Header("Characters Image")]
    public Sprite Portrait;

    [Header("Event Type")]
    public EventType myEvent;

    [Header("Fill out Relevant Info for Selected Event")]
    public string AbilityToUnlock;
    public string QuestIdToUnlock;
    public string AreaToTravelTo;
    public sideSounddecider voicesound;
}

[CreateAssetMenu(fileName = "New Sidebar Dialogue", menuName = "Sidebar Dialogue/ New Sidebar Dialogue Sequence")]
public class SidebarDialogueObject : ScriptableObject
{
    public string ConversationName;
    public List<SideBarSentence> Sentences;

   
}
