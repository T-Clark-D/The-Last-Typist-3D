using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Witch : Enemies
{
    public int m_wordLength = 5;
    public List<GameObject> corpseList;
    public NavMeshAgentAI agent;
    public GameObject player;
    WitchDoctor_Controller witchController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.target = player.transform;
        base.Initialize();
        base.InitializeTextBoxWithLength(m_wordLength);
        witchController = GetComponent<WitchDoctor_Controller>();
        if (m_speed != 0)
        {
            witchController.StartOrStopWalkAnimation1();
        }
    }

    // Update is called once per frame
    void Update()
    {
        SortCorpseList();
        SetWitchTarget();
        if (killed)
        {
            witchController.StartOrStopWalkAnimation1();
            witchController.DeathAnimation();
            gameObject.GetComponent<Enemies>().enabled = false;
        }
    }

    public bool getKilled()
    {
        return killed;
    }

    private void SortCorpseList()
    {
        var a = GameObject.FindGameObjectsWithTag("corpse");
        if (a == null || a.Length == 0)
        {
            corpseList.Clear();
            return;
        }
        corpseList = a.ToList();
        corpseList.ToList();
        corpseList.Sort(DistanceToWitch);
        //Debug.Log("Closest target is " + npcList[0].gameObject.name);
    }

    private int DistanceToWitch(GameObject a, GameObject b)
    {
        float squaredDistanceA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredDistanceB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredDistanceA.CompareTo(squaredDistanceB);
    }

    private void SetWitchTarget()
    {
        // || corpseList.Count <= 0
        if (corpseList == null)
        {
            agent.target = player.transform;
        }
        else
        {
            agent.target = corpseList[0].transform;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "corpse")
        {
            collision.gameObject.tag = "NPC";
            collision.gameObject.GetComponent<Enemies>().enabled = true;
            collision.gameObject.GetComponent<Enemies>().killed = false;
            if (collision.gameObject.GetComponent<Zombie_Controller>() != null)
            {
                collision.gameObject.GetComponent<Zombie_Controller>().ReviveAnimation();
            }
            else if (collision.gameObject.GetComponent<BombZombie_Controller>() != null)
            {
                //collision.gameObject.GetComponent<BombZombie_Controller>().ReviveAnimation();
            }
        }
    }
}
