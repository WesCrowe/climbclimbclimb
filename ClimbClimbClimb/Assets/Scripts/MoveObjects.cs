using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveObjects : MonoBehaviour
{

    public Transform destination;

    public int speed = 10;

    private float step;

    // Start is called before the first frame update
    void Start()
    {
        step = speed * Time.deltaTime; // Calculate step size

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position != destination.position || gameObject.transform.rotation != destination.rotation){
            transform.position = Vector3.MoveTowards(transform.position, destination.position, step);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, destination.rotation, step);
        }
    }
}
