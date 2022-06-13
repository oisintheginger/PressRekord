using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followscript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
    }
}
