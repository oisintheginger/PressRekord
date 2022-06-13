using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LoadUnload
{
    isLoadTrigger,
    isUnloadTrigger
}
public class SceneLoaderTrigger : MonoBehaviour
{
    bool ready = true;
    public LoadUnload TriggerType;
    public string SceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ready = false;
        if(collision.gameObject.GetComponent<PlayerStats>()!=null)
        {
            switch (TriggerType)
            {
                case LoadUnload.isLoadTrigger:
                    HandleLoad();
                    break;
                case LoadUnload.isUnloadTrigger:
                    HandleUnload();
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStats>() != null)
        {
            ready = true;
        }
    }
    void HandleUnload()
    {

        GameEventsSystem.gameEventsSystem.AreaEvents.OnUnloadArea(SceneName);
    }

    void HandleLoad()
    {
        GameEventsSystem.gameEventsSystem.AreaEvents.OnLoadArea(SceneName);
    }
}
