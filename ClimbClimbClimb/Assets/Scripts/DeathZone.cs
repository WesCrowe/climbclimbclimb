/* Author: Wesley Crowe
 * DeathZone.cs
 * A script that respawns the player if they fall out of bounds
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public Transform respawnPoint;

    void OnTriggerEnter(Collider c){

        Debug.Log("Collision Detected");

        if (c.gameObject.tag == "Player"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
