using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public DeathZone dz;

    void Start(){
        dz = GameObject.Find("DeathZone").GetComponent<DeathZone>();
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Player"){
            dz.respawnPoint.position = gameObject.transform.position;
        }
    }
}
