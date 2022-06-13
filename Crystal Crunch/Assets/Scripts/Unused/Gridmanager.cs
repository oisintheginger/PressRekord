using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gridmanager : MonoBehaviour
{

   [SerializeField] List<Vector2> gridposition = new List<Vector2>();
    public List<Vector2> Safezones = new List<Vector2>();
    bool notsafe = false;

    [SerializeField] int LENGTH, WIDTH;
    // Start is called before the first frame update
   public Vector2[] Nodecontainer = new Vector2[]
    {
        new Vector2 (1f,0),
        new Vector2 (1f,1f),
        new Vector2 (1f,-1f),
        new Vector2 (-1f,0),
        new Vector2 (-1f,-1f),
        new Vector2 (0,1f),
        new Vector2 (0,-1f),
        new Vector2(-1f,1f)

    };

    bool passable(Vector2 originalposition,Vector2 directioncheck)
    {
        RaycastHit2D hit = Physics2D.Linecast(originalposition, directioncheck);
        //Debug.DrawRay(transform.position, entitiydir.normalized, Color.green);
        if (hit.collider == null)
        {
            return true;
        }
        if (hit.transform.CompareTag("Player")||hit.transform.CompareTag("enemy") || hit.collider.isTrigger)
        {
            return true;
        }

        return false;
    }

    void Start()
    {
        for( int lenght = 0;lenght <= LENGTH; lenght++)
        {
            for (int width = 0; width <= WIDTH; width++)
            {

                gridposition.Add(new Vector2(lenght/2, width/2));
                
            }
        }
        foreach (Vector2 direction in gridposition)
        {
            notsafe = false;
            for (int i = 0; i < Nodecontainer.Length; i++)
            {
                Vector2 newpos = direction + Nodecontainer[i];

                if (!passable(direction,newpos))
                {
                    notsafe = true;
                    break;

                }
            }
            if (notsafe == false)
            {
                Safezones.Add(direction);
            }

        }
    }
  /* public void OnDrawGizmos()
    {
        if(Safezones!= null)
        {
            for (int i = 0; i < Safezones.Count - 1; i++)
            {
                Gizmos.DrawWireSphere(new Vector3(Safezones[i].x, Safezones[i].y, 0),1f);
            }
           
        }
    }*/

}
