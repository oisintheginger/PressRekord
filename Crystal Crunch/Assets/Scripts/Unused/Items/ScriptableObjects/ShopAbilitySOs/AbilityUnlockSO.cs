using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Purchasable Ability", menuName = "Ability Vendor Item/ Create New Vendor Item")]
public class AbilityUnlockSO : ScriptableObject
{
    public Sprite VendorImage;
    public int Cost;
    public string AbilityName;
}
