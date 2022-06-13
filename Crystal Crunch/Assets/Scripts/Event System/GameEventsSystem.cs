using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsSystem : MonoBehaviour
{
    public GameObject PlayerReferenceObject;

    public static GameEventsSystem gameEventsSystem;

    public DialogueEventSystem DialEvents;
   //public NPCInteractionEventsSystem NPCInterEvents;

    public PlayerGameEventsSystem PlayerGameEvents;
    public CameraEvents CameraEventsSystem;
    public EnemyEventsSystem EnemyEvents;
    public AreaEventsSystem AreaEvents;
    public HitCounterEventSystem hitevent;


    public AudioEventsSystem AudioEvents;
    public UIEventsSystem UIEvents;

    public PlayerState _PlayerState;
    private void Awake()
    {
        if (gameEventsSystem != this)
        {
            gameEventsSystem = this;
        }
        DialEvents = this.gameObject.GetComponent<DialogueEventSystem>();
        //NPCInterEvents = this.gameObject.GetComponent<NPCInteractionEventsSystem>();

        PlayerGameEvents = this.GetComponent<PlayerGameEventsSystem>();
        CameraEventsSystem = this.GetComponent<CameraEvents>();
        EnemyEvents = this.GetComponent<EnemyEventsSystem>();
        AreaEvents = this.GetComponent<AreaEventsSystem>();
        hitevent = this.GetComponent<HitCounterEventSystem>();

        AudioEvents = new AudioEventsSystem();
        UIEvents = new UIEventsSystem();
    }


    public event Action GameSave;
    public void OnGameSave()
    {
        if (GameSave != null)
        {
            GameSave.Invoke();
        }
    }

    public event Action GameLoad;
    public void OnGameLoad()
    {
        if (GameLoad != null)
        {
            GameLoad.Invoke();
        }
    }

    public event Action<string> UnlockQuestEvent;
    public void OnUnlockQuest(string questID)
    {
        if (UnlockQuestEvent != null)
        {
            UnlockQuestEvent.Invoke(questID);
        }
    }

    public event Action<string> DestroyQuestItem;
    public void OnDestroyQuestItem(string ItemID)
    {
        if (DestroyQuestItem != null)
        {
            DestroyQuestItem.Invoke(ItemID);
        }
    }


    public event Action QuestCompletedEvent;
    public void OnQuestCompletedEvent()
    {
        if(QuestCompletedEvent!=null)
        {
            QuestCompletedEvent.Invoke();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    /*
    
    #region Dialogue Events
    public event Action<DialogueObject> StartDialogueEvent;
    public void OnStartDialogue(DialogueObject DO)
    {
        if (StartDialogueEvent != null)
        {
            StartDialogueEvent.Invoke(DO);
        }
    }

    public event Action EndDialogueEvent;
    public void OnEndDialogue()
    {
        if(EndDialogueEvent!=null)
        {
            EndDialogueEvent.Invoke();
        }
    }
    #endregion
    
    #region UnlockingAbilities
    public event Action<string> PlayerUnlockAbilityEvent;
    public void OnPlayerUnlock(string unlockAbility)
    {
        if(PlayerUnlockAbilityEvent!=null)
        {
            PlayerUnlockAbilityEvent.Invoke(unlockAbility);
        }    
    }
    #endregion

    #region Player Upgrade Event
    public event Action<string, int> PlayerUpgradeEvent;
    public void OnPlayerUpgrade(string statToUpgrade, int valueToSet)
    {
        if(PlayerUpgradeEvent!=null)
        {
            PlayerUpgradeEvent.Invoke(statToUpgrade, valueToSet);
        }
    }
    #endregion

    
    #region Item and Inventory Events
    public event Action<ITEM_ID, int> UseItemEvent;
    public void OnUseItem(ITEM_ID itemToUse, int countCheck)
    {
        if(UseItemEvent!= null)
        {
            UseItemEvent.Invoke(itemToUse, countCheck);
        }
    }

    public event Action<ITEM_ID, int> RemoveItemEvent;
    public void OnRemoveItem(ITEM_ID itemToRemove, int amountToRemove)
    {
        if(RemoveItemEvent!=null)
        {
            RemoveItemEvent.Invoke(itemToRemove, amountToRemove);
        }
    }

    public event Action<ITEM_ID, int, BaseItemSO> AddItemEvent;
    public void OnAddItem(ITEM_ID itemToAdd, int amountToAdd, BaseItemSO itemSO = null)
    {
        
        if(AddItemEvent!=null)
        {
            AddItemEvent.Invoke(itemToAdd, amountToAdd, itemSO);
        }
    }

    public event Action<PlayerInventory> SendInventoryEvent;
    public void OnSendInventory(PlayerInventory playerInventoryRef)
    {
        if(SendInventoryEvent!=null)
        {
            SendInventoryEvent.Invoke(playerInventoryRef);
        }
    }

    public event Action RequestInventoryEvent;
    public void OnRequestInventory()
    {
        if(RequestInventoryEvent!=null)
        {
            RequestInventoryEvent.Invoke();
        }
    }

    #endregion
    

    #region Player GameState and Stat Change Events
    public event Action<State> ChangePlayerState;
    public void OnPlayerStateChange(State newState)
    {
        if(ChangePlayerState!=null)
        {
            ChangePlayerState.Invoke(newState);
        }
    }


    public event Action<float> HealPlayerEvent;
    public void OnPlayerHeal(float AmountToHeal)
    {
        if(HealPlayerEvent!=null)
        {
            HealPlayerEvent.Invoke(AmountToHeal);
        }
    }

    public event Action<float> DamagePlayerHealthEvent;
    public void OnPlayerDamage(float Damage)
    {
        if(DamagePlayerHealthEvent!= null)
        {
            DamagePlayerHealthEvent.Invoke(Damage);  
        }
    }

    public event Action<bool> PlayerSpeedChangeEvent;
    public void OnPlayerSpeedChange(bool SpeedUp)
    {
        if(PlayerSpeedChangeEvent!=null)
        {
            PlayerSpeedChangeEvent.Invoke(SpeedUp);
        }
    }
    #endregion


    /*
    #region NPC Interaction Events
    public event Action<NPC> ChangeNPCEvent;
    public void OnChangeNPC(NPC npcToDisplay)
    {
        if(ChangeNPCEvent != null)
        {
            ChangeNPCEvent.Invoke(npcToDisplay);
        }
    }
    public event Action OpenVendorEvent;
    public void OnOpenVendor()
    {
        if(OpenVendorEvent!=null)
        {
            OpenVendorEvent.Invoke();
        }
    }

    public event Action CloseVendorEvent;
    public void OnCloseVendor()
    {
        if(CloseVendorEvent!=null)
        {
            CloseVendorEvent.Invoke();
        }
    }
    public event Action<BaseItemSO> BuyItemEvent;
    public void OnBuyItem(BaseItemSO BISO)
    {
        if (BuyItemEvent != null)
        {
            BuyItemEvent.Invoke(BISO);
        }

    }
    #endregion
    */

    

}
