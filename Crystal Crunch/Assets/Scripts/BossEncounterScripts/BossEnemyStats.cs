using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyStats : MonoBehaviour, IEnemyDamageable<float, BaseAttack>
{
    BossAI MyBossAI;
    [SerializeField] int Lives;

    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;

    public bool Vulnerable = false;

    void Awake()
    {
        CurrentHealth = MaxHealth;
        if(MyBossAI == null&& this.gameObject.GetComponent<BossAI>()!=null)
        {
            MyBossAI = this.gameObject.GetComponent<BossAI>();
        }
    }

    public void TakeDamage(float damageTaken, BaseAttack incomingAttackType, GameObject gameObject = null)
    {
        GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);

        if(Vulnerable == false)
        {
            return;
        }
        CurrentHealth -= damageTaken;
        if(CurrentHealth<=0)
        {
            MyBossAI.StageKilled();
            Vulnerable = false;
        }
    }
}
