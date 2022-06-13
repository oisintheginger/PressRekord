using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class tinnoodleai : EnemyAI
{

    public bool isMoveable = true;
    IAstarAI AIComp;
    bool AttackReady = true;
    Vector2 AIDestVector = new Vector2();
    bool attackchainreset;
    [SerializeField]GameObject wavespawner;

    void Start()
    {
        this.MyEnemyStats = this.GetComponent<tinnoodlestats>();
        this.PlayerRef = base.PlayerRef;
        InvokeRepeating("PlayerDetection", 0.1f, 0.2f);
       
        AIDestVector = PlayerRef.transform.position;
        
    }

    public void Update()
    {
        if (PlayerDetected == true && attackchainreset==false)
        {
            attackchainreset = true;
            Invoke("spookyattack", 2f);
        }
        else if (PlayerDetected == false)
        {
            attackchainreset = false;
            MyAnim.SetBool("scream", false);
        }
    }

    void spookyattack()
    {
        MyAnim.SetBool("scream", true);
    }

   public void attackspawn()
    {
        Instantiate(wavespawner, gameObject.transform.position, gameObject.transform.rotation);
    }
    
}
