using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AreaEventsSystem : MonoBehaviour
{

    public event Action<string> RestartSceneEvent;
    public void OnRestartScene(string SceneToReload)
    {
        if(RestartSceneEvent!=null)
        {
            RestartSceneEvent.Invoke(SceneToReload);
        }
    }

    public event Action<string> LoadAreaEvent;
    public void OnLoadArea(string AreaToLoad)
    {
        if(LoadAreaEvent!=null)
        {
            LoadAreaEvent.Invoke(AreaToLoad);
        }
    }

    public event Action<string> UnloadAreaEvent;
    public void OnUnloadArea(string AreaToUnload)
    {
        if(UnloadAreaEvent != null)
        {
            UnloadAreaEvent.Invoke(AreaToUnload);
        }
    }

    #region Quest Stuff
    public event Action<string> AreaReached;
    public void OnAreaReached(string AreaID)
    {
        if(AreaReached!=null)
        {
            AreaReached.Invoke(AreaID); 
        }
    }

    public event Action ClearAllSpawnedQuestObjectsEvent;
    public void OnClearAllSpawnedQuestObjects()
    {
        if(ClearAllSpawnedQuestObjectsEvent != null)
        {
            ClearAllSpawnedQuestObjectsEvent.Invoke();
        }
    }

    public event Action<string> ClearSpecificSpawnedQuestObjectsEvent;
    public void OnClearSpecificSpawnedQuestObjects(string NAMECOMPARER)
    {
        if(ClearSpecificSpawnedQuestObjectsEvent != null)
        {
            ClearSpecificSpawnedQuestObjectsEvent(NAMECOMPARER);
        }
    }
    #endregion




}
