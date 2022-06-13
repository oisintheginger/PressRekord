using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RequiredQuestObject
{
    public int RelevantObjectiveIndex;
    public GameObject RequiredObject;
    public string SceneID;
    public bool needsAlert;
}
public abstract class Quest : MonoBehaviour
{
    public bool Completed;

    public bool Unlocked;

    public GameObject QuestObjectParent;
    
    public int CurrentObjectiveCount;
     
    public int ObjectivesIndex = 0;

    public string QuestID;
    
    [Header("Required Objects For Quest")]
    [Space(10f)]
    public List<RequiredQuestObject> RequiredObjects;
    
    [HideInInspector] public List<RequiredQuestObject> SpawnedQuestObjects = new List<RequiredQuestObject>();

    public List<Objective> MyObjectives;
    
    public abstract void AdvanceQuest();

    public abstract void UnlockQuest(string CheckID);

    public abstract void QuestObjectLoading(string SceneLoadingIn);

    public abstract void QuestObjectUnloading(string SceneUnloadingOut);
}
