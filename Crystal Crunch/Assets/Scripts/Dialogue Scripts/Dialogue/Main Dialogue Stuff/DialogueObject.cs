using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.Events;
using TMPro;

public enum LeftOrRight
{
    Left,
    Right
}
public enum SpriteEmotion
{
    none,
    Happy,
    Joy,
    Frightened,
    Disgusted,
    Angry,
    Shocked
}
public enum EventType
{
    none,
    UnlockAbility,
    AddItem,
    UnlockQuest,
    TravelToArea
}

public enum Sounddecider
{
    
    nothing,
    Player,
    rose,
    ticketmachine,
    Wolf,
    traintransition,
}

public enum musicdecider
{ 
    nothing,
    Wolf,
    rose,
    train,

}
[System.Serializable]
public class ConversationEvent : UnityEvent<DialogueObject> { }



[System.Serializable]
public struct Sentence
{
    
   // [Header("Vendor Enabled")]
   // public bool vendorEnabled;
    [Header("Character Speaking Name")]
    public string CharacterNameDisplay;

    [Header("Sentence")][TextArea]
    public string dialogueSentence;
    

    [Header("Characters Images")]
    public Sprite RightSprite;
    public Sprite LeftSprite;
    public Sprite RightBehindSprite;
    public Sprite LeftBehindSprite;
    public LeftOrRight ActiveSprite;
    public SpriteEmotion LeftEmote;
    public SpriteEmotion RightEmote;
    public bool LerpBackCharacter;
    [Header("Transition")]
    public float transitionSpeed;

    [Header("Dialogue Options")]
    public List<DialogueOption> myDialogueOptions;

    [Header("Linked To Dialogue")]
    public DialogueObject ConnectedDialogue;

    [Header("Event Type")]
    public EventType myEvent;

    [Header("Fill out Relevant Info for Selected Event")]
    public string AbilityToUnlock;
    public string QuestIdToUnlock;
    public string AreaToTravelTo;

    public string SceneToUnload;
    public string SceneToLoad;
    public Vector3 PositionToTravelTo;

    [Header("voice sfx")]
    public Sounddecider leftsound;
    public Sounddecider rightsound;

}

[System.Serializable]
public struct DialogueOption
{
    public DialogueObject LinkedDialogue;
    public string LinkedDialogueName;
}

[CreateAssetMenu(fileName = "Dialogue" , menuName = "Main Dialogue/ New Dialogue Sequence")]
public class DialogueObject : ScriptableObject
{
    public bool isEntryPoint;
    public string ConversationName;
    public List<Sentence> Sentences;
    [Header("Sound area")]
   
    public musicdecider music;
    public AnimatorOverrideController _animatorOverrideController;
}




/*[CustomEditor(typeof(DialogueObject))]
public class Soundpicker : Editor
{
    string[] _choices = new[] { "Player", "WolfNPC" };
   public int leftsounds = 0;
   public int rightsounds = 0;
   
    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Left Charachter sound");
        leftsounds = EditorGUILayout.Popup(leftsounds, _choices);
        EditorGUILayout.LabelField("Right Charachter sound");
        rightsounds = EditorGUILayout.Popup(rightsounds, _choices);
        // Update the selected choice in the underlying object
        var Soundchoice = target as Soundpicker;
        // Save the changes back to the object
        base.OnInspectorGUI();
        
    }
}*/

