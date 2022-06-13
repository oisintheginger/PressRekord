using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temporarymove : RedoneEnemyAi
{
    [SerializeField] GameObject player;
    bool test = true;
    Vector2 newdestination;


    public void FixedUpdate()
    {
        /* if (test == true)
         {
             pathredo();
         }*/
        transform.position = Vector2.MoveTowards(transform.position, pathcheck(player.transform.position), 15 * Time.fixedDeltaTime);
        //transform.position = pathcheck(player.transform.position);
    }
   

    void pathredo()
    {
        newdestination = pathcheck(player.transform.position);
        test = false;
        while (Vector2.Distance(transform.position, newdestination) > 0.5)
        {


            transform.position = Vector2.Lerp(transform.position, newdestination, 5 * Time.deltaTime);
        }
        test = true;
    }
}
