using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{
    public GameObject player;
    public Transform spawn;

    
    void Awake()
    {
        player.transform.position = spawn.position;     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
