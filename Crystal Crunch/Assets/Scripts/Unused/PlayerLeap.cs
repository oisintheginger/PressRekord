using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeap : BaseAbilities
{
    
    [SerializeField] float speed;
    

    [SerializeField] Animator spriteAnim;

    IEnumerator Leap(Transform target)
    {
        if (cooldown == false)
        {
            GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.isInteracting);
            rb.isKinematic = true;
            spriteAnim.SetTrigger("Leap");

            cooldown = true;//so players dont spam
            float timeelapse = 0;
        
            Vector2 XZtarget = new Vector2(target.position.x, target.position.y);
            float distance = target.lossyScale.x<=target.lossyScale.y ? (target.lossyScale.x / 2) * 0.8f : (target.lossyScale.y / 2) * 0.8f;
           

            while (Vector3.Distance(rb.position, target.position) > distance)
            {
                timeelapse += Time.fixedDeltaTime;
                rb.MovePosition(Vector2.Lerp(rb.position, XZtarget, timeelapse/speed));
               // float posy = Mathf.LerpAngle(rb.position.y, target.position.y+height, timeelapse / speed);
               // rb.position = new Vector3(rb.position.x, posy, rb.position.z);

                yield return new WaitForEndOfFrame();
            }

            spriteAnim.SetTrigger("Land");
            GameEventsSystem.gameEventsSystem.PlayerGameEvents.OnPlayerStateChange(State.isIdle);
            rb.isKinematic=false;
            yield return new WaitForSeconds(0.5f);
            cooldown = false;
        }
        
        
    }
}
