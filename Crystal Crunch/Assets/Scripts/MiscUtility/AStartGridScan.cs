using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AStartGridScan : MonoBehaviour
{
    //IEnumerator CurrentScan;

    void Start()
    {
        GameEventsSystem.gameEventsSystem.AreaEvents.LoadAreaEvent += ReScan;
    }

    void ReScan(string DUD)
    {
        StartCoroutine(ScanIteratively());
    }


    bool scanUnderWay= false;

    void Scan()
    {
        StartCoroutine(ScanIteratively());
    }

    IEnumerator ScanIteratively()
    {
        /*
        if(CurrentScan!=null)
        {
            yield return CurrentScan;
        }
        CurrentScan = ScanIteratively();
        */
       // while(scanUnderWay)
       // {
        //    yield return null;
        //}
       // scanUnderWay = true;
        foreach (NavGraph NG in AstarPath.active.graphs)
        {
            foreach (Progress progress in AstarPath.active.ScanAsync(NG))
            {
                yield return null;
            }
            yield return null;
        }
        //scanUnderWay = false;
       // yield break;
    }

}
