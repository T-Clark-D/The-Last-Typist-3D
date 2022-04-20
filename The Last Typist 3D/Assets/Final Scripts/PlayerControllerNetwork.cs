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
    public Targetable currentTarget;

    // Spike testing
    [SerializeField] GameObject spikeTrapPrefab;

    public MainCharacter_Controller MCC;
    private bool isWalking = false;
   

    // Start is called before the first frame update
    void Start()
    {

        MCC = GetComponentInChildren<MainCharacter_Controller>();
        m_RB = GetComponent<Rigidbody>();

        m_cam = Camera.main;
        m_RT = m_writtenText.GetComponent<RectTransform>();

        //targetedInstances = new List<TargetableNetwork>();
    }

    void Update()
    {
        m_RT.position = m_cam.WorldToScreenPoint(m_playerAnchor.transform.position);

        if (GameHandler.waveMode)
        {
            if (m_isCombatMode)
            {
                GetTextInput();
                FindMatchingTarget();
            }
            else if (!m_isCombatMode)
            {
                Movement();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_isCombatMode = !m_isCombatMode;
                MCC.SwitchMode();
                
                m_writtenText.text = "";
            }
        }

        if (GameHandler.resourceGatheringMode)
        {
            GetTextInput();
            FindMatchingTarget();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_writtenText.text = "";
            }
        }

    }

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //animations
        AnimateWalking(input);
        //face correct direction
        if(input.magnitude!=0)
        gameObject.transform.forward = -input;
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * m_speed;
        m_RB.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void AnimateWalking(Vector3 input)
    {
        if (input.magnitude > 0 && !isWalking)
        {
            MCC.StartOrStopWalkAnimation();
            isWalking = true;
        }
        else if (input.magnitude == 0 && isWalking)
        {
            MCC.StartOrStopWalkAnimation();
            isWalking = false;
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

    private void FindMatchingTarget()
    {
        if (GameHandler.waveMode)
        {
            var a = GameObject.FindGameObjectsWithTag("NPC");

            if (a == null || a.Length == 0)
            {
                return;
            }
            targetedInstances = a.ToList();
            targetedInstances.ToList();
            targetedInstances.Sort(DistanceToPlayer);

            foreach (GameObject target in targetedInstances)
            {
                var targetScript = target.GetComponent<Targetable>();
                if (targetScript.targetWord == m_writtenText.text)
                {
                    //Debug.Log(targetScript.targetWord);
                    GameHandler.currentEnemyNum--;
                    targetedInstances.Remove(target);
                    ((Enemies)targetScript).Death();
                    m_writtenText.text = "";

                }
                else if (m_writtenText.text.Length < targetScript.targetWord.Length && m_writtenText.text == targetScript.targetWord.Substring(0, m_writtenText.text.Length))
                {
                    ((Enemies)targetScript).TargetedText(m_writtenText.text.Length);
                    currentTarget = targetScript;
                }
                else
                {
                    ((Enemies)targetScript).TargetedText(0);
                }
            }
        }
        //searchesthrough corpses instead
        if (GameHandler.resourceGatheringMode)
        {
            var a = GameObject.FindGameObjectsWithTag("corpse");

            if (a == null || a.Length == 0)
            {
                return;
            }
            targetedInstances = a.ToList();
            targetedInstances.ToList();
            targetedInstances.Sort(DistanceToPlayer);

            foreach (GameObject target in targetedInstances)
            {
                var targetScript = target.GetComponent<Targetable>();
                if (targetScript.targetWord == m_writtenText.text)
                {
                    //Debug.Log(targetScript.targetWord);
                    GameHandler.currentEnemyNum--;
                    targetedInstances.Remove(target);
                    ((Enemies)targetScript).GrindUp();
                    m_writtenText.text = "";

                }
                else if (m_writtenText.text.Length < targetScript.targetWord.Length && m_writtenText.text == targetScript.targetWord.Substring(0, m_writtenText.text.Length))
                {
                    ((Enemies)targetScript).TargetedText(m_writtenText.text.Length);
                    currentTarget = targetScript;
                }
                else
                {
                    ((Enemies)targetScript).TargetedText(0);
                }
            }
        }


    }

    private int DistanceToPlayer(GameObject a, GameObject b)
    {
        float squaredDistanceA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredDistanceB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredDistanceA.CompareTo(squaredDistanceB);
    }

}
