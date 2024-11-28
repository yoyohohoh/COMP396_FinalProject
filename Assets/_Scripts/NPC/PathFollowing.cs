using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    public Path path;
    public float speed = 10.0f;
    [Range(1.0f, 1000.0f)]
    public float steeringInertia = 100.0f;
    public bool isLooping = true;
    public float waypointRadius = 1.0f;
    public float obstacleAvoidanceDistance = 5.0f;  // Distance to detect obstacles
    public float obstacleAvoidanceStrength = 5.0f;  // Strength of obstacle avoidance


    private float curSpeed;
    private int curPathIndex = 0;
    private int pathLength;
    private Vector3 targetPoint;
    private Vector3 velocity;

    private bool isEven = false;

    void Start()
    {
        path = GameObject.Find("Waypoints").GetComponent<Path>();
        pathLength = path.Length;

        // Initialize velocity towards the first waypoint
        targetPoint = path.GetPoint(0);
        velocity = (targetPoint - transform.position).normalized * speed * Time.deltaTime;
    }

    void Update()
    {
        // Unify the speed
        curSpeed = speed * Time.deltaTime;

        // Get the current target waypoint
        targetPoint = path.GetPoint(curPathIndex);

        // Check if the NPC is close to the current waypoint
        if (Vector3.Distance(transform.position, targetPoint) < waypointRadius)
        {
            if (isEven)
            {
                curPathIndex--;
                if (curPathIndex == 0)
                {
                    isEven = false;
                }
            }
            else
            {
                curPathIndex++;
                if (curPathIndex == pathLength - 1)
                {
                    isEven = true;
                }
            }
        }

        // Ensure we don't exceed the path length
        if (curPathIndex >= pathLength)
            return;

        // Calculate avoidance force
        Vector3 avoidanceForce = ObstacleAvoidance();

        // Calculate the next velocity
        Vector3 steeringForce = Steer(targetPoint);

        // Combine steering and avoidance forces
        velocity += steeringForce + avoidanceForce;

        // Limit velocity to current speed
        velocity = Vector3.ClampMagnitude(velocity, curSpeed);

        // Move the NPC
        transform.position += velocity;

        // Rotate the NPC to face its velocity direction
        if (velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(velocity);
        }
    }

    public Vector3 Steer(Vector3 target, bool bFinalPoint = false)
    {
        Vector3 desiredVelocity = (target - transform.position);
        float dist = desiredVelocity.magnitude;

        // Normalize the desired velocity
        desiredVelocity.Normalize();

        if (bFinalPoint && dist < waypointRadius)
            desiredVelocity *= curSpeed * (dist / waypointRadius);
        else
            desiredVelocity *= curSpeed;

        // Calculate the steering force
        Vector3 steeringForce = desiredVelocity - velocity;
        return steeringForce / steeringInertia;
    }

    Vector3 ObstacleAvoidance()
    {
        RaycastHit hit;
        Vector3 avoidanceForce = Vector3.zero;

        // Cast a ray in front of the NPC to detect obstacles
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleAvoidanceDistance))
        {
            if (hit.collider != null)
            {
                // Steer perpendicular to the hit surface
                Vector3 directionToAvoid = Vector3.Cross(hit.normal, Vector3.up).normalized;
                avoidanceForce = directionToAvoid * obstacleAvoidanceStrength;
            }
        }

        return avoidanceForce;
    }
}
