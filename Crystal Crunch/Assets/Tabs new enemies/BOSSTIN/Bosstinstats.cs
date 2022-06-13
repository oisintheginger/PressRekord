using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bosstinstats : EnemyStats
{
    [SerializeField]ParticleSystem ps;
    Bosstinai tinai;
    bool halfway, quaterway;
    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
        tinai = GetComponent<Bosstinai>();
        halfway = true;
    }
    public override void TakeDamage(float damageTaken, BaseAttack incomingAttackType, GameObject gameObject = null)
    {
        myStats.Health -= damageTaken;
        
        particlemmission();

        if(myStats.Health <= myStats.MaxHealth*0.5 && halfway == true)
        {
            halfway = false;
            GetComponent<Animator>().SetTrigger("stun");
        }

        if (myStats.Health <= 0)
        {
            GetComponent<Animator>().SetTrigger("death");
            GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerHeavyAttack, 3f);
            for(int i = 0; i<6; i++)
            {
                GameEventsSystem.gameEventsSystem.UIEvents.OnIncomingCommentEvent(CommentType.Positive);
            }
            myStats.Health = myStats.MaxHealth;

        }

    }

    void particlemmission()
    {
        var tex = ps.shape;
        Vector3 postest = (tinai.PlayerRef.transform.position - transform.position);
        float degrees = Mathf.Atan2(-postest.y, -postest.x) * Mathf.Rad2Deg;
        tex.rotation = new Vector3(0, 0, degrees);

        ps.Play();
    }

    void deactivate()
    {
        gameObject.SetActive(false);
    }
}
