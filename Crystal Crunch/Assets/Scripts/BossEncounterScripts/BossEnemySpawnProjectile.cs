using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BossEnemySpawnProjectile : MonoBehaviour
{
    public float MyDamage = .5f;
    public float DamageRadius = 3f;
    Animator MyAnim;
    FMOD.Studio.EventInstance spitcollision;
    public GameObject MyHitIndicatorRef;
    public float ProjectileSpeed = 60f;
    public float ProjectileSpeedMult = 100;
    int CurrentIndex = 0;

    bool Spawned = false;
    
    [HideInInspector] public List<Vector2> MyPath;

    [HideInInspector] public GameObject EnemyToSpawn;

    [HideInInspector] public BossAI BossParent;

    [SerializeField] Cinemachine.NoiseSettings SpawnEnemyShake;

    public bool IsEnemySpawner = true;

    private void Awake()
    {
        MyAnim = this.gameObject.GetComponent<Animator>();
    }
    bool hitPlayer = false;
    public LayerMask PlayerLayer;
    public void DoProjectileEffect()
    {
        
        MyAnim.SetTrigger("Splash");
        FMODUnity.RuntimeManager.PlayOneShot("event:/fly spit collision", transform.position);
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerHeavyAttack, .3f, SpawnEnemyShake);
        Collider2D Cols = Physics2D.OverlapCircle(this.transform.position, DamageRadius, PlayerLayer);
        if (Cols != null && hitPlayer == false && IsEnemySpawner)
        {
            GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerDamage(MyDamage, Cols.gameObject);
            
            hitPlayer = true;
        }
        if(Spawned == false && EnemyToSpawn !=null)
        {
            GameObject NewEnemy = Instantiate(EnemyToSpawn, BossParent.transform.parent);
            NewEnemy.transform.position = this.transform.position;
            if(IsEnemySpawner)
            { 
                BossParent.AddToSpawnedEnemiesList(NewEnemy);
            } 
            Spawned = true;
        }
    }

    private void FixedUpdate()
    {
        if (MyPath == null)
        {
            CurrentIndex = 0;
            return;
        }
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (MyPath != null && CurrentIndex < MyPath.Count)
        {
            float SpeedVariable = ProjectileSpeed + (((CurrentIndex) / MyPath.Count) * ProjectileSpeedMult);

            this.transform.position = Vector2.Lerp(this.transform.position, MyPath[CurrentIndex], SpeedVariable);
            if (Vector2.Distance(this.transform.position, MyPath[CurrentIndex]) < .1f)
            {
                if (CurrentIndex < MyPath.Count - 1)
                {
                    CurrentIndex += 1;
                }
                if (CurrentIndex >= MyPath.Count - 1)
                {
                    DoProjectileEffect();
                }
            }
        }
    }


    public void DestroyProjectile()
    {

        Destroy(this.gameObject);
    }

    public void flyspittingsound()
    {
        //FMODUnity.RuntimeManager.PlayOneShot("event:/fly spit", transform.position);
        spitcollision.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        spitcollision.start();
    }
}
