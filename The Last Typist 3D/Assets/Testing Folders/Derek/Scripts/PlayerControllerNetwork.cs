using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerControllerNetwork : MonoBehaviour
{
    public float m_speed;

    private bool m_isCombatMode;

    private Rigidbody m_RB;
    public GameObject m_writtenTextBox;
    public Text m_writtenText;

    private PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
        view = GetComponent<PhotonView>();
        m_writtenText = m_writtenTextBox.GetComponent<Text>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            if (m_isCombatMode)
            {
                GetTextInput();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_isCombatMode = !m_isCombatMode;
            }
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (!m_isCombatMode)
            {
                Movement();
            }
        }
    }

    private void GetTextInput()
    {
        m_writtenText.text += Input.inputString;
        if (Input.GetKeyDown(KeyCode.Backspace) && m_writtenText.text.Length > 1)
        {
            string backSpacedText = m_writtenText.text.Substring(0, m_writtenText.text.Length - 2);
            m_writtenText.text = backSpacedText;
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
