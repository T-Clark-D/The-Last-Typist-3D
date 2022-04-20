using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentAI : MonoBehaviour
{
    public Transform target;
    public GameObject meatGrinder;
    public float remainingDistance;
    private BasicZombie BZ;
    private Bombie B;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //BZ = GetComponent<BasicZombie>();
        //B = GetComponent<Bombie>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "NPC")
        {
            // Movement, zombie is alive
            agent.destination = target.position;
            if (agent.pathPending)
            {
                remainingDistance = -1;
            }
            else
            {
                //remainingDistance = agent.remainingDistance;
                remainingDistance = getPathDistanceWithCorners(agent.path);
            }
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    private float getPathDistanceWithCorners(NavMeshPath path)
    {
        float distance = 0f;

        for (var i = 0; i < path.corners.Length-1; i++)
        {
            distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
        }

        return distance;
    }
}
