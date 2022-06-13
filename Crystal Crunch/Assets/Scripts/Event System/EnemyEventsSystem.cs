using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyEventsSystem : MonoBehaviour
{
    public event Action<string, GameObject> EnemyKilled;
    public void OnEnemyKilled(string EnemyID, GameObject ENEMYOBJECT = null)
    {
        if(EnemyKilled!=null)
        {
            EnemyKilled.Invoke(EnemyID, ENEMYOBJECT);
        }
    }
}
