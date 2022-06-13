using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCheckpoint : MonoBehaviour
{
    string MySceneLocation;
    private void Awake()
    {
        MySceneLocation = this.gameObject.scene.name;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            ActiveSceneManager._Current.RespawnLocation = this.transform.position;
            ActiveSceneManager._Current.RespawnToScene = MySceneLocation;
        }
    }
}
