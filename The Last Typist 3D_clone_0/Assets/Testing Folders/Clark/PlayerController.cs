using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool m_isCombatMode;
    public float m_speed;

    private Rigidbody m_RB;
    public GameObject m_writtenTextBox;
    public Text m_writtenText;

    private List<Targetable> targetedInstances;
    public Targetable currentTarget;

    void Start()
    {
        targetedInstances = new List<Targetable>();
        m_isCombatMode = false;
        m_RB = GetComponent<Rigidbody>();
        m_writtenText = m_writtenTextBox.GetComponent<Text>();
    }

    void Update()
    {
        if (m_isCombatMode)
        {
            //read input
            GetTextInput();
            //find partial matching and matching target 
            FindMatchingTarget();
            //aim at target
        }
        else
        {
            Movement();
        }
        //toggles between in and out of combat mode using space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_isCombatMode = !m_isCombatMode;
           
            //clear the written text
            m_writtenText.text = "";
        }
    }

    private void FindMatchingTarget()
    {
        Debug.Log(targetedInstances.ToString());
        var TargetableObjects = FindObjectsOfType<Targetable>();
        foreach (Targetable target in TargetableObjects)
        {
            if (target.targetWord == m_writtenText.text)
            {
                ((Enemies)target).Death();
                targetedInstances.Remove(target);
                m_writtenText.text = "";
                //we need to keep track of these so that we can reset the partial matching and dynamically switch targets
                
            }
            else if (/*m_writtenText.text.Length != 0 && */m_writtenText.text.Length < target.targetWord.Length && m_writtenText.text == target.targetWord.Substring(0, m_writtenText.text.Length))
            {
                ((Enemies)target).TargetedText(m_writtenText.text.Length);
                currentTarget = target;
                //targetedInstances.Add(target);
            }
            else
            {
                //check if that enemy triggered the partial matching
                ((Enemies)target).TargetedText(0);
            }
        } 
        /*
        if (targetedInstances.Count > 0)
        {
            try
            {
                foreach (Targetable targeted in targetedInstances)
                {
                    if (targeted != null && m_writtenText.text.Length <= targeted.targetWord.Length && !targeted.targetWord.Substring(0, m_writtenText.text.Length).Equals(m_writtenText.text))
                    {
                        Debug.Log("we in if for unmathcing instances");
                        //untargets
                        ((Enemies)targeted).TargetedText(0);
                        targetedInstances.Remove(targeted);
                    }
                }
            }
            catch(Exception e)
            {
            }
        }*/
    }

    private void GetTextInput()
    {
        m_writtenText.text += Input.inputString;
        if (Input.GetKeyDown(KeyCode.Backspace) && m_writtenText.text.Length > 1)
        {
            String backSpacedText = m_writtenText.text.Substring(0, m_writtenText.text.Length - 2);
            m_writtenText.text = backSpacedText;
        }
    }

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * m_speed;
        m_RB.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
