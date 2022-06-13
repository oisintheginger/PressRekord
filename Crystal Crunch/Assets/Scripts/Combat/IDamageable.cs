using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T> 
{
    void TakeDamage(T damageTaken, GameObject gameobject = null);

}
