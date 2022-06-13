using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveInactive : MonoBehaviour
{
    enum ActivateDeactivate
    {
        Activate,
        Deactivate
    }

    [SerializeField] ActivateDeactivate Type = ActivateDeactivate.Deactivate;
    public void ActivateDeactivateFunction()
    {
        switch(Type)
        {
            case ActivateDeactivate.Activate:
                this.gameObject.SetActive(true);
                break;
            case ActivateDeactivate.Deactivate:
                this.gameObject.SetActive(false);
                break;
        }
    }
}
