using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class transform : MonoBehaviour
{
    public float speed = 7f;
    public float height = 20f;
   
    private float newTargetPos;
    private Vector3 startPos;
   

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            float newY = startPos.y + Mathf.PingPong(Time.time * speed, height);

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
   
}
