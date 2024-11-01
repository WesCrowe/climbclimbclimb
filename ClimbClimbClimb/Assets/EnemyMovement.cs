using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 7f;
    public float distance = 20f;
    public bool moveX = false;
    public bool moveZ = false;
    public bool moveY = false;

    private float newTargetPos;
   
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 platformMovement;


    private Transform playerTransform;
    private bool playerOnPlatform = false;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (moveX)
        {
            float newX = startPos.x + Mathf.PingPong(Time.time * speed, distance);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
        else if (moveZ)
        {
            float newZ = startPos.z + Mathf.PingPong(Time.time * speed, distance);
            transform.position = new Vector3(transform.position.x, transform.position.y, newZ);
        }
        else if (moveY) 
        {
            float newY = startPos.y + Mathf.PingPong(Time.time * speed, distance);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        Vector3 platformMovement = transform.position - endPos;

        if (playerOnPlatform && playerTransform != null)
        {
            playerTransform.position += platformMovement;
        }
        
        endPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Player is on the Object");
            playerTransform = other.transform;
            playerOnPlatform = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            playerTransform = null;
        }
    }
}