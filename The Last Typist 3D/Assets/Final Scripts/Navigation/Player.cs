using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public CharacterController cc;
    private float yVelocity;

    private void Update()
    {
        Camera cam = Camera.main;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = cam.transform.right * horizontal + cam.transform.forward * vertical;
        movement = Vector3.ProjectOnPlane(movement, Vector3.up);

        if (movement.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
            animator.SetBool("walking", true);

            if (Input.GetKey(KeyCode.LeftShift))
                animator.SetBool("running", true);
            else
                animator.SetBool("running", false);
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetBool("running", false);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetFloat("SpeedMult", 1.2f);
        }
        else
        {
            animator.SetFloat("SpeedMult", 1.0f);
        }

        // Gravity simulation; if on the ground, don't sink
        if (cc.isGrounded)
        {
            yVelocity = 0;
        }

        // Gravity force
        //yVelocity = -4*Time.deltaTime;
        yVelocity = 0;
    }

    private void OnAnimatorMove()
    {
        // Find out the change in position calculated by the animator
        Vector3 velocity = animator.deltaPosition;
        // Gravity isn't simulatd when we're using a character controller... time to fix that
        velocity.y = yVelocity;
        cc.Move(velocity);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
