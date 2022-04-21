using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicZombie : Enemies
{

    public int m_wordLength = 5;
    Zombie_Controller zombieController;
    NavMeshAgentAI agent;

    // Start is called before the first frame update
    public void Start()
    {
        agent = GetComponent<NavMeshAgentAI>();
        base.Initialize();
        base.InitializeTextBoxWithLength(m_wordLength);
        zombieController = GetComponent<Zombie_Controller>();
        if (m_speed != 0)
        {
            zombieController.StartOrStopWalkAnimation1();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (killed)
        {

            zombieController.StartOrStopWalkAnimation1();
            zombieController.DeathAnimation();
            gameObject.GetComponent<Enemies>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[BASIC] Basic zombie collided with object.");
        if (other.tag == "Player")
        {
            m_speed = 0;
            Debug.Log("[BASIC] Collided with " + other.tag);
            StartCoroutine(Attack());
        }
        else if (other.tag == "SpikeTrap")
        {
            Death();
        }
    }

    private IEnumerator Attack()
    {
        var init_speed = agent.getSpeed();
        agent.setSpeed(0f);
        zombieController.StartOrStopWalkAnimation1();
        zombieController.StartOrStopAttackAnimation();
        yield return new WaitForSeconds(3.333f);
        zombieController.StartOrStopAttackAnimation();
        zombieController.StartOrStopWalkAnimation1();
        agent.setSpeed(init_speed);
    }

    public bool getKilled()
    {
        return killed;
    }
}
