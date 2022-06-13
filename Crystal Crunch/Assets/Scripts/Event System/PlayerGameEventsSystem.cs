using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerGameEventsSystem : MonoBehaviour
{

    #region UnlockingAbilities
    public event Action<string> PlayerUnlockAbilityEvent;
    public void OnPlayerUnlock(string unlockAbility)
    {
        if (PlayerUnlockAbilityEvent != null)
        {
            PlayerUnlockAbilityEvent.Invoke(unlockAbility);
        }
    }
    #endregion

    #region Player Upgrade Event
    public event Action<string, int> PlayerUpgradeEvent;
    public void OnPlayerUpgrade(string statToUpgrade, int valueToSet)
    {
        if (PlayerUpgradeEvent != null)
        {
            PlayerUpgradeEvent.Invoke(statToUpgrade, valueToSet);
        }
    }
    #endregion

    #region Player GameState and Stat Change Events
    public event Action<State> ChangePlayerState;
    
    public void OnPlayerStateChange(State newState)
    {
        if (ChangePlayerState != null)
        {
            ChangePlayerState.Invoke(newState);
            ReadPlayerStats.Invoke();
        }
    }
    public event Action ReadPlayerStats;
    public void OnReadPlayerStats()
    {
        if(ReadPlayerStats!=null)
        {
            ReadPlayerStats.Invoke();
        }
    }

    public event Action<PlayerStats> SendPlayerStats;
    public void OnSendPlayerStats(PlayerStats PS)
    {
        if(SendPlayerStats!=null)
        {
            SendPlayerStats.Invoke(PS);
        }
    }


    public event Action<float> HealPlayerEvent;
    public void OnPlayerHeal(float AmountToHeal)
    {
        if (HealPlayerEvent != null)
        {
            HealPlayerEvent.Invoke(AmountToHeal);
        }
    }

    public event Action<float, GameObject> DamagePlayerHealthEvent;
    public void OnPlayerDamage(float Damage, GameObject playerObject)
    {
        if (DamagePlayerHealthEvent != null)
        {
            DamagePlayerHealthEvent.Invoke(Damage, playerObject);
        }
    }

    public event Action<bool> PlayerSpeedChangeEvent;
    public void OnPlayerSpeedChange(bool SpeedUp)
    {
        if (PlayerSpeedChangeEvent != null)
        {
            PlayerSpeedChangeEvent.Invoke(SpeedUp);
        }
    }
    #endregion

    public event Action PlayerDiedEvent;
    
    public void OnPlayerDied()
    {
        if(PlayerDiedEvent!=null)
        {
            PlayerDiedEvent.Invoke();
        }
    }

}
