using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum NPCType
{
    Regular,
    ShopKeeper,
    UpgradeMerchant,
    AbilityMerchant,
    TravelMerchant,
    CapacityMerchant
}

[System.Serializable]
public struct QuestDependentDialogue
{
    public string RelevantQuestID;
    public int RelevantQuestobjectiveIndex;
    public DialogueObject RelevantQuestDialogue;
    public bool isFinalDialogue;
}
public class NPC : MonoBehaviour
{

    public List<QuestDependentDialogue> MyQuestLinkedDialogues;

    public DialogueObject MyDefaultDialogue;

    bool playerInteracting = false;

    public string NPC_ID;

    public bool DestroyOnInteraction;

    [SerializeField] GameObject InteractionSprite;

    [SerializeField] GameObject alertsprite;

    PlayerStats currentPlayerStats;


    private void Start()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.SendPlayerStats += SetPlayerStatRef;
        InputManager.inputManager.PInput.PlayerInputControls.Interaction.performed += ctx => EnterDialogue(playerInteracting);
        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.Interaction.performed += ctx => EnterDialogue(playerInteracting);
        alertcheck();
    }

    void EnterDialogue(bool isInteracting)
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnReadPlayerStats();
        if(isInteracting && PlayerStats._CurrentPlayerStats.PlayerStateInfo.playerState != State.istalking && InputManager.GameIsPaused == false)
        {
            GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.istalking);
            DialogueObject DO = DialogueObjectSearch();
            GameEventsSystem.gameEventsSystem.DialEvents.OnStartDialogue(DO);
            GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);
        }

        if(DestroyOnInteraction == true)
        {
            Destroy(this.gameObject);
        }
    }

    DialogueObject DialogueObjectSearch() //reads through dialogue objects connected to the NPC 
    {
        foreach (Quest Q in QuestManager.questManager.UnlockedQuests)
        {
            foreach(QuestDependentDialogue QDD in MyQuestLinkedDialogues)
            {
                if(Q.Completed == false && Q.ObjectivesIndex == QDD.RelevantQuestobjectiveIndex && Q.QuestID == QDD.RelevantQuestID && Q.Unlocked == true && QDD.isFinalDialogue == false)
                {
                    Debug.Log(Q.QuestID);
                    return QDD.RelevantQuestDialogue;
                }
                if (Q.Completed == true && QDD.isFinalDialogue)
                {
                    return QDD.RelevantQuestDialogue;
                }
            }
        }
        Debug.Log("Defaulting");
        return MyDefaultDialogue;
    }

    void SetPlayerStatRef(PlayerStats PL)
    {
        currentPlayerStats = PL;
    }

    void alertcheck()
    {
        foreach (Quest Q in QuestManager.questManager.UnlockedQuests)
        {
            foreach (QuestDependentDialogue QDD in MyQuestLinkedDialogues)
            {
                if (Q.Completed == false  && Q.QuestID == QDD.RelevantQuestID &&  Q.ObjectivesIndex == QDD.RelevantQuestobjectiveIndex && QDD.RelevantQuestDialogue!=null)
                {
                     if (alertsprite != null)
                     {
                        alertsprite.SetActive(true);
                     }
                    return;
                }
                
            }
        }

        if (alertsprite != null)
        {
            alertsprite.SetActive(false);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInteracting = true;
            if(InteractionSprite!=null)
            {
                InteractionSprite.SetActive(true);
            }
            if (alertsprite != null)
            {
                alertsprite.SetActive(false);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerInteracting = false;
            alertcheck();
            if (InteractionSprite != null)
            {
                InteractionSprite.SetActive(false);
            }
            
        }
    }
    private void OnDestroy()
    {
        playerInteracting = false;

        InputManager.inputManager.PInput.PlayerInputControls.Interaction.performed -= ctx => EnterDialogue(playerInteracting);
        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.Interaction.performed -= ctx => EnterDialogue(playerInteracting);
    }
}
