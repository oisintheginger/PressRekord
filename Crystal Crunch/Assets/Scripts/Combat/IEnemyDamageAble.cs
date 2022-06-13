using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDamageable<T,S>
{

    void TakeDamage(T damageTaken, S incomingAttackType, GameObject gameObject = null);
}
