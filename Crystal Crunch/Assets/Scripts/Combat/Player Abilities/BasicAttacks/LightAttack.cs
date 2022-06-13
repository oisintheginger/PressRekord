using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : BaseAttack
{
    [SerializeField] GameObject HitEffect;
    public float AttackForce = 10f;
   [SerializeField] Playeraudiomanager attacksound;

    public void doLightAttack()
    {
        AttackLogic();
        this.gameObject.GetComponent<Rigidbody2D>().velocity = InputManager.inputManager.LastMoveDirection*30f;
    }

    public void doLightCameraShake()
    {
       // GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerAttack, 0.1f);
    }

    public void doLightAttackControllerVibration()
    {
        InputManager.inputManager.StartVibing(.2f, .2f, 0.2f);
    }

    private void AttackLogic()
    {
        foreach (Collider2D target in this.gameObject.GetComponent<HitBox>().targets)
        {

            if (HitEffect!=null && target !=null )
            {
                if (target.gameObject.layer == 12 || target.gameObject.layer == 23)
                {
                    attacksound.attackhitlightsfx();
                    Vector2 dir = new Vector2();
                    dir = target.transform.position - this.gameObject.transform.position;
                    GameObject HITEFFECT = Instantiate(HitEffect);
                    HITEFFECT.transform.parent = null;
                    HITEFFECT.transform.position = target.transform.position + new Vector3(0, .5f, 0);
                    GameEventsSystem.gameEventsSystem.hitevent.particleemit(0, 3, 1);
                    target.gameObject.GetComponent<IEnemyDamageable<float, BaseAttack>>().TakeDamage(this.DamageAmount, this);
                }
                if (target.gameObject.layer == 24)
                {
                    target.gameObject.GetComponent<IEnemyDamageable<float, BaseAttack>>().TakeDamage(this.DamageAmount, this);
                }
            }

            

        }
    }

}
