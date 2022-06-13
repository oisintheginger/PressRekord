using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finallightattack : BaseAttack
{
    [SerializeField] GameObject HitEffect;
    public float AttackForce = 10f;
    [SerializeField] Playeraudiomanager attacksound;

    public void doFinalLightAttack()
    {
        AttackLogic();
        this.gameObject.GetComponent<Rigidbody2D>().velocity = InputManager.inputManager.LastMoveDirection * 30f;
    }

    public void doLightCameraShakefinal()
    {
         GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerLeap, 0.1f);
    }

    public void doLightAttackControllerVibration()
    {
        InputManager.inputManager.StartVibing(.2f, .2f, 0.2f);
    }

    private void AttackLogic()
    {
        foreach (Collider2D target in this.gameObject.GetComponent<HitBox>().targets)
        {
            attacksound.attackhitlightsfx();
            
            if (HitEffect != null)
            {
                GameObject HITEFFECT = Instantiate(HitEffect);
                HITEFFECT.transform.parent = null;
                HITEFFECT.transform.position = target.transform.position + new Vector3(0, 0.5f, 0);

            }
            target.gameObject.GetComponent<IEnemyDamageable<float, BaseAttack>>().TakeDamage(this.DamageAmount, this);
            GameEventsSystem.gameEventsSystem.hitevent.particleemit(0, 3, 3);
            Vector2 dir = new Vector2();
            dir = target.transform.position - this.gameObject.transform.position;

        }
    }

}
