/* BouncePad.cs
 * Author: Wesley Crowe
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the player lands on the bounce pad, take their y velocity and reverse it
    // with even more force
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Player"){
            Vector3 velocity = collision.gameObject.GetComponent<Rigidbody>().velocity;

            //reverse the vertical velocity to send them up
            // bounce higher by multiplying it
            velocity.y = -velocity.y * 2;
        }
    }
}
