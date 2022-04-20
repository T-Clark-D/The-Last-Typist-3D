using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentAI : MonoBehaviour
{
    public Transform target;
    public GameObject meatGrinder;
    public float remainingDistance;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
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
