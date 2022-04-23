using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // Movement speed scalar for seek
    public float moveSpeed;
    // Maximum speed threshold for animator blend tree
    public float maxSpeed;
    // LookWhereYouMove rotation speed (degrees per second)
    public float rotationSpeed = 300f;
    // The object the NPC will move towards
    public GameObject targetObj;
    public Rigidbody rb;
    private Animator animator;
    // Radii for smooth movement
    public float stopRadius = 1f;
    public float slowRadius = 6f;
    public float farRadius = 1000f;
    // The distance bewteen the NPC and the target
    public float distance;

    // The NPC's target (used to access the Vector3)
    private Transform target;
    // The NPC's actual movement speed magnitude
    private float seekVelocityMagnitude = 0;
    // The NPC's movement vector, fed into seek
    [SerializeField] private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = Vector3.zero;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if (targetObj != null)
        {
            target = targetObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Animator magic, DO NOT TOUCH
        animator.SetFloat("Blend", seekVelocityMagnitude / maxSpeed);
    }

    private void FixedUpdate()
    {
        if (targetObj != null)
        {
            target = targetObj.transform;
            // First, find the target
            LockTarget();
            // Do the kinematic seek math to determine the velocity + movement vector
            movement = KinematicSeek();
            // Rotate towards target
            LookAtMovement();
            // Use movement vector to move
            Move();
        }
    }

    // Apply the movement vector to the rigidbody.
    void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Kinematic seek, has built-in arrive function.
    Vector3 KinematicSeek()
    {
        Vector3 desiredVelocity = target.position - transform.position;
        float distance = desiredVelocity.magnitude;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        if (distance <= stopRadius)
        {
            desiredVelocity *= 0;
        }
        else if (distance < slowRadius)
        {
            desiredVelocity *= (distance / slowRadius);
        }

        seekVelocityMagnitude = desiredVelocity.magnitude;
        return desiredVelocity;
    }

    // Look in the direction of movement.
    void LookAtMovement()
    {
        // If the player is moving...
        if (movement != Vector3.zero)
        {
            // ... Create a quaternion which sets where the player should be facing.
            Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(movement, Vector3.up), rotationSpeed * Time.fixedDeltaTime);
            // Rotate towards that direction at a set rotation speed.
            rb.MoveRotation(targetRotation);
        }
    }

    // Save the NPC's target and the distance to said target.
    void LockTarget()
    {
        movement = target.position - transform.position;
        distance = movement.magnitude;
    }
}
