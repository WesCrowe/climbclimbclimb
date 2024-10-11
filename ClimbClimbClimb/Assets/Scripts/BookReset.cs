using UnityEngine;

public class DroppableReset : MonoBehaviour
{
    private Vector3 originalPosition; // To store the starting position

    void Start()
    {
        // Save the original position of the droppable object
        originalPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object collides with the plane
        if (collision.gameObject.CompareTag("Plane")) // Replace "Plane" with the correct tag or name of the plane
        {
            // Reset the object to its original position
            transform.position = originalPosition;
        }
    }
}
