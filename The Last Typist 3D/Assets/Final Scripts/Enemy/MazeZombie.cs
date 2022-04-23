using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeZombie : Enemies
{
    public int m_wordLength = 5;
    public float explosionRadius;
    public CapsuleCollider capsuleCollider;
    public GameObject targetObj;
    public float distance;

    WitchDoctor_Controller witchZombieController;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        base.InitializeTextBoxWithLength(m_wordLength);
        witchZombieController = GetComponent<WitchDoctor_Controller>();
        if (m_speed != 0)
        {
            witchZombieController.StartOrStopWalkAnimation1();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "NPC" && targetObj != null)
        {
            distance = (targetObj.transform.position - transform.position).magnitude;
            Vector3 direction = (targetObj.transform.position - transform.position).normalized;
            Vector3 velocity = direction * m_speed;
            transform.LookAt(targetObj.transform.position);
            transform.position = (transform.position + velocity * Time.deltaTime);
        }
        if (killed)
        {
            witchZombieController.StartOrStopWalkAnimation1();
            witchZombieController.DeathAnimation();
            gameObject.GetComponent<Enemies>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[BOMBIE] Bomber zombie collided with object.");
        if (other.tag == "Player"
            || other.tag == "MeatGrinder"
            || other.tag == "SandBag"
            || other.tag == "SpikeTrap")
        {
            m_speed = 0;
            Debug.Log("[BOMBIE] Collided with "+other.tag);
        }
    }

    public bool getKilled()
    {
        return killed;
    }
}
