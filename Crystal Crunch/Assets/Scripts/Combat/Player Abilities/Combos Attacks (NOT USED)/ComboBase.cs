using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct ComboIdentity
{
    public BaseInput myInputType;
    public List<BaseInput> myInputCombo;
}
public abstract class ComboBase : BaseAttack
{
    public ComboIdentity comboID;
}
