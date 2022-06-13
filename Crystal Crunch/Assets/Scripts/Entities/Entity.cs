using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct npcStats
{
    public float Health;
    public float MaxHealth;
    public float AttackCooldown;
    public bool stun; // bool added (stun will be added to player when combat is more clean
}
public class Entity : MonoBehaviour
{
    public npcStats myStats;
    public string myIDString = null;

}
