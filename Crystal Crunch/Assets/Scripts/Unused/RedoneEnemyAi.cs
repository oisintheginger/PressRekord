using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class RedoneEnemyAi : MonoBehaviour
{
    List<Vector2> accepted = new List<Vector2>();
    List<Vector2> declined = new List<Vector2>();
    [SerializeField] List<Vector2> pathlist = new List<Vector2>(10);
    bool pathselected = false;
    [SerializeField] bool Listfull = false;
    [SerializeField] int maxlistlenght;
    [SerializeField] Vector2 currentdestination = Vector2.zero;
    [SerializeField] public Gridmanager grid;
    int backtrackcounter = 0;
    Vector2 startinglistpos =  Vector2.zero;
    /* Vector2[] Nodecontainer = new Vector2[]
     {
         new Vector2 (2f,0),
         new Vector2 (2f,2f),
         new Vector2 (2f,-2f),
         new Vector2 (-2f,0),
         new Vector2 (-2f,-2f),
         new Vector2 (0,2f),
         new Vector2 (0,-2f),
         new Vector2(-2f,2f)

     };*/



    /* bool passable(Vector2 directioncheck)
     {
      RaycastHit2D hit = Physics2D.Linecast(pathlist[pathlist.Count-1],directioncheck );
         //Debug.DrawRay(transform.position, entitiydir.normalized, Color.green);
         if( hit.collider==null)
         {
             return true;
         }
         if (hit.collider.isTrigger)
         {
             return true;
         }

         return false;
     }*/

    bool distacncecheck(Vector2 directioncheck, Vector2 targetpos)
    {
        return Vector2.Distance(directioncheck, targetpos) <= Vector2.Distance(pathlist[pathlist.Count-1], targetpos) ? true : false;

    }

    public Vector2 pathcheck(Vector2 targetpos)
    {
        if (currentdestination == Vector2.zero)
        {
            foreach (Vector2 position in grid.Safezones)
            {
               
                if (Vector2.Distance(transform.position, position) < 0.5f)
                {
                    pathlist[0] = position;
                    startinglistpos = pathlist[0];
                    break;
                }

            }
        }
        //|| Vector2.Distance(pathlist[pathlist.Count - 1], targetpos) > 2f
        while ( Listfull == false )
        {
            foreach (Vector2 direction in grid.Nodecontainer)
            {
                Vector2 newpos = pathlist[pathlist.Count - 1] + direction;
               
                if (grid.Safezones.Contains(newpos) && !pathlist.Contains(newpos))
                {
                    if (distacncecheck(newpos, targetpos))
                    {
                        accepted.Add(newpos);

                    }

                    else
                    {
                        declined.Add(newpos);
                    }
                }
            }

            if (accepted.Count == 0)
            {
                foreach (Vector2 direction in declined)
                {

                    if (grid.Safezones.Contains(direction))
                    {
                        accepted.Add(direction);

                    }
                }

                int alternativemove = 0;

                if(accepted.Count == 0)
                {
                    pathlist.Add(pathlist[pathlist.Count-1]);
                    if (pathlist.Count == maxlistlenght)
                    {
                        Listfull = true;
                    }
                    continue;
                }

                for (int i = 0; i < accepted.Count-1; i++)
                {
                    float testdistace = Vector2.Distance(accepted[i], targetpos);
                    float optimaldistance = Vector2.Distance(accepted[alternativemove], targetpos);


                    if (testdistace < optimaldistance)
                    {
                        alternativemove = i;
                    }

                }
                pathlist.Add(accepted[alternativemove]);

                accepted.Clear();
                declined.Clear();
                
                if(pathlist.Count == maxlistlenght || Vector2.Distance(pathlist[pathlist.Count - 1], targetpos) < 2f)
                {
                    Listfull = true;
                    
                }
                continue;
            }

            int shortestdistance = 0;

            for (int i = 0; i < accepted.Count-1; i++)
            {
                float testdistace = Vector2.Distance(accepted[i], targetpos);
                float optimaldistance = Vector2.Distance(accepted[shortestdistance], targetpos);
                Vector2 backpacecheck = new Vector2(Mathf.Round(accepted[i].x * 10), Mathf.Round(accepted[i].y * 10));

                if (testdistace < optimaldistance)
                {
                    shortestdistance = i;
                }

            }
            pathlist.Add(accepted[shortestdistance]);

            accepted.Clear();
            declined.Clear();
            if (pathlist.Count == maxlistlenght || Vector2.Distance(pathlist[pathlist.Count - 1], targetpos) < 2f)
            {
                Listfull = true;
                
            }
           
        }

       if (Vector2.Distance(transform.position, currentdestination) < 0.5 || currentdestination == Vector2.zero)
        {
            currentdestination = pathlist[1];
            pathlist.RemoveAt(1);
            if(pathlist.Count == 1)
            {
                Listfull = false;
                currentdestination = Vector2.zero;
                return transform.position;
            }
            return currentdestination;
        }
        return currentdestination;
    }

    public void resetdata()
    {
        pathlist.Clear();
        pathlist.Add(startinglistpos);
        pathlist.Add(startinglistpos);
        foreach (Vector2 position in grid.Safezones)
        {

            if (Vector2.Distance(transform.position, position) < 0.5f)
            {
                pathlist[0] = position;
                pathlist[1] = position;
                break;
            }

        }
        currentdestination = Vector2.zero;
    }

}