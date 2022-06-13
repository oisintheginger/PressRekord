using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour {

    [SerializeField] Transform respawnPoint;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.transform.position = respawnPoint.position;
        }
    }

}
