using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : BaseAttack
{
    public float AttackForce = 10f;
    public GameObject HitEffect;
   [SerializeField] GameObject wave;
   [SerializeField] GameObject dust;
    
    public void doHeavyAttack()
    {
        AttackLogic();
        StartCoroutine("heavywave");
        Instantiate(dust,transform.position + (Vector3)(InputManager.inputManager.LastMoveDirection / 2),Quaternion.LookRotation(InputManager.inputManager.LastMoveDirection));
        
    }
    
    public void doHeavyCameraShake()
    {
        GameEventsSystem.gameEventsSystem.CameraEventsSystem.CameraShake(CameraShakeType.playerHeavyAttack, 0.2f);
    }

    public void doHeavyAttackControllerVibration()
    {
        InputManager.inputManager.StartVibing(.2f, .2f, 0.2f);
        
    }

    IEnumerator heavywave()
    {
        int wavecounter = 1;
        Vector3 direction = InputManager.inputManager.LastMoveDirection;
        Vector3 playerpos =transform.position;

        float angx = direction.x < 0 ? 0 : 180;
        var angy = direction.y *90;
        angx = direction.y != 0 && direction.x < 0 ? -120*Mathf.Round(direction.y) : angx;
        //angx = direction.y != 0 && direction.x > 0 ? -90 * direction.y : angx;
        var ang =  angy + angx;

        Quaternion angle = Quaternion.Euler(new Vector3(0, 0,ang));
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = playerpos + (direction * 2 * wavecounter);
            Instantiate(wave, pos,angle);

            wavecounter++;
            yield return new WaitForSecondsRealtime(0.2f);
        }
        yield break;
    }

    private void AttackLogic()
    {
        foreach (Collider2D target in this.gameObject.GetComponent<HitBox>().targets)
        {
            
            if (HitEffect != null)
            {
                GameObject HITEFFECT = Instantiate(HitEffect);
                HITEFFECT.transform.parent = null;
                HITEFFECT.transform.position = target.transform.position + new Vector3(0, .5f, 0);
            }
            GameEventsSystem.gameEventsSystem.hitevent.particleemit(0, 3, 4);
            target.gameObject.GetComponent<IEnemyDamageable<float, BaseAttack>>().TakeDamage(this.DamageAmount, this);


            Vector2 dir = new Vector2();
            dir = target.transform.position - this.gameObject.transform.position;
        }
    }

}
