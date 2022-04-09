using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerControllerNetwork : MonoBehaviour
{
    public float m_speed;

    private bool m_isCombatMode;

    private Rigidbody m_RB;
    [SerializeField] Text m_writtenText;

    // Written Text Script
    [SerializeField] GameObject m_playerAnchor;
    private Camera m_cam;
    private RectTransform m_RT;

    // For targetting and killing zombies
    [SerializeField] private List<GameObject> targetedInstances;
    public TargetableNetwork currentTarget;

    // Spike testing
    [SerializeField] GameObject spikeTrapPrefab;


    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody>();

        m_cam = Camera.main;
        m_RT = m_writtenText.GetComponent<RectTransform>();

        //targetedInstances = new List<TargetableNetwork>();
    }

    void Update()
    {
        m_RT.position = m_cam.WorldToScreenPoint(m_playerAnchor.transform.position);
        if (m_isCombatMode)
        {
            GetTextInput();
            FindMatchingTarget();
        }
        else if (!m_isCombatMode)
        {
            Movement();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_isCombatMode = !m_isCombatMode;
        }

    }

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * m_speed;
        m_RB.MovePosition(transform.position + velocity * Time.deltaTime);
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

    private void FindMatchingTarget()
    {
        var a = GameObject.FindGameObjectsWithTag("NPC");
        if (a == null || a.Length == 0)
        {
            return;
        }
        targetedInstances = a.ToList();
        targetedInstances.ToList();
        targetedInstances.Sort(DistanceToPlayer);
        Debug.Log("Closest target is " + targetedInstances[0].gameObject.name);
        foreach (GameObject target in targetedInstances)
        {
            var targetScript = target.GetComponent<TargetableNetwork>();
            if (targetScript.targetWord == m_writtenText.text)
            {
                ((EnemiesNetwork)targetScript).Death();
                targetedInstances.Remove(target);
                m_writtenText.text = "";
            }
            else if (m_writtenText.text.Length < targetScript.targetWord.Length && m_writtenText.text == targetScript.targetWord.Substring(0, m_writtenText.text.Length))
            {
                ((EnemiesNetwork)targetScript).TargetedText(m_writtenText.text.Length);
                currentTarget = targetScript;
            }
            else
            {
                ((EnemiesNetwork)targetScript).TargetedText(0);
            }
        }
/*
        var TargetableObjects = FindObjectsOfType<TargetableNetwork>();
        foreach (TargetableNetwork target in TargetableObjects)
        {
            if (target.targetWord == m_writtenText.text)
            {
                ((EnemiesNetwork)target).Death();
                //targetedInstances.Remove(target);
                m_writtenText.text = "";
            }
            else if (m_writtenText.text.Length < target.targetWord.Length && m_writtenText.text == target.targetWord.Substring(0, m_writtenText.text.Length))
            {
                ((EnemiesNetwork)target).TargetedText(m_writtenText.text.Length);
                currentTarget = target;
            }
            else
            {
                ((EnemiesNetwork)target).TargetedText(0);
            }
        }
*/
    }

    private int DistanceToPlayer(GameObject a, GameObject b)
    {
        float squaredDistanceA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredDistanceB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredDistanceA.CompareTo(squaredDistanceB);
    }

}
