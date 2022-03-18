using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{

    public CharacterController cc;
    public float speed;
    public float turningSmoothFactor;
    private float turningSmoothVelocity;
    private Vector3 direction;
    private float targetAngle;
    private float currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.zero;
        //speed = 6f;
        targetAngle = 0f;
        currentAngle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //direction.x = Input.GetAxisRaw("Horizontal");
        //direction.z = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        if (direction.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            currentAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turningSmoothVelocity, turningSmoothFactor);
            transform.rotation = Quaternion.Euler(0f, currentAngle, 0f);
            cc.Move(direction * speed * Time.deltaTime);
        }
    }
}
