using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BaseAbilities
{
    [SerializeField]GameObject wall;
    [SerializeField] GameObject star;
    Collider2D col;
    Color og;
    PlayerStats PS;
    bool onetimerecharge;
    // Start is called before the first frame update
    void Start()
    {
        playerinput();
        col = GetComponent<Collider2D>();
        og = star.GetComponent<SpriteRenderer>().color;
        PS = this.gameObject.GetComponent<PlayerStats>();
    }

    public void playerinput()
    {
        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.Block.started +=
      ctx =>
      {
          if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting || cooldown==true)
          {
              return;
          }
          Vector3 direction = InputManager.inputManager.playerInput != Vector2.zero ? InputManager.inputManager.playerInput : InputManager.inputManager.LastMoveDirection;
          Vector3 playerpos = transform.position + (Vector3.up * 0.8f);

          float angx = direction.x < 0 ? 0 : 180;
          var angy = direction.y * 90;
          angx = direction.y != 0 && direction.x < 0 ? -120 * Mathf.Round(direction.y) : angx;
          //angx = direction.y != 0 && direction.x > 0 ? 0 : angx;
          var ang = angy + angx;

          Quaternion angle = Quaternion.Euler(new Vector3(0, 0, ang));
          Vector3 pos = playerpos + (direction * 1.2f);
          wall.GetComponent<SpriteRenderer>().color = og;
          wall.transform.rotation = angle;
          wall.transform.position = pos;
          wall.SetActive(true);
          wall.GetComponent<counterAttack>().counter = true;
          star.GetComponent<SpriteRenderer>().enabled = true;

      };
        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.Block.performed +=
     ctx =>
     {
         if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting || cooldown == true)
         {
             return;
         }
         wall.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
         wall.GetComponent<counterAttack>().counter = false;
         Invoke("stopblock", 1.5f);
         Debug.Log("done");
         star.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);

     };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.Block.canceled +=
     ctx =>
     {
         if (PS.PlayerStateInfo.playerState == State.istalking || PS.PlayerStateInfo.playerState == State.isStunned || PS.PlayerStateInfo.playerState == State.isInteracting || cooldown == true)
         {
             return;
         }
         wall.GetComponent<counterAttack>().counter = false;
         wall.SetActive(false);
         cooldown = true;
         star.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
         CancelInvoke();
         Invoke("recharge", 1.5f);
         Debug.Log("yeehaa");

     };

     }
    void recharge()
    {
        
            cooldown = false;
            star.GetComponent<SpriteRenderer>().enabled = false;
            star.GetComponent<SpriteRenderer>().color = og;
        
    }


   void stopblock()
    {

        wall.SetActive(false);
        star.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
        cooldown = true;
        Invoke("recharge", 1.5f);
    }

}
