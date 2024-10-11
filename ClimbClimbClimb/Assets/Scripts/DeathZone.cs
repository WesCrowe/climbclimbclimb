using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter(Collider c){

        Debug.Log("Collision Detected");

        if (c.gameObject.tag == "Player"){
            Debug.Log("Player Collided");
            c.gameObject.transform.position = respawnPoint.position;

            Debug.Log("Player Position: "+c.gameObject.transform.position);
            Debug.Log("Respawn Point position: "+respawnPoint.position);
        }
    }
}
