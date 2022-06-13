using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatArea : MonoBehaviour
{
    bool AreaCleared = false;
    
    public float Radius;
    public List<GameObject> MyEnemies;
    public GameObject CombatWalls;
    int RequiredCount = 0;
    int CurrentCount = 0;
    bool check = false;

    FMOD.Studio.PARAMETER_ID paraid;

    public List<Vector3> EnemyStartingPositions = new List<Vector3>();
    void Start()
    { 
        RequiredCount = MyEnemies.Count;
        InvokeRepeating("CheckRepeating", 1f, 0.5f);
        FMOD.Studio.EventDescription Event;
        FMOD.Studio.PARAMETER_DESCRIPTION Parameter;
        Event = FMODUnity.RuntimeManager.GetEventDescription("event:/Music");
        Event.getParameterDescriptionByName("enemy encounter", out Parameter);
        paraid = Parameter.id;

        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent += RePopulateCombatRoom;

        if(MyEnemies!=null)
        {
            for(int i = 0; i < MyEnemies.Count; i++)
            {
                Vector3 n = new Vector3();
                n = MyEnemies[i].transform.position;
                EnemyStartingPositions.Add(n);
            }
        }
    }


    void CheckRepeating()
    {
        bool cleared = true;

        for (int i = 0; i < MyEnemies.Count; i++)
        {
            if (MyEnemies[i].activeInHierarchy == true)
            {
                cleared = false;

                return;
            }
        }
        AreaCleared = cleared;
        if (cleared)
        {
            if (!check)
            {
                FMODUnity.RuntimeManager.StudioSystem.setParameterByID(paraid, 0);
                check = true;
            }
            CombatWalls.SetActive(false);
            Debug.Log("ClearedRoom");
        }
    }

    void RePopulateCombatRoom()
    {
        Debug.Log("CalledTheRespawn");
        CombatWalls.SetActive(false);
        this.gameObject.GetComponent<Collider2D>().enabled = true;
        
        if(AreaCleared == false)
        {
            for(int i =  0; i < MyEnemies.Count; i++)
            {
                MyEnemies[i].transform.position = EnemyStartingPositions[i];
                MyEnemies[i].GetComponent<EnemyAI>().PlayerDetected = false;
                MyEnemies[i].GetComponent<EnemyAI>().LastKnownPlayerLocation = EnemyStartingPositions[i];
                MyEnemies[i].GetComponent<EnemyStats>().myStats.Health = MyEnemies[i].GetComponent<EnemyStats>().myStats.MaxHealth;
                if (MyEnemies[i].activeInHierarchy == false)
                {
                    GameObject NewEnemy = Instantiate(MyEnemies[i]);
                    NewEnemy.transform.parent = this.transform.parent;
                    NewEnemy.transform.position = EnemyStartingPositions[i];
                    Destroy(MyEnemies[i]);
                    MyEnemies[i] = NewEnemy;
                    
                }

                MyEnemies[i].SetActive(true);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerStats>())
        {
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            FMODUnity.RuntimeManager.StudioSystem.setParameterByID(paraid, 1);
            CombatWalls.SetActive(true);
            for(int i = 0; i< MyEnemies.Count -1; i++)
            {
                MyEnemies[i].GetComponent<EnemyAI>().PlayerDetected = true;
            }
        }
        
    }

    private void OnDestroy()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent -= RePopulateCombatRoom;
    }
}
