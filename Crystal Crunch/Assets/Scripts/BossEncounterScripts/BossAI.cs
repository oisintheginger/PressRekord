using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;
using UnityEngine.Playables;

[System.Serializable]
public struct PositionAndSwitchGroup
{
    public Transform Position;
    public BossPlatformSwitch FirstSwitch;
    public BossPlatformSwitch SecondSwitch;
    public Light2D FirstSwitchLight;
    public Light2D SecondSwitchLight;
}
public class BossAI : MonoBehaviour
{

    [SerializeField] const string BossID = "Boss";

    [SerializeField] bool DebugTrigger = false;


    [SerializeField] BossEncounterTrigger BossTrigger;

    BossEnemyStats BossStats;
    BossSpawnEnemiesAttack BossAttack;

    int BossStage = 0;
    int ActiveSwitchCount = 0;
    List<Vector2> MoveArcPositions;
    int CurrentPointIndex;
    bool Moving;

    [SerializeField] GameObject RangedAttackProjectile;

    [Header("Round Enemy Lists: ")] 
    [Space(50)]
    public List<GameObject> FirstRoundEnemies;
    [Space(10)]
    public List<GameObject> SecondRoundEnemies;
    [Space(10)]
    public List<GameObject> ThirdRoundEnemies;

    [Header("Position and Switch Groups: ")]
    [Space(30)]
    public PositionAndSwitchGroup FirstRound;
    [Space(10)]
    public PositionAndSwitchGroup SecondRound;
    [Space(10)]
    public PositionAndSwitchGroup ThirdRound;
    [Space(15)]
    public Transform CentralPosition;

    [HideInInspector] public List<GameObject> SpawnedEnemies;


    [HideInInspector] public bool SpawnedAllRoundEnemies = false;
    [HideInInspector] public bool WaveCleared = false;
    
    [Header("Platform Blockers and Room Blockers")]
    [Space(30)]
    public GameObject RoomBlockers;
    public GameObject PlatformBlockers;
    public GameObject BossPlatformBlockers;


    [Header("Platform Lights")]
    [Space(20)]
    [SerializeField] Color DeactiveColor;
    [SerializeField] Color ActiveColor;

    [Header("CameraShake")]
    [Space(20)]
    [SerializeField] NoiseSettings BossLandGroundShake;


    [Header("BossTimelineShit")]
    [Space(25)]
    [SerializeField] PlayableDirector BossCinematicTimeline;
    [SerializeField] GameObject CinematicCamera;


    [Header("Sidebar Dialogue Tips")]
    [Space(25)]
    [SerializeField] SidebarDialogueObject QuickKatHitTheSwitches;
    [SerializeField] SidebarDialogueObject KatWeHaveToBeatThisGuy;


    Vector2 StartPos = new Vector2();
    private void Awake()
    {
        SpawnedEnemies = new List<GameObject>();
        BossStats = this.gameObject.GetComponent<BossEnemyStats>();
        BossAttack = this.gameObject.GetComponent<BossSpawnEnemiesAttack>();
        MoveArcPositions = new List<Vector2>();

        StartPos = this.gameObject.transform.position;
    }

