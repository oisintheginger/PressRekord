using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public enum CharacterState
{
    Stunned,
    Talking,
    Attacking,
    Idle
}

[System.Serializable]
public enum State
{
    isIdle,
    istalking,
    isInteracting,
    isAttacking,
    isStunned,
    isImmune
}

[System.Serializable]
public struct PlayerState
{
    public State playerState;
}


public class PlayerStats : MonoBehaviour, IDamageable<float>
{
    public static PlayerStats _CurrentPlayerStats;
    public GameObject HitEffect;

    public PlayerState PlayerStateInfo;
    [SerializeField] VolumeProfile changeVolume;
    public npcStats playerStats;
    public bool canleap;
    Playeraudiomanager Playersfx;
    [SerializeField] SpriteRenderer rend;
    public bool isTalking = false;
    public float MaxSpeed;
    
    public bool iFramesActive = false;
    [SerializeField]Material flash;
    Material defaultmat;
    Animator anim;
    [SerializeField] Image buffer;
    [SerializeField] GameObject deathchecker;
    [SerializeField] GameObject ResumeButton;
    public static bool PlayerDead = false;
    

    void Awake()
    {
        if(_CurrentPlayerStats!=this)
        {
            _CurrentPlayerStats = this;
        }
    }

    private void Start()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.HealPlayerEvent += AddHealth;
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.DamagePlayerHealthEvent += TakeDamage;
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.ChangePlayerState += StateChange;
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.ReadPlayerStats += ReceiveStatRequest;

        Playersfx = GetComponent<Playeraudiomanager>();
        anim = GetComponent<Animator>();
        defaultmat = rend.material;
    }


    IEnumerator FrameFreeze()
    {
        gameObject.layer = 22;
        Playersfx.startmodulate();
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerHeavyAttack, 0.6f);
        
        buffer.enabled = true;
        Time.timeScale = .1f;
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1f;
        
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.OnRestoreDefaultPP();
        
        if (playerStats.Health <= 0)
        {
            
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(1f);
            buffer.enabled = false;
            deathchecker.SetActive(true);
            EventSystem.current.SetSelectedGameObject(ResumeButton);
            gameObject.layer = 9;
            yield break;
        }


        Playersfx.stopmodulate();

        buffer.enabled = false;
        anim.SetTrigger("hit");
        yield return new WaitForSecondsRealtime(1f);
        gameObject.layer = 9;
        rend.material = defaultmat;
        
        yield break;
    }

    
  

    public void TakeDamage(float damageTaken, GameObject PlayerObject)
    { 
        if(isTalking)
        {
            return;
        }
        if(PlayerObject!=this.gameObject)
        {
            return;
        }
        

        if(iFramesActive)
        {
            return;
        }

        GameEventsSystem.gameEventsSystem.CameraEventsSystem.OnChangePostProcessingProfile(changeVolume);
        GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Negative);
        if (HitEffect!=null)
        {
            GameObject HITEFFECT = Instantiate(HitEffect);
            HITEFFECT.transform.parent = null;
            HITEFFECT.GetComponent<SpriteRenderer>().color = Color.red;
            HITEFFECT.transform.position = this.transform.position + new Vector3(0, 1f, 0);
            HITEFFECT.transform.localScale = HITEFFECT.transform.lossyScale * 2;
        }
        

        Playersfx.Hurtsfx();
        
        playerStats.Health -= damageTaken;
        StartCoroutine(FrameFreeze());
        InputManager.inputManager.StartVibing(damageTaken / this.playerStats.MaxHealth, damageTaken / this.playerStats.MaxHealth, 0.3f);
        StartCoroutine(FrameFreeze());
       
        
        GameEventsSystem.gameEventsSystem.UIEvents.OnUpdatePlayerHealthBar(Mathf.Abs(playerStats.Health / playerStats.MaxHealth));
    }

    public void death()
    {
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.OnRestoreDefaultPP();
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerDied();
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.DamagePlayerHealthEvent -= TakeDamage;
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(null);
        deathchecker.SetActive(false);
        StartCoroutine(RespawnCoroutine());
    }

    public void AddHealth(float healthToAdd)
    {
        if(playerStats.Health + healthToAdd>playerStats.MaxHealth)
        {
            playerStats.Health = playerStats.MaxHealth;
            return;
        }
        playerStats.Health += healthToAdd;

        GameEventsSystem.gameEventsSystem.UIEvents.OnUpdatePlayerHealthBar(Mathf.Abs(playerStats.Health / playerStats.MaxHealth));
    }

    public void StateChange(State NewState)
    {
        PlayerStateInfo.playerState = NewState;
    }

    void ReceiveStatRequest()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnSendPlayerStats(this);
    }

    IEnumerator RespawnCoroutine()
    {
        PlayerDead = true;
        this.gameObject.transform.position = ActiveSceneManager._Current.RespawnLocation;
        GameEventsSystem.gameEventsSystem.AreaEvents.OnLoadArea(ActiveSceneManager._Current.RespawnToScene);
        playerStats.Health = playerStats.MaxHealth;
        GameEventsSystem.gameEventsSystem.UIEvents.OnUpdatePlayerHealthBar(Mathf.Abs(playerStats.Health / playerStats.MaxHealth));
        yield return new WaitForSeconds(3f);
        PlayerDead = false;
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.DamagePlayerHealthEvent += TakeDamage;
    }

}
