using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints (empty GameObjects)
    public float speed = 2f; // Speed of the platform movement
    private int currentWaypointIndex = 0; // Current target waypoint

    void Update()
    {
        // If there are waypoints set
        if (waypoints.Length > 0)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        // Move towards the current waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        float step = speed * Time.deltaTime; // Calculate step size

        // Move the platform towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        // Check if the platform has reached the current waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Update to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop back to the first waypoint
        }
    }
}
