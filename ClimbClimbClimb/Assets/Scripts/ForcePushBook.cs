using UnityEngine;

public class ForceMovingBook : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints (empty GameObjects)
    public float drawbackSpeed = 0.5f;
    public float pushSpeed = 2f;
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
        float step = 0;
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        if (currentWaypointIndex == 0) {
            step = pushSpeed * Time.deltaTime; // Calculate step size
        } else {
            step = drawbackSpeed * Time.deltaTime;
        }

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
