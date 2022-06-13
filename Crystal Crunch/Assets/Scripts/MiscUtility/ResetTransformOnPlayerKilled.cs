using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ResetTransformOnPlayerKilled : MonoBehaviour
{
    List<GameObject> MyChildrenObjects;
    List<Vector2> MyChildrenObjectsStartPosition;
    void Start()
    {
        MyChildrenObjects = new List<GameObject>();
        MyChildrenObjectsStartPosition = new List<Vector2>();

        for(int i = 0; i<transform.childCount; i++)
        {
            MyChildrenObjects.Add(this.transform.GetChild(i).gameObject);
            MyChildrenObjectsStartPosition.Add(this.transform.GetChild(i).position);
        }

        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent += ResetChildPositions;
    }

    void ResetChildPositions()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            MyChildrenObjects[i].transform.position = MyChildrenObjectsStartPosition[i];
        }
    }
    private void OnDestroy()
    {
        GameEventsSystem.gameEventsSystem.PlayerGameEvents.PlayerDiedEvent -= ResetChildPositions;
    }

}
