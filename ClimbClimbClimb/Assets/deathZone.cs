using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZone : MonoBehaviour
{
    public GameObject player;
    public Transform spawn;
    private CharacterController controller;

    private void Start()
    {
        controller = player.GetComponent<CharacterController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("player fell");
            controller.enabled = false;       
            player.transform.position = spawn.position;
            controller.enabled = true;  
        }
    }
}
