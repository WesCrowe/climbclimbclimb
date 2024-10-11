using UnityEngine;

public class ButtonCollision : MonoBehaviour
{
    
    public Transform flyingPlatform; // The object that will move when the button is pressed
    public Transform initalPosition;
    public Transform pressedPosition;
    //public Transform targetObject; // The empty GameObject acting as the target position

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the one you're expecting (optional: tag check or name check)
        //if (collision.gameObject.CompareTag("Droppable")) // Replace "Droppable" with your object's tag
        //{
        //    // Move the object to the position of the targetObject
        //    flyingPlatform.position = targetObject.position;
        //}
        Debug.Log("pressed");
        flyingPlatform.position = pressedPosition.position;
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("not pressed");
        flyingPlatform.position = initalPosition.position;
    }
}