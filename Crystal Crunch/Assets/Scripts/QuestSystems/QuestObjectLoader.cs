using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectLoader : MonoBehaviour
{
    public List<GameObject> SpawnedObjects;

    public string currentSceneName;
    private void Awake()
    {
        SpawnedObjects = new List<GameObject>();
        currentSceneName = this.gameObject.scene.name;
       // Invoke("SpawningObjectsFunction", .3f);
    }


    /*
    void SpawningObjectsFunction()
    {
        foreach(Quest Q in QuestManager.questManager.UnlockedQuests)
        {
            foreach(RequiredQuestObject RQO in Q.RequiredObjects)
            {
                if(Q.ObjectivesIndex == RQO.RelevantObjectiveIndex)
                {
                    if(RQO.SceneID == currentSceneName)
                    {
                        GameObject GO = Instantiate(RQO.RequiredObjectPrefab);
                        GO.transform.position = RQO.Position;
                        GO.transform.parent = this.gameObject.transform;
                        SpawnedObjects.Add(GO);

                        RequiredQuestObject NRQO = new RequiredQuestObject();
                        NRQO.Position = RQO.Position;
                        NRQO.RelevantObjectiveIndex = RQO.RelevantObjectiveIndex;
                        NRQO.SceneID = RQO.SceneID;
                        NRQO.RequiredObjectPrefab = GO;
                        Q.SpawnedQuestObjects.Add(NRQO);
                    }
                }
            }
        }
    }
    */
    private void OnDestroy()
    {
        foreach (Quest Q in QuestManager.questManager.UnlockedQuests)
        {
            foreach (RequiredQuestObject RQO in Q.RequiredObjects)
            {
                if (RQO.SceneID == currentSceneName)
                {
                    Q.SpawnedQuestObjects.Remove(RQO);
                }
            }
        }
        foreach (GameObject G in SpawnedObjects)
        {
            if(G!=null)
            {
                Destroy(G);
            }
        }
    }
}
