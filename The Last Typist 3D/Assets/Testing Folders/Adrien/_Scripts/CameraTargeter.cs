using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class CameraTargeter : MonoBehaviour
{
    public GameObject player;
    public PlayerControllerNetwork pc;
    public List<GameObject> npcList;
    [SerializeField] private CinemachineVirtualCamera cam;

    private void Awake()
    {
        pc = player.GetComponent<PlayerControllerNetwork>();
        cam = GameObject.FindGameObjectWithTag("vcam1").GetComponent<CinemachineVirtualCamera>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SortNPCList();
        SetCameraTarget();
    }

    private void SortNPCList()
    {
        var a = GameObject.FindGameObjectsWithTag("NPC");
        if (a == null || a.Length == 0)
        {
            return;
        }
        npcList = a.ToList();
        npcList.ToList();
        npcList.Sort(DistanceToPlayer);
        //Debug.Log("Closest target is " + npcList[0].gameObject.name);
    }

    private int DistanceToPlayer(GameObject a, GameObject b)
    {
        float squaredDistanceA = (a.transform.position - transform.position).sqrMagnitude;
        float squaredDistanceB = (b.transform.position - transform.position).sqrMagnitude;
        return squaredDistanceA.CompareTo(squaredDistanceB);
    }

    private void SetCameraTarget()
    {
        if (pc.targetedInstances != null || pc.targetedInstances.Count > 0)
        {
            cam.LookAt = npcList[0].transform;
        }
    }
}
