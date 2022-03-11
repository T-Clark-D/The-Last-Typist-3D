using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControllerNetwork : MonoBehaviour
{
    public float m_speed;
    private Rigidbody m_RB;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            Movement();
        }
    }

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * m_speed;
        m_RB.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
