using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLoadedTeleportation : MonoBehaviour
{
    [SerializeField] string AreaName;

    // Start is called before the first frame update
    void Start()
    {
        GameObject P = FindObjectOfType<PlayerStats>().gameObject;
        P.transform.position = this.transform.position;
        ActiveSceneManager._Current.CurrentlyLoadedScene = this.gameObject.scene.name;
        GameEventsSystem.gameEventsSystem.AreaEvents.OnAreaReached(AreaName);
    }
}
