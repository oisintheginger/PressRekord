using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] GameObject spawnee;

    public void spawnobject()
    {
        Instantiate(spawnee, transform.position,transform.rotation);
    }
}
