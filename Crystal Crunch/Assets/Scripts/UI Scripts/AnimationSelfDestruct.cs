using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSelfDestruct : MonoBehaviour
{
    public void DestroyMeAnimEvent()
    {
        Destroy(this.gameObject);
    }
}
