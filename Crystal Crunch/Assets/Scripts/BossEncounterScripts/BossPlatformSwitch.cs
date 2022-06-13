using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BossPlatformSwitch : MonoBehaviour, IEnemyDamageable<float, BaseAttack>
{
    [SerializeField] Color TriggeredColor;
    [SerializeField] BossAI BossAI;
    [SerializeField] Light2D LightSwitch;
    public GameObject PlatformBlocker;

    public bool Active = false;
    public void TakeDamage(float damageTaken, BaseAttack incomingAttackType, GameObject gameObject = null)
    {
       if(Active)
        {
            BossAI.SwitchClicked();
            LightSwitch.color = TriggeredColor;
            Active = false;
        }
    }

    public void ActivateSwitch()
    {
        Active = Active ? false : true;

        PlatformBlocker.SetActive(!Active);
    }

}