    public void StartEncounterDelay()
    {
        Invoke("Stage", 3.5f);
        Moving = true;
        MoveArcPositions = BezierCalculator.ProjectileArcCalculation(FirstRound.Position.position, this.transform.position, 50, 30f, 30f);
    }

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent += RestartEncounter;
    }

    private void Update()
    {
        if(DebugTrigger == true)
        {
            BossCinematicTimeline.Play();
            DebugTrigger = false;
        }

        if(Moving)
        {
            MoveToNewPosition();
        }

    }

    public void Stage()
    {
        FirstRound.FirstSwitch.GetComponent<BossPlatformSwitch>().Active = false;
        FirstRound.SecondSwitch.GetComponent<BossPlatformSwitch>().Active = false;
        SecondRound.FirstSwitch.GetComponent<BossPlatformSwitch>().Active = false;
        SecondRound.SecondSwitch.GetComponent<BossPlatformSwitch>().Active = false;
        ThirdRound.FirstSwitch.GetComponent<BossPlatformSwitch>().Active = false;
        ThirdRound.SecondSwitch.GetComponent<BossPlatformSwitch>().Active = false;

        FirstRound.FirstSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(true);
        FirstRound.SecondSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(true);
        SecondRound.FirstSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(true);
        SecondRound.SecondSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(true);
        ThirdRound.FirstSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(true);
        ThirdRound.SecondSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(true);


        FirstRound.FirstSwitchLight.GetComponent<Light2D>().color = DeactiveColor;
        FirstRound.SecondSwitchLight.GetComponent<Light2D>().color = DeactiveColor;

        SecondRound.FirstSwitchLight.GetComponent<Light2D>().color = DeactiveColor;
        SecondRound.SecondSwitchLight.GetComponent<Light2D>().color = DeactiveColor;

        ThirdRound.FirstSwitchLight.GetComponent<Light2D>().color = DeactiveColor;
        ThirdRound.SecondSwitchLight.GetComponent<Light2D>().color = DeactiveColor;

        //PlatformBlockers.SetActive(true);
        RoomBlockers.SetActive(true);
        BossPlatformBlockers.SetActive(true);
        CinematicCamera.gameObject.SetActive(false);

        SpawnedAllRoundEnemies = false;
        
        switch (BossStage)
        {
            case 0:
                
                StartCoroutine(RoundLogic(FirstRound.FirstSwitch, FirstRound.SecondSwitch, 
                    FirstRound.FirstSwitchLight, FirstRound.SecondSwitchLight, 
                    FirstRoundEnemies));
                BossStats.MaxHealth = 40f;
                BossStats.CurrentHealth = BossStats.MaxHealth;
                break;
            case 1:
                MoveArcPositions.Clear();
                Moving = true;
                StartCoroutine(RoundLogic(SecondRound.FirstSwitch, SecondRound.SecondSwitch,
                    SecondRound.FirstSwitchLight, SecondRound.SecondSwitchLight,
                    SecondRoundEnemies));
                MoveArcPositions = BezierCalculator.ProjectileArcCalculation(SecondRound.Position.position, this.transform.position, 50, 30f, 30f);
                BossStats.MaxHealth = 40f;
                BossStats.CurrentHealth = BossStats.MaxHealth;
                break;
            case 2:
                MoveArcPositions.Clear();
                Moving = true;
                StartCoroutine(RoundLogic(ThirdRound.FirstSwitch, ThirdRound.SecondSwitch,
                    ThirdRound.FirstSwitchLight, ThirdRound.SecondSwitchLight,
                    ThirdRoundEnemies));
                MoveArcPositions = BezierCalculator.ProjectileArcCalculation(ThirdRound.Position.position, this.transform.position, 50, 30f, 30f);
                BossStats.MaxHealth = 40f;
                BossStats.CurrentHealth = BossStats.MaxHealth;
                break;
            case 3:
                BossDefeat();
                break;
        }
    }

    void MoveToNewPosition()
    {
        if (MoveArcPositions != null && CurrentPointIndex < MoveArcPositions.Count)
        {
            float SpeedVariable = 50 * Time.deltaTime;

            this.transform.position = Vector2.Lerp(this.transform.position, MoveArcPositions[CurrentPointIndex], SpeedVariable);
            if (Vector2.Distance(this.transform.position, MoveArcPositions[CurrentPointIndex]) < .1f)
            {
                if (CurrentPointIndex < MoveArcPositions.Count - 1)
                {
                    CurrentPointIndex += 1;
                }
                if (CurrentPointIndex >= MoveArcPositions.Count - 1)
                {
                    GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerLightAttack, .5f, BossLandGroundShake);
                    Moving = false;
                    CurrentPointIndex = 0;
                    return;
                }
            }
        }
    }

    public void StageKilled()
    {
        BossStage += 1;
        Stage();
    }

    public void BossDefeat()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent -= RestartEncounter;
        GameEventsSystem.gameEventsSystem.EnemyEvents.OnEnemyKilled(BossID);
        FirstRound.FirstSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        FirstRound.SecondSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        SecondRound.FirstSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        SecondRound.SecondSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        ThirdRound.FirstSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        ThirdRound.SecondSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        StopAllCoroutines();
        PlatformBlockers.SetActive(false);
        RoomBlockers.SetActive(false);
        BossPlatformBlockers.SetActive(false);
    }

    IEnumerator RoundLogic(BossPlatformSwitch Switch1, BossPlatformSwitch Switch2, Light2D LightSwitch1, Light2D LightSwitch2, List<GameObject> EnemiesToSpawn)
    {
        yield return SpawnInEnemies(EnemiesToSpawn);
        bool EnemiesStillAlive = true;
        while(EnemiesStillAlive)
        {
            yield return ShootDamageProjectile();
            bool PassCheck = true;
            for (int i = 0; i <= SpawnedEnemies.Count - 1; i++)
            {
                if (SpawnedEnemies[i].gameObject.activeInHierarchy == true)
                {
                    Debug.Log("EnemyIsStillActive");
                    PassCheck = false;
                    Switch1.Active = false;
                    Switch2.Active = false;
                    yield return null;
                }
                if(i == SpawnedEnemies.Count - 1 && PassCheck == true)
                {
                    EnemiesStillAlive = false;
                }
            }
            yield return null;
        }
        PlatformBlockers.SetActive(false);
        GameEventsSystem.gameEventsSystem.DialEvents.OnStartSidebarDialogue(QuickKatHitTheSwitches);

        Switch1.ActivateSwitch();
        Switch2.ActivateSwitch();
        LightSwitch1.color = ActiveColor;
        LightSwitch2.color = ActiveColor;
        yield break;
    }

    IEnumerator SpawnInEnemies(List<GameObject> EnemiesToSpawn)
    {
        while (Moving)
        {
            yield return null;
        }

        bool clearedList = false;
        SpawnedAllRoundEnemies = false;
        if(SpawnedEnemies.Count>0)
        {
            while(clearedList ==false)
            {
                for (int i = 0; i <= SpawnedEnemies.Count - 1; i++)
                {
                    Destroy(SpawnedEnemies[i]);
                    Debug.Log("Destroyed enemy at " + i);
                }
                SpawnedEnemies.Clear();
                clearedList = true;
                yield return null;
            }
        }
        for (int i = 0; i <= EnemiesToSpawn.Count-1; i++)
        {
            BossAttack.SpawnAttack(EnemiesToSpawn[i]);
            yield return new WaitForSeconds(2f);
        }

        SpawnedAllRoundEnemies = true;
        yield break;
    }

    IEnumerator ShootDamageProjectile()
    {
        if(RangedAttackProjectile==null)
        {
            yield break;
        }
        for(int i =  0; i <= BossStage; i++)
        {
            BossAttack.SpawnAttack(RangedAttackProjectile, false);
            yield return new WaitForSeconds(.5f);
        }
        yield return new WaitForSeconds(3f);
        yield break; 
    }

    public void SwitchClicked()
    {
        ActiveSwitchCount += 1;
        if(ActiveSwitchCount == 2)
        {

            BossStats.Vulnerable = true;
            MoveArcPositions.Clear();
            MoveArcPositions = BezierCalculator.ProjectileArcCalculation(CentralPosition.position, this.transform.position, 50, 10f);
            Moving = true;
            ActiveSwitchCount = 0;
        }
    }

    public void AddToSpawnedEnemiesList(GameObject EnemyToAdd)
    {
        SpawnedEnemies.Add(EnemyToAdd);
    }

    void RestartEncounter()
    {
        StopAllCoroutines();
        SpawnedAllRoundEnemies = false;
        WaveCleared = false;
        BossStats.Vulnerable = false;
        BossStage = 0;
        ActiveSwitchCount = 0;
        BossTrigger.Triggered = false;
        RoomBlockers.SetActive(false);
        PlatformBlockers.SetActive(false);
        BossPlatformBlockers.SetActive(false);
        FirstRound.FirstSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        FirstRound.SecondSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        SecondRound.FirstSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        SecondRound.SecondSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        ThirdRound.FirstSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        ThirdRound.SecondSwitch.GetComponent<BossPlatformSwitch>().PlatformBlocker.SetActive(false);
        FirstRound.FirstSwitchLight.GetComponent<Light2D>().color = DeactiveColor;
        FirstRound.SecondSwitchLight.GetComponent<Light2D>().color = DeactiveColor;
        SecondRound.FirstSwitchLight.GetComponent<Light2D>().color = DeactiveColor;
        SecondRound.SecondSwitchLight.GetComponent<Light2D>().color = DeactiveColor;
        ThirdRound.FirstSwitchLight.GetComponent<Light2D>().color = DeactiveColor;
        ThirdRound.SecondSwitchLight.GetComponent<Light2D>().color = DeactiveColor;

        this.transform.position = StartPos;
        for (int i = 0; i <= SpawnedEnemies.Count - 1; i++)
        {
            Destroy(SpawnedEnemies[i]);
        }
        SpawnedEnemies.Clear();
        this.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        
        if (MoveArcPositions != null && MoveArcPositions.Count > 0)
        {
            for (int i = 0; i < MoveArcPositions.Count - 2; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(MoveArcPositions[i], MoveArcPositions[i + 1]);
            }
        }
    }

    private void OnDestroy()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent -= RestartEncounter;
    }

    
}
